using UnityEngine;
using System.Collections;

public class cutinhalfSelector : MonoBehaviour {
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
	
	void OnEnable()
	{
		if (isCut)
		{
		
			foreach (Transform t in disableAfterCut)
			{
				t.gameObject.SetActiveRecursively(false);
			}			
		}
		else
		{
			foreach (Transform t in enableAfterCut)
			{
				t.gameObject.SetActiveRecursively(false);
			}
		}
		
	}
	
	
	public Transform [] enableAfterCut;
	public Transform [] disableAfterCut;
	private float cutThreshold = 1.0f;
	private float kineticThreshold = 20.0f;
	bool isCut = false;
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision with: " + other.gameObject.name);
			Debug.Log("Collision tag: " + other.gameObject.tag);
		if (!isCut)
		{
		if (other.gameObject.tag == "blade")
		{
			//float v = other.rigidbody.velocity.magnitude;
			//	Debug.Log("Blade Velocity: " + other.rigidbody.velocity.ToString());
			//	Debug.Log("My Velocity: " + rigidbody.velocity.ToString());
			//float kineticEnergy = .5f * other.rigidbody.mass * v * v;
				Debug.Log(other.transform.parent.parent.gameObject.name);
			float kineticEnergy = other.transform.parent.parent.GetComponentInChildren<velocityMeasure>().instEnergy;
			Debug.Log("Collision with blade, rv: " + other.relativeVelocity.magnitude.ToString() + " KE of " + other.gameObject.name + " = " + kineticEnergy.ToString());
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
			
				//StartCoroutine(MsgAfterDelay())
				transform.parent.parent.BroadcastMessage("Selected", SendMessageOptions.DontRequireReceiver);
				scorekeeper.CutFish();
			}
		}
	}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
