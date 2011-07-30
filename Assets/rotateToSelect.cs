using UnityEngine;
using System.Collections;

public class rotateToSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public Transform measureRotation;
	public float value;
	// Update is called once per frame
	void Update () {
//		Debug.Log( transform.rotation.eulerAngles.ToString());
	
			//map -180 to 180 to 0 1
		//value = mapEulerTo01(measureRotation.rotation.eulerAngles.y);
		value = mapEulerWithMaxTo01(measureRotation.rotation.eulerAngles.y);

	}
	
	//-180 to 180 maps to 0 1, using 0 to 360 coords 
	float mapEulerTo01(float eulerAngle)
	{
		float div360 = eulerAngle / 360.0f;
		return (div360 > .5f) ? (div360 - .5f) : (div360 + .5f);
	}
	
	//
	public float maxAngle = 90.0f;
	//maps from -maxangle to maxangle
	float mapEulerWithMaxTo01(float eulerAngle)
	{
		float tmp = (eulerAngle > 180.0f) ? (eulerAngle - 360.0f) : eulerAngle;	//convert 0 to 360 to -180  to 180
		float tmp2 = Mathf.Clamp(tmp, -maxAngle, maxAngle); //clamp
		return ((tmp2 / (2*maxAngle)) + .5f);  
		
	}
}

