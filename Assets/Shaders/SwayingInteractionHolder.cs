using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class SwayingInteractionHolder : MonoBehaviour {
    [SerializeField]
    GameObject[] objects;
    Vector4[] positions = new Vector4[100];
   
    void Start() 
    {
        objects = GameObject.FindGameObjectsWithTag("pushesGrash");
        InvokeRepeating("populateList", 0f, 10f);
        // calls populateList to update list every 10 seconds, needs to be changed
    }

    void populateList()
    {
        objects = GameObject.FindGameObjectsWithTag("pushesGrash");
    }

    void Update () {
        if (objects != null)
        {
            for (int i = 0; i < objects.Length && i < positions.Length; i++) // fix later
            {
                if (positions[i] != null && objects[i] != null){
                    positions[i] = objects[i].transform.position;
                }
            }
        }
        Shader.SetGlobalFloat("_PositionArray", objects.Length);
        Shader.SetGlobalVectorArray("_Positions", positions);
       
    }
}