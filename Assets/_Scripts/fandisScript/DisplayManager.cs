using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Holoville.HOTween;
using Holoville.HOTween.Core;


public class DisplayManager : MonoBehaviour 
{
    public static DisplayManager instance;

    public TextMesh text; //to store which text mesh we will display the text to

    public bool isTouch; //detecting whether our finger is touching the screen or not
    public bool isHold; //dtecting whether we are holding the screen or not

    public bool isTimeToChange; //detecting whether is it already a time to change to the next text

    public int currentIdx; //which idx of the words array we are currently at

    private float currentTimeForHold; 
    private float maxTimeForHold = 0.25f; //if we do not release the screen for this certain amount of time, it will be detected as hold
    private float currentTimeForTransition;   
    private float maxTimeForTransition = 0.25f; //if we hold the screen, than the text will be changed every 0.5 seconds

    private Vector3 clickedMousePos;

	// Use this for initialization
    void Awake ()
    {
        instance = this;
    }

	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
        //update the value of text
        text.text = ParserManager.instance.words[currentIdx];

        //check touch
        if (Input.GetMouseButtonDown(0))
        {
            isTouch = true;

            //set the mouse pos
            clickedMousePos = Input.mousePosition;
        }

        //when we are touching
        if (isTouch)
        {
            //increase the time
            currentTimeForHold += Time.deltaTime;

            //when it passes the threshold
            if (currentTimeForHold >= maxTimeForHold)
            {
                isTouch = false;
                isHold = true;
            }
        }

        if (isHold)
        {
            //increase the time
            currentTimeForTransition += Time.deltaTime;
    
            //when it passes the threshold
            if (currentTimeForTransition >= maxTimeForTransition)
            {
                isTimeToChange = true;
                currentTimeForTransition = 0f;
            }

            if (isTimeToChange)
            {
                //increase the index by one
                if (!IsMax() || !IsMin())
                {
                    if (GetNavigation() == 1 && !IsMax())
                    {
                        currentIdx += 1;
                    }
                    else
                    if (GetNavigation() == 2 && !IsMin())
                    {
                        currentIdx -= 1;
                    }
                }
                isTimeToChange = false;
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            //if we release the screen while not holding (tapping)
            if (isTouch)
            {
                //increase the index by one
                if (!IsMax() || !IsMin())
                {
                    if (GetNavigation() == 1 && !IsMax())
                    {
                        currentIdx += 1;
                    }
                    else
                    if (GetNavigation() == 2 && !IsMin())
                    {
                        currentIdx -= 1;
                    }
                }
            }

            isTouch = false;
            isHold = false;
            isTimeToChange = false;

            currentTimeForHold = 0f;
            currentTimeForTransition = 0f;

            clickedMousePos = Vector3.zero; 
        }
	}   

    //to check whether it's already the max index in list or not
    public bool IsMax ()
    {
        if (currentIdx == ParserManager.instance.words.Count - 1)
        {
            return true;
        }

        return false;
    }
    
    //to check whether it's already the min index in list or not
    public bool IsMin ()
    {
        if (currentIdx == 0)
        {
            return true;
        }

        return false;
    }


    //to check whether we touch the screen on the right or left part. 1 if right, 2 if left
    public int GetNavigation ()
    {
        //if on the right side
        if (clickedMousePos.x >= (Screen.width * 1f) / 2f)
        {
            return 1;
        }
        else
        {
            return 2;
        }
    }
}
