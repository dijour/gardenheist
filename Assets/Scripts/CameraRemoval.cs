using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraRemoval : NetworkBehaviour
{
    public GameObject[] cameraObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.isLocalPlayer) {
            foreach(GameObject obj in cameraObjects) {
                //print("HELLO");
                Destroy(obj);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
