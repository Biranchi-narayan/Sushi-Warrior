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
	bool isCut = false;
	void OnCollisionEnter(Collision other)
	{
		Debug.Log("Collision with" + other.gameObject.name);
		if (!isCut)
		{
		if (other.gameObject.tag == "blade")
		{
			Debug.Log("Collision with blade");
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
	
	// Update is called once per frame
	void Update () {
	
	}
}
