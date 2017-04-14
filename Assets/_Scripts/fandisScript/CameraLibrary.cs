using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraLibrary : MonoBehaviour 
{
    // An integer that holds the information of what the current mode is
    //1 is mobile touch
    //2 is mobile vr
    //3 is mobile gyro
    //4 is google vr
    public int modeID; 

    // A tag name for wall 
    public string wallTag;
	
	// Front and back collider
	public CameraCollider frontCollider;
	public CameraCollider backCollider;

	// Text to know what movement next, going to the front or going to the back	
	public Text nextMoveText;
	
	// Text to know what mode it is currently
	public Text currentModeIDText;
	
    //to know whether player is clicking the ui button or the screen
    private bool isClickUIButton;

    [System.Serializable]
    // A class that manages interactions for mobile touch mode
    public class MobileTouch     
    {
        //to store how fast the camera will rotate
        public float rotationSpeed;
        //to store how fast the camera will translate forward and backward
        public float translationSpeed;
        //to store the max zoomed in value of the camera
        public float minFieldOfView;
        //to store the max zoomed out value of the camera
        public float maxFieldOfView;

        //to know whether the camera is rotating, translating, or in idle status
        //1 is idle
        //2 is rotating
        //3 is translating 
        public int statusID;
        //to store the threshold value of when it should be considered as rotation or still idle (in pixels)
        public int idleToRotationXThreshold;

        //to know whether the screen is being touched or not
        public bool isScreenTouched;
        
        //to store the screen coordinate position where the screen is firstly touched
        [HideInInspector] public Vector3 touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);
        //to store the prev frame mouse position 
        [HideInInspector] public Vector3 prevFrameTouchPos;

        //to store what kind of translation should be done next
        //"F" is forward
        //"S" is stop
        //"B" is backward
        [HideInInspector] public string[] translationArray = new string[4] {"S", "F", "S", "B"};
        [HideInInspector] public int currentIdxInTranslationArray = 0;
    }

    [System.Serializable]
    public class MobileGyro     
    {
        //to store how fast the camera will rotate
        public float rotationSpeed;
        //to store how fast the camera will translate forward and backward
        public float translationSpeed;
        //to store the max zoomed in value of the camera
        public float minFieldOfView;
        //to store the max zoomed out value of the camera
        public float maxFieldOfView;

        //to know whether the camera is translating, or in idle status
        //1 is idle
        //3 is translating 
        public int statusID;
        
        //to know whether the screen is being touched or not
        public bool isScreenTouched;
        
        //to store the screen coordinate position where the screen is firstly touched
        [HideInInspector] public Vector3 touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);
        //to store the prev frame mouse position 
        //[HideInInspector] public Vector3 prevFrameTouchPos;

        //to store what kind of translation should be done next
        //"F" is forward
        //"S" is stop
        //"B" is backward
        [HideInInspector] public string[] translationArray = new string[4] {"S", "F", "S", "B"};
        [HideInInspector] public int currentIdxInTranslationArray = 0;

        //to store to which direction the camera should translate
        //[HideInInspector] public Vector3 directionToTranslate;
    }

    //an instance of each class
    public MobileTouch mobileTouch = new MobileTouch();
    public MobileGyro mobileGyro = new MobileGyro();

	// Use this for initialization
	void Start () 
    {
        //enable the gyro
		Input.gyro.enabled = true;

        //set what mode it is right now text
        currentModeIDText.text = "" + modeID;
	}
	
	// Update is called once per frame
	void Update () 
    {
        #region MOBILE_TOUCH_UPDATE
		//if mobile touch
        if (modeID == 1)
        {
            //if in idle status
            if (mobileTouch.statusID == 1)
            {
                //when the screen is touched
                if (Input.GetMouseButtonDown(0))
                {
                    //store the first screen pos
                    mobileTouch.touchStartScreenPos = Input.mousePosition;

                    //store to the variable
                    mobileTouch.isScreenTouched = true;
                }

                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileTouch.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileTouch.isScreenTouched = false;

                    //go to translation status
                    mobileTouch.statusID = 3;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileTouch.currentIdxInTranslationArray += 1;
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileTouch.currentIdxInTranslationArray = 0;
                    }
                }

                //when the screen is being hold
                if (Input.GetMouseButton(0))
                {
                    //if it's been moved for more than a certain pixels
                    if (Mathf.Abs(Input.mousePosition.x - mobileTouch.touchStartScreenPos.x) > mobileTouch.idleToRotationXThreshold)
                    {
                        //goes to rotation status
                        mobileTouch.statusID = 2;
                    }
                }
            }
            else
            //if in rotation status
            if (mobileTouch.statusID == 2)
            {
                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileTouch.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileTouch.isScreenTouched = false;

                    //go to idle status
                    mobileTouch.statusID = 1;
                }

                //when the screen is being hold
                if (Input.GetMouseButton(0))
                {
                    //rotate the thread
                    float xDelta = Input.mousePosition.x - mobileTouch.prevFrameTouchPos.x;
					float yDelta = Input.mousePosition.y - mobileTouch.prevFrameTouchPos.y;
				
                    float xVelocity = -xDelta * mobileTouch.rotationSpeed * 0.1f;
					float yVelocity = -yDelta * mobileTouch.rotationSpeed * 0.1f;

					//rotate the camera
					Camera.main.transform.parent.eulerAngles += new Vector3(-yVelocity, xVelocity, 0f);
					
                    //Camera.main.transform.Rotate (yVelocity, xVelocity, 0f);
                }
            }
            else
            //if in translation status
            if (mobileTouch.statusID == 3)
            {
				//if it's time to move forward
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "F" && !frontCollider.isHittingWall)
                {
                    //move forward relative to the rotation of the camera
                    Camera.main.transform.parent.position += Camera.main.transform.parent.forward * mobileTouch.translationSpeed * Time.deltaTime;
                }
                else
                //if it's time to move backward
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "B" && !backCollider.isHittingWall)
                {
                    //move negative forward relative to the rotation of the camera
                    Camera.main.transform.parent.position -= Camera.main.transform.parent.forward * mobileTouch.translationSpeed * Time.deltaTime;
                }
				
				//if there's something blocking on the front
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "F" && frontCollider.isHittingWall)
                {
					//go to idle status
                    mobileTouch.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileTouch.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileTouch.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
				}
				
				//if there's something blocking on the back
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "B" && backCollider.isHittingWall)
                {
					//go to idle status
                    mobileTouch.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileTouch.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileTouch.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
				}

                //when the screen is touched
                if (Input.GetMouseButtonDown(0))
                {
                    //store the first screen pos
                    mobileTouch.touchStartScreenPos = Input.mousePosition;

                    //store to the variable
                    mobileTouch.isScreenTouched = true;
                }

                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileTouch.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileTouch.isScreenTouched = false;

                    //go to idle status
                    mobileTouch.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileTouch.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileTouch.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
                }
				
				/*
                //if it's time to move forward
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "F")
                {
                    //if it exceed the min value
                    if (Camera.main.fieldOfView <= mobileTouch.minFieldOfView)
                    {
                        //clamp
                        Camera.main.fieldOfView = mobileTouch.minFieldOfView;

                        //go to idle status
                        mobileTouch.statusID = 1;
    
                        //check if it exceeds the length of the array or not
                        //if not
                        if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                        {
                            //increase the index
                            mobileTouch.currentIdxInTranslationArray += 1;
                        }
                        //otherwise
                        else
                        {
                            //reset back to zero
                            mobileTouch.currentIdxInTranslationArray = 0;
                        }
                    }
                    else
                    {
                        //decrease the field of view
                        Camera.main.fieldOfView -= mobileTouch.translationSpeed * Time.deltaTime;
                    }
                }
                else
                //if it's time to move backward
                if (mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray] == "B")
                {
                    //if it exceed the max value
                    if (Camera.main.fieldOfView >= mobileTouch.maxFieldOfView)
                    {
                        //clamp
                        Camera.main.fieldOfView = mobileTouch.maxFieldOfView;

                        //go to idle status
                        mobileTouch.statusID = 1;
    
                        //check if it exceeds the length of the array or not
                        //if not
                        if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                        {
                            //increase the index
                            mobileTouch.currentIdxInTranslationArray += 1;
                        }
                        //otherwise
                        else
                        {
                            //reset back to zero
                            mobileTouch.currentIdxInTranslationArray = 0;
                        }
                    }
                    else
                    {
                        //increase the field of view
                        Camera.main.fieldOfView += mobileTouch.translationSpeed * Time.deltaTime;
                    }
                }

                //when the screen is touched
                if (Input.GetMouseButtonDown(0))
                {
                    //store the first screen pos
                    mobileTouch.touchStartScreenPos = Input.mousePosition;

                    //store to the variable
                    mobileTouch.isScreenTouched = true;
                }

                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileTouch.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileTouch.isScreenTouched = false;

                    //go to idle status
                    mobileTouch.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileTouch.currentIdxInTranslationArray += 1;
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileTouch.currentIdxInTranslationArray = 0;
                    }
                }
				*/
			}
        
			//store the mouse position to be accessed in the next frame
            mobileTouch.prevFrameTouchPos = Input.mousePosition;

			//check if it exceeds the length of the array or not
            //if not
            if (mobileTouch.currentIdxInTranslationArray != mobileTouch.translationArray.Length - 1)
            {
				//update the next move text
				nextMoveText.text = "" + mobileTouch.translationArray[mobileTouch.currentIdxInTranslationArray + 1];
            }
            //otherwise
            else
            {
                //update the next move text
				nextMoveText.text = "" + mobileTouch.translationArray[0];
            }
		}
        #endregion

        #region MOBILE_GYRO_UPDATE
        //if mobile gyro
        if (modeID == 3)
        {
            //if in idle status
            if (mobileGyro.statusID == 1)
            {
                //when the screen is touched
                if (Input.GetMouseButtonDown(0))
                {
                    //store the first screen pos
                    mobileGyro.touchStartScreenPos = Input.mousePosition;

                    //store to the variable
                    mobileGyro.isScreenTouched = true;
                }

                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileGyro.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileGyro.isScreenTouched = false;

                    //save the direction to translate
                    //mobileGyro.directionToTranslate = Camera.main.transform.forward;
    
                    //go to translation status
                    mobileGyro.statusID = 3;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileGyro.currentIdxInTranslationArray += 1;
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileGyro.currentIdxInTranslationArray = 0;
                    }
                }
            }
            else
            //if in translation status
            if (mobileGyro.statusID == 3)
            {
                //if it's time to move forward
                if (mobileGyro.translationArray[mobileGyro.currentIdxInTranslationArray] == "F" && !frontCollider.isHittingWall)
                {
                    //move forward relative to the rotation of the camera
                    Camera.main.transform.parent.position += Camera.main.transform.parent.forward * mobileGyro.translationSpeed * Time.deltaTime;

                    //if it exceed the min value
                    /*if (Camera.main.fieldOfView <= mobileGyro.minFieldOfView)
                    {
                        //clamp
                        Camera.main.fieldOfView = mobileGyro.minFieldOfView;

                        //go to idle status
                        mobileGyro.statusID = 1;
    
                        //check if it exceeds the length of the array or not
                        //if not
                        if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                        {
                            //increase the index
                            mobileGyro.currentIdxInTranslationArray += 1;
                        }
                        //otherwise
                        else
                        {
                            //reset back to zero
                            mobileGyro.currentIdxInTranslationArray = 0;
                        }
                    }
                    else
                    {
                        //decrease the field of view
                        Camera.main.fieldOfView -= mobileGyro.translationSpeed * Time.deltaTime;
                    }*/
                }
                else
                //if it's time to move backward
                if (mobileGyro.translationArray[mobileGyro.currentIdxInTranslationArray] == "B" && !backCollider.isHittingWall)
                {
                    //move negative forward relative to the rotation of the camera
                    Camera.main.transform.parent.position -= Camera.main.transform.parent.forward * mobileGyro.translationSpeed * Time.deltaTime;

                    //if it exceed the max value
                    /*if (Camera.main.fieldOfView >= mobileGyro.maxFieldOfView)
                    {
                        //clamp
                        Camera.main.fieldOfView = mobileGyro.maxFieldOfView;

                        //go to idle status
                        mobileGyro.statusID = 1;
    
                        //check if it exceeds the length of the array or not
                        //if not
                        if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                        {
                            //increase the index
                            mobileGyro.currentIdxInTranslationArray += 1;
                        }
                        //otherwise
                        else
                        {
                            //reset back to zero
                            mobileGyro.currentIdxInTranslationArray = 0;
                        }
                    }
                    else
                    {
                        //increase the field of view
                        Camera.main.fieldOfView += mobileGyro.translationSpeed * Time.deltaTime;
                    }*/
                }
				
				//if there's something blocking on the front
                if (mobileGyro.translationArray[mobileGyro.currentIdxInTranslationArray] == "F" && frontCollider.isHittingWall)
                {
					//go to idle status
                    mobileGyro.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileGyro.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileGyro.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
				}
				
				//if there's something blocking on the back
                if (mobileGyro.translationArray[mobileGyro.currentIdxInTranslationArray] == "B" && backCollider.isHittingWall)
                {
					//go to idle status
                    mobileGyro.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileGyro.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileGyro.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
				}

                //when the screen is touched
                if (Input.GetMouseButtonDown(0))
                {
                    //store the first screen pos
                    mobileGyro.touchStartScreenPos = Input.mousePosition;

                    //store to the variable
                    mobileGyro.isScreenTouched = true;
                }

                //when the screen is released
                if (Input.GetMouseButtonUp(0))
                {
                    //reset the first screen pos
                    mobileGyro.touchStartScreenPos = new Vector3(-10000f, -10000f, -10000f);

                    //mark the bool
                    mobileGyro.isScreenTouched = false;

                    //go to idle status
                    mobileGyro.statusID = 1;

                    //check if it exceeds the length of the array or not
                    //if not
                    if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
                    {
                        //increase the index
                        mobileGyro.currentIdxInTranslationArray += 1;
						
						Debug.Log("Increase by 1");
                    }
                    //otherwise
                    else
                    {
                        //reset back to zero
                        mobileGyro.currentIdxInTranslationArray = 0;
						
						Debug.Log("Back to 0");
                    }
                }
            }

            //rotate the camera
            Camera.main.transform.parent.eulerAngles += new Vector3(-Input.gyro.rotationRateUnbiased.x * mobileGyro.rotationSpeed, -Input.gyro.rotationRateUnbiased.y * mobileGyro.rotationSpeed, 0f);
            //Camera.main.transform.Rotate (-Input.gyro.rotationRateUnbiased.x * mobileGyro.rotationSpeed, -Input.gyro.rotationRateUnbiased.y * mobileGyro.rotationSpeed, 0f);
            //Camera.main.transform.eulerAngles += new Vector3(-Input.GetAxis("Vertical") * mobileGyro.rotationSpeed, Input.GetAxis("Horizontal") * mobileGyro.rotationSpeed, 0f);

			//check if it exceeds the length of the array or not
            //if not
            if (mobileGyro.currentIdxInTranslationArray != mobileGyro.translationArray.Length - 1)
            {
				//update the next move text
				nextMoveText.text = "" + mobileGyro.translationArray[mobileGyro.currentIdxInTranslationArray + 1];
            }
            //otherwise
            else
            {
                //update the next move text
				nextMoveText.text = "" + mobileGyro.translationArray[0];
            }
        }
        #endregion
	}
	
    public void OnToggleButtonClicked (Text text)
    {
        //if mobile touch
        if (modeID == 1)
        {
            //go to mobile vr
            modeID = 2;
        }
        else
        //if mobile VR
        if (modeID == 2)
        {
            //go to mobile gyro
            modeID = 3;
        }
        else
        //if mobile gyro
        if (modeID == 3)
        {
            //go to google vr
            modeID = 4;
        }
        else
        //if google VR
        if (modeID == 4)
        {
            //go to mobile touch
            modeID = 1;
        }

        //change the text
        text.text = "" + modeID;
    }
}
