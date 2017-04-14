using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forwardCam : MonoBehaviour {

    public bool myTrigger=true;
    public float mySpeed;


    //  void OnTriggerEnter(Collider other) {
    void OnMouseDown()
    {
        if (myTrigger == false) {
            myTrigger = true;
        }
        else if (myTrigger == true) {
            myTrigger = false;
        }
    }

    void HandleClick()
    {
        if (myTrigger == false) { myTrigger = true; }
        else if (myTrigger == true) { myTrigger = false; }
    }

    // Update is called once per frame
    void Update()
    {

        if (myTrigger == true)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * mySpeed);
        }
        else transform.position = transform.position;
    }



}


