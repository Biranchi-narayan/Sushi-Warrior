using UnityEngine;
using System.Collections;

public class reloadFromPrefab : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public GameObject pf;
	void ReloadFromPrefab()
	{
		GameObject clone = (GameObject)Instantiate(pf, transform.position, transform.rotation); //Make a clone
		clone.transform.parent = transform.parent; //make it have my parent
		Debug.Log("Cloned!");
		Destroy(gameObject); //Kill Myself!
		
	}
}
