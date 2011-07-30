using UnityEngine;
using System.Collections;

public class StartMenuItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public GameObject pf;
	public Transform cutToSelectItem;
	private GameObject clone;
	void OnEnable()
	{
		Destroy(clone);
		Debug.Log("ReloadFromPrefab");
		clone = (GameObject)Instantiate(pf, cutToSelectItem.position, cutToSelectItem.rotation); //Make a clone
		clone.transform.parent = transform; //make it have me as parent
		clone.SetActiveRecursively(false);
		 //Kill child!
		Debug.Log("Cloned!");
		cutToSelectItem = clone.transform;
	}
	void MenuItem_Deselect()
	{
		
		
		GetComponentInChildren<knifeselect>().renderer.material.color = Color.white;
		
		//foreach (Renderer r  in transform.Find("poppyFish").GetComponentsInChildren<Renderer>())
		//{
		//	r.enabled = false;
		//}
		clone.gameObject.SetActiveRecursively(false);
	}
	void MenuItem_Select()
	{
//		foreach (Renderer r  in transform.Find("poppyFish").GetComponentsInChildren<Renderer>())
//		{
//			r.enabled = true;
//		}
		clone.SetActiveRecursively(true);
		GetComponentInChildren<knifeselect>().renderer.material.color = Color.green;
	}	
	// Update is called once per frame
	void Update () {
	
	}
}
