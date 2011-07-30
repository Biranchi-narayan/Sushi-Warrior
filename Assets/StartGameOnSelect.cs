using UnityEngine;
using System.Collections;

public class StartGameOnSelect : MonoBehaviour {

	public GameObject ContainingMenu;
	public GameObject Spawner;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Selected()
	{
		yield return new WaitForSeconds(2.0f);
		ContainingMenu.SetActiveRecursively(false);
		Spawner.SetActiveRecursively(true);
	}
}
