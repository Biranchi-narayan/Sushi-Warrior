using UnityEngine;
using System.Collections;

public class rotate_on_click : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public float sp = 90.0f;
	public float total = 0.0f;
	// Update is called once per frame
	void Update () {
		if (total < 720){
			
		float amt = sp*Time.deltaTime;
		transform.Rotate(new Vector3(0,amt,0));
		total += amt;
			}
	}
}
