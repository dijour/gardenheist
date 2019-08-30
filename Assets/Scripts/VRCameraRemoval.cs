using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class VRCameraRemoval : NetworkBehaviour
{
    public GameObject cameraObject;
    public GameObject audioObject;
    public GameObject fallbackObjects;

    // Start is called before the first frame update
    void Start()
    {
        if (!this.isLocalPlayer) {
            cameraObject.GetComponent<Camera>().enabled = false;
            audioObject.GetComponent<AudioListener>().enabled = false;
            Destroy(fallbackObjects);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
