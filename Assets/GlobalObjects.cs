using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalObjects : MonoBehaviour
{

    public static GameObject farmerNet;

    public static GameObject farmerShovel;

    public static GameObject farmer;

    public static Transform VRLoc;
    public static Transform rightAttachment;
    public static Transform leftAttachment;

    public GameObject netObject;

    public GameObject shovelObject;

    public GameObject farmerModel;

    public GameObject vrObject;

    public GameObject rhObject;

    public GameObject lhObject;

    // Start is called before the first frame update
    void Start()
    {
        farmerNet = netObject;
        farmerShovel = shovelObject;
        farmer = farmerModel;
        leftAttachment = lhObject.transform;
        rightAttachment = rhObject.transform;
        VRLoc = vrObject.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
