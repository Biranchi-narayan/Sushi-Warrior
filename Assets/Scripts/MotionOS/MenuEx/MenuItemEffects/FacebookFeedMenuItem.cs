using UnityEngine;
using System.Collections;
using System;

public class FacebookFeedMenuItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	IEnumerator MenuItem_Init(Hashtable item)
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		Transform cube = transform.Find("Cube");
		
		string idString = item["id"] as string;
		if(String.IsNullOrEmpty(idString)) {
			print("invalid feedItem, no id");
			return false;
		}
		//print("message id: "+idString);
		
		// description
		string message = item["message"] as string;
		if( ! String.IsNullOrEmpty(message) ) {
			textMesh.text = item["message"] as string;
		}else {
			textMesh.text = "id=\""+idString+"\"";
		}
		
		// image
		Hashtable fromTable = item["from"] as Hashtable;
		string fromIDString = fromTable["id"] as string;
		
		string urlString = "https://graph.facebook.com/"+fromIDString+"/picture";
		print("image url: "+urlString);
		WWW req = new WWW(urlString);
		yield return req;
		
		cube.gameObject.renderer.material.mainTexture = req.texture;
		
		//float sin45 = 1.0f/Mathf.Sqrt(2.0f);
		float w = 0.0f;
		float x = 0.0f;
		float y = 1.0f;
		float z = 0.0f;
		cube.rotation = new Quaternion(x,y,z,w);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
