using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour 
{
	public bool isHittingWall; //to know whether the collider is hitting the wall or not
	
	private CameraLibrary cameraLibrary;

	// Use this for initialization
	void Start () 
	{
		//assign the cameraLibrary variable with CameraLibrary script
		cameraLibrary = this.gameObject.transform.parent.gameObject.GetComponent<CameraLibrary>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	void OnTriggerExit (Collider other)
    {
		Debug.Log("Trigger exit!");
		
        //if this is a wall
        if (other.gameObject.tag == cameraLibrary.wallTag)
        {
			//set to false
			isHittingWall = false;
		}
	}
	
	void OnTriggerEnter (Collider other)
    {
		Debug.Log("Trigger enter!");
		
        //if this is a wall
        if (other.gameObject.tag == cameraLibrary.wallTag)
        {
			//set to true
			isHittingWall = true;
			
            //if mobile gyro
            if (cameraLibrary.modeID == 3)
            {
                //if in translation status
                if (cameraLibrary.mobileGyro.statusID == 3)
                {
					Debug.Log("Hitting the wall while moving. Make it idle!");
					
                    //stop the camera //go to idle
                    cameraLibrary.mobileGyro.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (cameraLibrary.mobileGyro.currentIdxInTranslationArray != cameraLibrary.mobileGyro.translationArray.Length - 1)
                    {
                        //increase the index
                        cameraLibrary.mobileGyro.currentIdxInTranslationArray += 1;
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        cameraLibrary.mobileGyro.currentIdxInTranslationArray = 0;
                    }
                }
            }
        }
    }
}
