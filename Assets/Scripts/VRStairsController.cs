using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRStairsController : MonoBehaviour
{
    public float stairHeight;
    public float starterHeight;

    public float stairTriggerZ = -100;

     void OnTriggerStay(Collider other)
     {
         if (other.name == "HeadCollider")
         {
            GameObject farmerTransform = GameObject.FindWithTag("farmerTransform");
            if (starterHeight == 0)
            {
                starterHeight = farmerTransform.transform.position.y;
            }
            if (stairTriggerZ == -100) //dummy value
            {
                stairTriggerZ = other.transform.position.z;
            }
            /* Debug.Log(other.transform.position.x + ", " +
                      other.transform.position.y + ", " +
                      other.transform.position.z);*/
            Vector3 temp = farmerTransform.transform.position;
            float zConstant = other.transform.position.z-stairTriggerZ; // stairTriggerZ is negative
            //Debug.Log("z="+zConstant);
            temp.y = starterHeight + (stairHeight)*(zConstant);
            farmerTransform.transform.position = temp;
            
         }
     }

     void OnTriggerExit(Collider other)
     {
         if (other.name == "HeadCollider")
         {
            GameObject farmerTransform = GameObject.FindWithTag("farmerTransform");
            Vector3 temp = farmerTransform.transform.position;
            farmerTransform.transform.position = temp;
            StartCoroutine("FakeGravity");
            stairTriggerZ = -100;
            
         }
     }

     private IEnumerator FakeGravity()
     {
         GameObject farmerTransform = GameObject.FindWithTag("farmerTransform");
            Vector3 temp = farmerTransform.transform.position;
        Debug.Log(temp.y + " vs " + starterHeight);
         while (temp.y > starterHeight)
         {
            Debug.Log(temp.y + " vs2 " + starterHeight);
            yield return new WaitForSeconds(0.01f);
            temp.y -= 0.015f;

            farmerTransform.transform.position = temp;
         }
         temp.y = starterHeight;
     }
}
