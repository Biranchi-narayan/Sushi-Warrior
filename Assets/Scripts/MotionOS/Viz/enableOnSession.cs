using UnityEngine;
using System.Collections;

public class enableOnSession : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public GameObject[] gos;
	
	void OnSessionStart()
	{
		foreach (GameObject go in gos)
		{
			go.SetActiveRecursively(true);
		}
	}
	void OnSessionEnd()
	{
		foreach (GameObject go in gos)
		{
			go.SetActiveRecursively(false);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
