using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkAuthority : NetworkBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        print(this.isLocalPlayer);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
