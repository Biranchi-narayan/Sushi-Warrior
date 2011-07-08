using UnityEngine;
using System.Collections;

public class spawncuttable : MonoBehaviour {
	public Rigidbody projectile;
	public Vector3 velo;
	public Vector3 offset;
	public float delayLow;
	public float delayHigh;
	public OpenNIUserTracker kinect;
	public Vector3 lowRand;
	public Vector3 highRand;
	
	public Vector3 lowRTorque;
	public Vector3 highRTorque;
	// Use this for initialization
		private Score scorekeeper;
	void Start () {
		scorekeeper = GameObject.Find("Score").GetComponent<Score>();
	}

	float timeAccum = 0.0f;
	// Update is called once per frame
	void Update () {
		timeAccum += Time.deltaTime;
		if (timeAccum > Random.Range(delayLow,delayHigh))
		
		{
			if (kinect.trackingUser)
			{
				timeAccum = 0.0f;
				Rigidbody clone;
            	clone = (Rigidbody)Instantiate(projectile, transform.position + offset, transform.rotation);
				scorekeeper.FireFish();
				/*
				Color rc = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f),1f);
				foreach (Renderer r in clone.transform.GetComponentsInChildren<Renderer>())
				{
					
					r.material.color = rc;
            	}
				*/
				float xRandMove = (Random.Range(lowRand.x, highRand.x));
				float yRandMove = (Random.Range(lowRand.y, highRand.y));
				float zRandMove = (Random.Range(lowRand.z, highRand.z));

            	clone.velocity = velo + new Vector3(xRandMove, yRandMove, zRandMove);
            	
            	
            	
				clone.angularVelocity = new Vector3(Random.Range(lowRTorque.x,highRTorque.y),Random.Range(lowRTorque.x,highRTorque.y),Random.Range(lowRTorque.z,highRTorque.z));
			}

		}
		
		
	}
}
