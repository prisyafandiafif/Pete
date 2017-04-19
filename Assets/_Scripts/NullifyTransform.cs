using UnityEngine;
using System.Collections;

public class NullifyTransform : MonoBehaviour 
{
    public CameraLibrary cameraLib;

    public Transform t;

	void LateUpdate () 
    {
        if (cameraLib.modeID == 2 && t != null)
        {
            transform.localRotation = Quaternion.Inverse(t.localRotation);
            transform.localPosition = -t.localPosition;
        }
	} 
}
