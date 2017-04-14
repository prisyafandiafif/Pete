using UnityEngine;
using UnityEngine.VR;
using System.Collections;

public class VRTransform : MonoBehaviour {
	void Update () {
        transform.localRotation = InputTracking.GetLocalRotation(VRNode.CenterEye) * Quaternion.Euler(0, 0, 0);
	} 
}
