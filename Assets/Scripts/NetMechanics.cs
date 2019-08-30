using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetMechanics : NetworkBehaviour
{
    private GameObject globalNet;
    private GameObject globalShovel;

    public GameObject localNet;

    public GameObject localShovel;

    void OnEnable()
    {
        globalNet = GlobalObjects.farmerNet;
        globalShovel = GlobalObjects.farmerShovel;
        /*DisableRendering dr = globalNet.GetComponent<DisableRendering>();
        if (dr != null) {
            dr.SetRendererEnabled(false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer) {
            globalNet.transform.position = localNet.transform.position;
            globalNet.transform.rotation = localNet.transform.rotation;
            
            globalShovel.transform.position = localShovel.transform.position;
            globalShovel.transform.rotation = localShovel.transform.rotation;
            GlobalObjects.farmer.SetActive(false);
        }
    }
    
}
