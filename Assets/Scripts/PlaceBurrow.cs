using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;
using Sound;

public class PlaceBurrow : NetworkBehaviour
{
    public int numBurrows;

    public GameObject burrowPrefab;

    public float burrowY;

    public Image burrowProgressImage;

    public float burrowProgress;

    public bool isBurrowing;

    public Text burrowsLeftText;

    public float burrowTime;

    public HoldItems holdItems;

    // Start is called before the first frame update
    void Start()
    {
        if (this.isLocalPlayer) {
            burrowProgressImage.type = Image.Type.Filled;
            burrowsLeftText.text = "Burrows Left: " + numBurrows;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer) {
            if (isBurrowing) {
                burrowProgress += Time.deltaTime / burrowTime;
                if (burrowProgress >= 1) {
                    isBurrowing = false;
                    holdItems.fpc.canMove = true;
                    numBurrows--;
                    burrowsLeftText.text = "Burrows Left: " + numBurrows;
                    Cmd_CreateBurrow();
                }
            } else {
                burrowProgress = 0;
            }
            if (Input.GetKeyDown(KeyCode.B)) {
                if (numBurrows > 0) {
                    isBurrowing = true;
                    holdItems.fpc.canMove = false;
                    burrowProgress = 0;
                    SoundManager.S.PlayDig();
                }
            }

            burrowProgressImage.fillAmount = burrowProgress;
        }
    }

    [Command]
    void Cmd_CreateBurrow() {
        Vector3 pos = transform.position;

        GameObject newBurrow = Instantiate(burrowPrefab, new Vector3(pos.x, burrowY, pos.z), Quaternion.identity);
        NetworkServer.Spawn(newBurrow);

        //Rpc_CreateBurrow(pos.x, pos.z);
    }

    [ClientRpc]
    void Rpc_CreateBurrow(float x, float z) {
        Instantiate(burrowPrefab, new Vector3(x, burrowY, z), Quaternion.identity);
    }
}
