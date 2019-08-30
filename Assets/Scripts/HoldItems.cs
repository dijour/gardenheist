using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using Indicator;
using Sound;

 public class HoldItems : NetworkBehaviour {
    public float speed = 5;
    public bool canHold = true;
    public GameObject targetBall;
    public GameObject heldBall;
    public Transform guide;
    public HighlightPickup outlined;

    public Transform cameraTransform;

    private bool clicked = false;
    public bool collided = false;
    public bool hidden = false;
    public bool isLocal = false;
    public GameObject model;
    public GameObject textBox;
    public IndicatorText indText;
    public GameObject textMeshP;

    public UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc; 

    private bool caught;
 
    void Start(){
        indText = (IndicatorText) textBox.GetComponent(typeof(IndicatorText));
        textMeshP = GameObject.Find("NumRabbitsKilled");
        isLocal = this.isLocalPlayer;
    }
    void Update()
    {
        if (this.isLocalPlayer) {
            //isLocal = true;
            RaycastHit hit;
            GameObject newball;
            if(canHold && Physics.SphereCast(cameraTransform.position, 0.03f, cameraTransform.forward, out hit, 0.3f) &&
                            hit.collider.gameObject.CompareTag("pickable"))
            {
                newball = hit.collider.gameObject;
            } else {
                newball = null;
            }

            // Disable picking up if we can't move
            if (!fpc.canMove) newball = null;

            if (newball != targetBall) {
                targetBall = newball;
                if (outlined) {
                    outlined.RemoveOutline();
                }
                if (targetBall != null) {
                    outlined = targetBall.GetComponent<HighlightPickup>();
                    if (outlined != null) {
                        outlined.AddOutline();
                    }
                }
            }
            if (Input.GetMouseButtonDown(0))
            {
                print("Mouse down");
                Cmd_PickupThrow(targetBall, cameraTransform.forward.x, cameraTransform.forward.y,
                                                    cameraTransform.forward.z, speed);
            }
            if (Input.GetKeyDown(KeyCode.F) && hidden){
                print("Back!");
                indText.BurrowReveal();
                Cmd_BurrowReveal();
            }
            if (Input.GetKeyDown(KeyCode.F) && collided && !(hidden)){
                print("Hide!");
                //fpc.Cmd_setCanMove();
                //collided = false;
                indText.BurrowHide();
                Cmd_BurrowHide();
            }
            collided = false;
        }

       
   }//update

     //We can use trigger or Collision
    void OnTriggerEnter(Collider col)
    {
        //col.GetComponent<Collider>().enabled = false;
        if (col.gameObject.tag == "net" && !this.caught){
            print("Caught rabbit w net");
            //fpc.canMove = false;
            //indText.Caught();
            caught = true;
            UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc = (UnityStandardAssets.Characters.FirstPerson.FirstPersonController) this.gameObject.GetComponent(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController));
            forceDrop();
            fpc.Cmd_setCanMove();
            CountRabbit cr = (CountRabbit) textMeshP.GetComponent<CountRabbit>();
            cr.KillRabbit();
            StartCoroutine(ExecuteAfterTime(2));
            //SteamVR_Controller.Input(0).TriggerHapticPulse(500);
            //this.gameObject.SendMessage("setCanMove");
        }
        //col.GetComponent<Collider>().enabled = true;
    }
    
    IEnumerator ExecuteAfterTime(float time) {
        yield return new WaitForSeconds(time);
        caught = false;
    }

    void OnTriggerStay(Collider col){
        if (col.gameObject.tag == "burrow"){
            print("Colliding with burrow");
            collided = true;
        }
    }
    
    void OnCollisionEnter(Collision col){
        /**
        print("Collided");
        if (col.gameObject.tag == "burrow"){
            print("Collided with burrow");
            collided = true;
            Renderer rend = this.gameObject.GetComponent<Renderer>();
            rend.enabled = false;
        }**/
    }
    [Command]
    private void Cmd_BurrowHide(){
        Rpc_BurrowHide();
    }
    [Command]
    private void Cmd_BurrowReveal(){
        Rpc_BurrowReveal();
    }
    [ClientRpc]
    void Rpc_BurrowHide(){
        //Renderer rend = model.gameObject.GetComponent<Renderer>();
        //rend.enabled = false;
        model.SetActive(false);
        fpc.canMove = false;
        hidden = true;
    }
    [ClientRpc]
    void Rpc_BurrowReveal(){
        //Renderer rend = model.gameObject.GetComponent<Renderer>();
        //rend.enabled = true;
        model.SetActive(true);
        fpc.canMove = true;
        hidden = false;

    }
 
    [ClientRpc]
    private void Rpc_PickedUp(GameObject newball)
    {
        newball.GetComponent<Rigidbody>().useGravity = false;
        newball.GetComponent<Rigidbody>().detectCollisions = false;
        newball.GetComponent<Rigidbody>().isKinematic = true;
        newball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        newball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //newball.GetComponent<NetworkTransform>().enabled = false;
        newball.transform.SetParent(guide);

        //we apply the same rotation our main object (Camera) has.
        newball.transform.localRotation = transform.rotation;
        //We re-position the ball on our guide object 
        newball.transform.position = guide.position;

        Vegetable v = newball.GetComponent<Vegetable>();
        string name = "Object";
        if (v) {
            name = v.vegName;
        }

        if (this.isLocalPlayer) {
            print("Picked up!");
            indText.PickUp(name);
        }
        canHold = false;
    }

    [ClientRpc]
    private void Rpc_Thrown(GameObject thrown)
    {
        thrown.GetComponent<Rigidbody>().useGravity = true;
        thrown.GetComponent<Rigidbody>().detectCollisions = true;
        thrown.GetComponent<Rigidbody>().isKinematic = false;
        thrown.GetComponent<Collider>().isTrigger = false;
        //thrown.GetComponent<NetworkTransform>().enabled = true;
        thrown.transform.parent = null;

        Vegetable v = thrown.GetComponent<Vegetable>();
        string name = "Object";
        if (v) {
            v.thrower = this;
            name = v.vegName;
        }

        if (this.isLocalPlayer) {
            indText.Drop(name);
            print("Thrown...");
        }
        canHold = true;
    }

    public void playScore(string name) {
        if (this.isServer) {
            Rpc_PlayScore(name);
        }
    }

    [ClientRpc]
    public void Rpc_PlayScore(string name) {
        if (this.isLocalPlayer) {
            indText.Score(name);
        }
    }

    public void forceDrop() {
        // Force a drop by setting newball to null
        Cmd_PickupThrow(null, 0, 0, 0, 0);
    }

    [Command]
     private void Cmd_PickupThrow(GameObject newball, float launchX, float launchY, float launchZ, float speed)
     {
        if (canHold) {
            if (!newball)
                return;
            this.heldBall = newball;
            //Cmd_AssignLocalAuthority(ball);

            heldBall.GetComponent<Rigidbody>().useGravity = false;
            heldBall.GetComponent<Rigidbody>().detectCollisions = false;
            heldBall.GetComponent<Rigidbody>().isKinematic = true;
            heldBall.GetComponent<NetworkTransform>().enabled = false;
            //Cmd_ChangeRigidBody(ball, false);
    
            //We set the object parent to our guide empty object.
            heldBall.transform.SetParent(guide);
            print("Set parent");

            //we apply the same rotation our main object (Camera) has.
            heldBall.transform.localRotation = transform.rotation;
            //We re-position the ball on our guide object 
            heldBall.transform.position = guide.position;

            Rpc_PickedUp(newball);

            canHold = false;
        } else {
            if (!heldBall) {
                canHold = true;
                return;
            }
            //Cmd_ChangeRigidBody(ball, true);

            GameObject myball = guide.GetChild(0).gameObject;

            Vegetable v = myball.GetComponent<Vegetable>();
            if (v) {
                v.thrower = this;
            }
            
            myball.GetComponent<Rigidbody>().useGravity = true;
            myball.GetComponent<Rigidbody>().detectCollisions = true;
            myball.GetComponent<Rigidbody>().isKinematic = false;
            myball.GetComponent<NetworkTransform>().enabled = true;
            myball.GetComponent<Collider>().isTrigger = false;

            Rpc_Thrown(myball);

            // we don't have anything to do with our ball field anymore
            heldBall = null; 
            //Apply velocity on throwing
            guide.GetChild(0).gameObject.GetComponent<Rigidbody>().velocity =
                                                new Vector3(launchX, launchY, launchZ) * speed;

            //Unparent our ball
            guide.GetChild(0).parent = null;
            
            canHold = true;
        }
     }
/*
     [Command]
     void Cmd_ChangeRigidBody(GameObject obj, bool enabled) {
        if (enabled) {
            //Set our Gravity to true again.
            obj.GetComponent<Rigidbody>().useGravity = true;
            obj.GetComponent<Rigidbody>().detectCollisions = true;
            obj.GetComponent<Rigidbody>().isKinematic = false;
        } else {
            //Set gravity to false while holding it
            obj.GetComponent<Rigidbody>().useGravity = false;
            obj.GetComponent<Rigidbody>().detectCollisions = false;
            obj.GetComponent<Rigidbody>().isKinematic = true;
        }
     }

     [Command]
     void Cmd_AssignLocalAuthority (GameObject obj) {
         NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity> ().netId;
         GameObject client = NetworkServer.FindLocalObject (nIns);
         NetworkIdentity ni = client.GetComponent<NetworkIdentity> ();
         ni.AssignClientAuthority(connectionToClient);
     }
 
     [Command]
     void Cmd_RemoveLocalAuthority (GameObject obj) {
             NetworkInstanceId nIns = obj.GetComponent<NetworkIdentity> ().netId;
         GameObject client = NetworkServer.FindLocalObject (nIns);
         NetworkIdentity ni = client.GetComponent<NetworkIdentity> ();
         ni.RemoveClientAuthority (ni.clientAuthorityOwner);
     }
     */
 }//class