using UnityEngine;
using System.Collections;
using System;

public class mobileCamera : MonoBehaviour 
{
    public float yspeed = 10f;
    public float rotSpeed = 1000f;
    public Transform thread;

    void Awake () 
	{
        //trackedObj = GetComponent<SteamVR_TrackedObject>();
	}
	
	void FixedUpdate () {
        /*device = SteamVR_Controller.Input((int)trackedObj.index);

        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Touch' on the Trigger");
        }

        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated TouchDown on the Trigger");
        }

        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated TouchUp on the Trigger");
        }

        if (device.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You are holding 'Press' on the Trigger");
        }

        if (device.GetPressDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated PressDown on the Trigger");
        }

        if (device.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated PressUp on the Trigger");
        }*/

        //if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        //{
        //    Debug.Log("You activated PressUp on the Touchpad");
        //    sphere.transform.position = Vector3.zero;
        //    sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
        //    sphere.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        //}


        /*if (device.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            Debug.Log("You activated PressUp on the Touchpad");
            Debug.Log("GetAxis");
            //TRY for const movement
            // thread.transform.position = thread.transform.position + thread.transform.Translate(new Vector3(0.0f, yspeed * Time.deltaTime, 0.0f));
            thread.transform.Translate(new Vector3(0.0f, yspeed * Time.deltaTime, 0.0f));
            //thread.transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);
        }


        if (device.GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You activated PressUp on the Touchpad");
            print ( "Yo");
            // thread.transform.position.y = thread.transform.position.(y+5);
            thread.transform.Rotate(new Vector3(0.0f, rotSpeed * Time.deltaTime, 0.0f));
            //thread.transform.position = new Vector3(transform.position.x, 5.0f, transform.position.z);
        }*/



    }


    // On Collision with Vive Controller /
    // On Touch
    /*void OnTriggerStay(Collider col)
    {
        Vector3 paddle = col.gameObject.transform.position;
        Debug.Log("You have collided with " + col.name + " and activated OnTriggerStay");
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have collided with " + col.name + " while holding down Touch");
            col.attachedRigidbody.isKinematic = true;
            col.gameObject.transform.position = paddle;
            //  col.gameObject.transform.SetParent(gameObject.transform);
        }
        if (device.GetTouchUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            Debug.Log("You have released Touch while colliding with " + col.name);
            // col.gameObject.transform.SetParent(null);
            col.attachedRigidbody.isKinematic = false;

            //tossObject(col.attachedRigidbody);
        }
    }

    void tossObject(Rigidbody rigidBody)
    {
        Transform origin = trackedObj.origin ? trackedObj.origin : trackedObj.transform.parent;
        if (origin != null)
        {
            rigidBody.velocity = origin.TransformVector(device.velocity);
            rigidBody.angularVelocity = origin.TransformVector(device.angularVelocity);
        }
        else
        {
            rigidBody.velocity = device.velocity;
            rigidBody.angularVelocity = device.angularVelocity;
        }

    }*/
}
