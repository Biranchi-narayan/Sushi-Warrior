using UnityEngine;
using System.Collections;

public class tenseconds : MonoBehaviour {

	float timeAccum = 0.0f;
	// Use this for initialization
	void Start () {
		timeAccum = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		timeAccum += Time.deltaTime;
		if (timeAccum > 10.0f)
		{
			Destroy (gameObject);
		}
	}
}
