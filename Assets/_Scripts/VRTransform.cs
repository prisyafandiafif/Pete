using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class VRTransform : MonoBehaviour 
{
    public CameraLibrary cameraLib;

	void Update () 
    {
        if (cameraLib.modeID == 2)
        {
            transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye) * Quaternion.Euler(0, 0, 0);
        }
	} 
}
