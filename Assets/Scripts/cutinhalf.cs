using UnityEngine;
using System.Collections;

public class cutinhalf : MonoBehaviour {
private Score scorekeeper;
	// Use this for initialization
	void Start () {
		
		scorekeeper = GameObject.Find("Score").GetComponent<Score>();
		foreach (Transform t in enableAfterCut)
			{
				t.gameObject.SetActiveRecursively(false);
			}
			foreach (Transform t in disableAfterCut)
			{
				t.gameObject.SetActiveRecursively(true);
			}

	}
	
	
	public Transform [] enableAfterCut;
	public Transform [] disableAfterCut;
	
	/*	private float kineticEnergy = 0.0f;
	void FixedUpdate()
	{
		float v = rigidbody.velocity.magnitude;
		kineticEnergy = .5f * rigidbody.mass * v * v;
	}
	*/
	
	private float kineticThreshold = 25.0f;
	
	
	private float cutThreshold = 3f;
	bool isCut = false;
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision with" + other.gameObject.name);
		if (!isCut)
		{
		if (other.gameObject.tag == "blade")
		{
				
			float kineticEnergy = other.transform.parent.parent.GetComponentInChildren<velocityMeasure>().instEnergy;
			
		Debug.Log("Collision with blade, rv: " + other.relativeVelocity.magnitude.ToString() + " ke: " + kineticEnergy.ToString());
			if ((other.relativeVelocity.magnitude > cutThreshold) && (kineticEnergy > kineticThreshold))
			{	
			foreach (Transform t in enableAfterCut)
			{
				t.gameObject.SetActiveRecursively(true);
			}
				foreach (Transform t in disableAfterCut)
			{
				t.gameObject.SetActiveRecursively(false);
			}
			isCut = true;
			scorekeeper.CutFish();
		}
			}
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
