using UnityEngine;
using System.Collections;

public class MobileCameraControl : MonoBehaviour 
{
    public float rotationSpeed; //how fast the thread needs to spin
    public float verticalTranslationSpeed; //how fast the thread needs to move vertically

    public float yMaxHeight; //to know what's the maximum height the thread can achieve
    public float yMinHeight; //to know what's the minimum height the thread can achieve

    public float yFriction;
    public float xFriction;

    public GameObject thread; //to store the thing that we want to move vertically and spin

    private Vector3 mouseStartPosition; //to store the value of mouse position when it's pressed down
    //private Vector3 mouseEndPosition; //to store the value of mouse position when it's released
   
    private Vector3 mouseDeltaPosition;

    private bool isDown;    
    private bool isMomentumActivated;

    private float yVelocity;
    private float xVelocity;

    //private Vector3 mouseStartTranslatePos;
    //private Vector3 mouseStartRotatePos;
 
    //private Vector3 initThreadPosition;

    //private bool isTranslate;
   
	// Use this for initialization
	void Start () 
    {
	    //initialize
        //initThreadPosition = thread.transform.position;
	}
	
	// Update is called once per frame
	void Update () 
    {
        //when mouse is pressed down
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down!");
            mouseStartPosition = Input.mousePosition;
            //mouseStartTranslatePos = Input.mousePosition;
            //mouseStartRotatePos = Input.mousePosition;
            isDown = true;
            isMomentumActivated = false;

            yVelocity = 0f;
            xVelocity = 0f;
        }

        //when it's hold
        if (Input.GetMouseButton(0))
        {
            //calculate the delta 
            /*float yDelta = Input.mousePosition.y - mouseStartPosition.y;

            Debug.Log("Y Mouse Position: " + Input.mousePosition.y);
            Debug.Log("Mouse Hold! " + yDelta);

            //move the thread vertically
            thread.transform.position = initThreadPosition - new Vector3(0f, yDelta * Time.deltaTime * verticalTranslationSpeed, 0f);

            if (thread.transform.position.y >= yMaxHeight)
            {
                thread.transform.position = new Vector3(thread.transform.position.x, yMaxHeight, thread.transform.position.z);
            }

            if (thread.transform.position.y <= yMinHeight)
            {
                thread.transform.position = new Vector3(thread.transform.position.x, yMinHeight, thread.transform.position.z);
            }*/

               
            if (isDown)
            {   
                mouseDeltaPosition = Input.mousePosition - mouseStartPosition;

                if (Mathf.Abs(mouseDeltaPosition.x) > 0f || Mathf.Abs(mouseDeltaPosition.y) > 0f)
                {
                    isDown = false;
                }
            }

            if (!isDown)
            {
                //if move vertically
                if (Mathf.Abs(mouseDeltaPosition.y) > Mathf.Abs(mouseDeltaPosition.x))
                {
                    //move the thread
                    float yDelta = Input.mousePosition.y - mouseStartPosition.y;
                
                    yVelocity = yDelta * verticalTranslationSpeed * 0.01f;

                    //Debug.Log("yVelocity: " + yVelocity);

                    if (yVelocity >= 0.15f)
                    {
                        yVelocity = 0.15f; 
                    }
                    else
                    if (yVelocity <= -0.15f)
                    {
                        yVelocity = -0.15f; 
                    }

                    thread.transform.Translate (0f, yVelocity, 0f);
                
                    mouseStartPosition = Input.mousePosition;
        
                    if (thread.transform.position.y >= yMaxHeight)
                    {
                        thread.transform.position = new Vector3(thread.transform.position.x, yMaxHeight, thread.transform.position.z);
                    }
        
                    if (thread.transform.position.y <= yMinHeight)
                    {
                        thread.transform.position = new Vector3(thread.transform.position.x, yMinHeight, thread.transform.position.z);
                    }
                }
                else
                //if rotate
                if (Mathf.Abs(mouseDeltaPosition.x) > Mathf.Abs(mouseDeltaPosition.y))
                {
                    //rotate the thread
                    float xDelta = Input.mousePosition.x - mouseStartPosition.x;
                
                    xVelocity = -xDelta * rotationSpeed * 0.01f;

                    thread.transform.Rotate (0f, xVelocity, 0f);
                
                    mouseStartPosition = Input.mousePosition;
                }
            }
        }

        //when mouse is pressed up
        if (Input.GetMouseButtonUp(0))
        {
            //mouseEndPosition = Input.mousePosition;

            /*float xDelta = mouseEndPosition.x - mouseStartPosition.x;

            Debug.Log("Mouse Up!" + xDelta);*/

            isMomentumActivated = true;
        }

        if (isMomentumActivated)
        {
            //if move vertically
            if (Mathf.Abs(mouseDeltaPosition.y) > Mathf.Abs(mouseDeltaPosition.x))
            {
                yVelocity *= Mathf.Pow(yFriction, Time.deltaTime);
    
                thread.transform.Translate (0f, yVelocity, 0f);
    
                if (thread.transform.position.y >= yMaxHeight)
                {
                    thread.transform.position = new Vector3(thread.transform.position.x, yMaxHeight, thread.transform.position.z);
                }
            
                if (thread.transform.position.y <= yMinHeight)
                {
                    thread.transform.position = new Vector3(thread.transform.position.x, yMinHeight, thread.transform.position.z);
                }
            }
            else
            //if rotate
            if (Mathf.Abs(mouseDeltaPosition.x) > Mathf.Abs(mouseDeltaPosition.y))
            {
                xVelocity *= Mathf.Pow(xFriction, Time.deltaTime);
                
                thread.transform.Rotate (0f, xVelocity, 0f);
            }
        }
	}
}
