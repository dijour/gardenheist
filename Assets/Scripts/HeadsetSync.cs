using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class HeadsetSync : NetworkBehaviour
{

    public Transform headset;
    public Transform leftHand;
    public Transform rightHand;

    // Update is called once per frame
    void Update()
    {
        if (this.isLocalPlayer) {
            GlobalObjects.VRLoc.position = headset.position;
            GlobalObjects.VRLoc.rotation = headset.rotation;
            GlobalObjects.rightAttachment.position = rightHand.position;
            GlobalObjects.rightAttachment.rotation = rightHand.rotation;
            GlobalObjects.leftAttachment.position = leftHand.position;
            GlobalObjects.leftAttachment.rotation = leftHand.rotation;
        }
    }
}
