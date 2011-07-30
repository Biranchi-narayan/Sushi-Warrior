using UnityEngine;
using System.Collections;

public class SkeletonToJS : MonoBehaviour {

	public BerkeliumWindow rw;
	public OpenNISkeleton ons;
	// Use this for initialization
	void Start () {
	
	}
	void OnEnable()
	{
		string js = "document.style.backgroundColor='#FF00FF'";
		rw.ExecuteJavascript(js);
	}
	// Update is called once per frame
	void Update () {
		string js = "";
		
		js = ons.JSONSkeleton();
		//Debug.Log(js);
		rw.ExecuteJavascript("SetSkeleton('"+js+"');");
	}
}
