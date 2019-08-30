 using UnityEngine;
 using System.Collections;
 
 public class rotObj : MonoBehaviour {
 
 	public float idleSpeed = 0.1F;
     public float rotationSpeed = 10.0F;
     public float lerpSpeed = 1.0F;
     public Transform t;
 
     private Vector3 theSpeed;
     private Vector3 avgSpeed;
     private bool isDragging = false;
     private bool unclicked = true;
     private int unclickedTimer;
     private Vector3 targetSpeedX;

     void OnMouseDown() {
         isDragging = true;
         unclicked = false;
     }
 
     void Update() {

     	if (unclicked)
     	{
     		theSpeed = new Vector3(-idleSpeed, 0, 0);
     	}

        else if (Input.GetMouseButton(0) && isDragging) {
             theSpeed = new Vector3(-Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"), 0.0F);
             avgSpeed = Vector3.Lerp(avgSpeed, theSpeed, Time.deltaTime * 5);
             unclickedTimer = 0;
         }
        else {
             if (isDragging) {
                 theSpeed = avgSpeed;
                 isDragging = false;
             }
             float i = Time.deltaTime * lerpSpeed;
             theSpeed = Vector3.Lerp(theSpeed, Vector3.zero, i);
             unclickedTimer++;
         }

         if (unclickedTimer >= 120)
		  {
		    Vector3 goalSpeed = new Vector3(-idleSpeed, 0, 0);
		    float i = Time.deltaTime * lerpSpeed;
            theSpeed = Vector3.Lerp(theSpeed, goalSpeed, i);
		  }
 
 		 transform.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
         transform.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
         transform.eulerAngles = new Vector3(
		    0,
		    transform.eulerAngles.y,
		    transform.eulerAngles.z
		);


         t.Rotate(Camera.main.transform.up * theSpeed.x * rotationSpeed, Space.World);
         t.Rotate(Camera.main.transform.right * theSpeed.y * rotationSpeed, Space.World);
         t.eulerAngles = new Vector3(
		    0,
		    t.eulerAngles.y,
		    t.eulerAngles.z
		);
     }
 }