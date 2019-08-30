using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class DestroyBurrow : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void destroyBurrow(Transform b){
        Cmd_destroyBurrow(b.position);
    }
    [Command]
	public void Cmd_destroyBurrow(Vector3 b){
		Rpc_destroyBurrow(b);
	}
	[ClientRpc]
	void Rpc_destroyBurrow(Vector3 b){
		Collider[] colliders;
        if ((colliders = Physics.OverlapSphere(b, 0.2f)).Length > 0) {
            foreach (var collider in colliders) {
                var go = collider.gameObject;
                if (go.tag == "burrow") {
                    Destroy(go);
                }
            }
        }
	}
}
