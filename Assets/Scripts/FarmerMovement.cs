using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class FarmerMovement : MonoBehaviour
{

    public Transform rightHand;
    public Transform leftHand;

    public Transform rightArm;
    public Transform leftArm;

    public Transform rightShoulder;

    public Transform leftShoulder;

    public Transform bodyTransform;

    public Transform headTransform;

    public Vector3 bodyOffset;

    // Update is called once per frame
    void Update()
    {
        if (GlobalObjects.leftAttachment != null) {
            Vector3 netPos = GlobalObjects.leftAttachment.position;
            Vector3 shovPos = GlobalObjects.rightAttachment.position;
            rightHand.position = shovPos;
            leftHand.position = netPos;

            rightArm.position = (shovPos + rightShoulder.position) / 2;
            leftArm.position = (netPos + leftShoulder.position) / 2;

            bodyTransform.position = GlobalObjects.VRLoc.position + bodyOffset;

            Vector3 euler = GlobalObjects.VRLoc.rotation.eulerAngles;
            bodyTransform.rotation = Quaternion.Euler(0, euler.y + 180, -90);
            headTransform.rotation = Quaternion.Euler(-euler.x, euler.y + 180, -euler.z - 90);
        }
    }
}
