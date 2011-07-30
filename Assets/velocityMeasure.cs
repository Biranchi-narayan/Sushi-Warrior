using UnityEngine;
using System.Collections;

public class velocityMeasure : MonoBehaviour {

	Vector3 prevPosition;
	public float totalEnergy;
	public float maxEnergy = 300.0f;
	public float leaky = .9f;
	public Transform trns;
	public float instEnergy;
	void Start () {
		totalEnergy = 0.0f;
		prevPosition = Vector3.zero;
	}
	
	
	// Update is called once per frame
	void Update () {
		Vector3 deltaX = (prevPosition-trns.position);
		prevPosition = trns.position;
		float v = (deltaX.magnitude / Time.deltaTime);
		instEnergy = v*v;
		totalEnergy = totalEnergy*leaky + instEnergy;
		totalEnergy = Mathf.Clamp(totalEnergy,0,maxEnergy);
	}
}
