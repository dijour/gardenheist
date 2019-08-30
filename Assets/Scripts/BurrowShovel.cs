using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Sound;

public class BurrowShovel : NetworkBehaviour
{


	public int Health = 5;
	public GameObject scoreS;
    // Start is called before the first frame update
    void Start()
    {
		//scoreS = GameObject.Find("ScoringSystem");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "shovel"){
			print("Shoveled Once");
			Health -= 1;
            SoundManager.S.PlayDig();
			if (Health <= 0){
				Cmd_destroyBurrow(gameObject);
				//DestroyBurrow db = (DestroyBurrow) scoreS.GetComponent(typeof(DestroyBurrow));
				//db.destroyBurrow(this.gameObject.transform);
			}
			
			//UnityStandardAssets.Characters.FirstPerson.FirstPersonController fpc = (UnityStandardAssets.Characters.FirstPerson.FirstPersonController) this.gameObject.GetComponent(typeof(UnityStandardAssets.Characters.FirstPerson.FirstPersonController));
			//forceDrop();
			//fpc.Cmd_setCanMove();
		
		}
	}

	[Command]
	public void Cmd_destroyBurrow(GameObject b){
		Rpc_destroyBurrow(b);
	}
	[ClientRpc]
	void Rpc_destroyBurrow(GameObject b){
		Destroy(b);
	}

}
