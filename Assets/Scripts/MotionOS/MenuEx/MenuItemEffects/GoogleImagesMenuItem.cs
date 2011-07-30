using UnityEngine;
using System.Collections;

public class GoogleImagesMenuItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	IEnumerator MenuItem_Init(Hashtable result)
	{
		// description
		GetComponentInChildren<TextMesh>().text = (string)result["contentNoFormatting"];
		
		// image
		WWW req = new WWW((string)result["url"]);
		yield return req;
		renderer.material.mainTexture = req.texture;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
