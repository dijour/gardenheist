using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider col){
        //print("HereB");
        if (col.gameObject.tag == "rabbit"){
            //print("HereA");
            HoldItems hold = (HoldItems) col.gameObject.GetComponent<HoldItems>();
            if (hold.isLocal){
                this.gameObject.GetComponent<Renderer>().enabled = false;
            }
            //co.a = 0f;
        }
    }
    void OnTriggerExit(Collider col){
        if (col.gameObject.tag == "rabbit"){
            //print("HereA");
            HoldItems hold = (HoldItems) col.gameObject.GetComponent<HoldItems>();
            if (hold.isLocal){
                this.gameObject.GetComponent<Renderer>().enabled = true;
            }
            //this.gameObject.GetComponent<Renderer>().enabled = true;
            //co.a = 0f;
        }
    }
}
