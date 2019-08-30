using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scarecrow : MonoBehaviour
{
    float count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count += Time.deltaTime;
        if ((count % 2>= 0) && (count % 2 <= 1)){
            Vector3 v = Vector3.left *  Time.deltaTime;
            v = new Vector3((float)(v.x * 0.3) , v.y, v.z);
            transform.Translate(v);
        }
        else{
            Vector3 v = -Vector3.left *  Time.deltaTime;
            v = new Vector3((float)(v.x * 0.3) , v.y, v.z);
            transform.Translate(v);
        }

        
        
    }
}
