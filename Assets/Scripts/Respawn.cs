using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.NetworkSystem;

public class Respawn : MonoBehaviour
{
	public GameObject rabbitPrefab;
    public GameObject NetworkManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void RespawnRabbit(GameObject rabbit){
        print("Touched");
        rabbit.GetComponent<Transform>().position = this.gameObject.GetComponent<Transform>().position;
        //Destroy(rabbit);
        //CustomNetworkManager NM = (CustomNetworkManager) NetworkManager.GetComponent(typeof(CustomNetworkManager));
        //Network.Instantiate(rabbitPrefab,this.transform.position,Quaternion.identity,0);


    }
}
