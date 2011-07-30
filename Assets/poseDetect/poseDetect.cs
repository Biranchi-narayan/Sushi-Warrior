using UnityEngine;

using System.Collections;

public class poseDetect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	
	void FixedUpdate () {
		if (poseStart)
		{
			timeHeld += Time.deltaTime;
			if (timeHeld > 1.0f)
			{
				
				if (!poseDetected)
				{
					Debug.Log("POSE DETECTED POSE DETECTED POSE DETECTED !!!!!!!!!!!!!!!");
				}
				poseDetected = true;
			}
		}
	}
	
	
	public bool poseStart = false;
	public float holdTime = 1.0f;
	public float timeHeld = 0.0f;
	public bool poseDetected = false;
	void OnTriggerEnter(Collider c)
	{
		Debug.Log(c.name);
		if (c.name == "RightHand")
		{
			poseStart = true;
		}
		
	}
	void OnTriggerExit(Collider c)
	{
		Debug.Log(c.name);
		if (c.name == "RightHand")
		{
			poseStart = false;
			poseDetected = false;
			timeHeld = 0.0f;
		}
	}
}
