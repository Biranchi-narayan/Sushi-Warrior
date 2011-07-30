using UnityEngine;
using System.Collections;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	public Transform[] enableWithMenu;
	public Transform selectionController;
	void OnEnable()
	{
			foreach (Transform t in enableWithMenu)
			{
				t.gameObject.active = true;
			}
		selectionController.active = true;
	}
	
}
