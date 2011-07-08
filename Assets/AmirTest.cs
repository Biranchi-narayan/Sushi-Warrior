using UnityEngine;
using System.Collections;
using AmirTest2;

public class AmirTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
	Amir amir = new Amir();
		int x = amir.squareit(4);
		Debug.Log(x.ToString());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
