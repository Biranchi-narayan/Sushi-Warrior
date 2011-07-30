using UnityEngine;
using System.Collections;

public class FacebookFeed : MonoBehaviour {
	
	public MenuBase Menu;
	public int chunkSize;//must be between 1 and 8 for google api
	public string URL = "https://graph.facebook.com/ted.blackman/feed?access_token=";
	public string token;//should set in inspector window
	public string nextURL = "";
	public string previousURL = "";
	public bool fetching = false;
	
	Hashtable queryResult;
	ArrayList feedItems;
	
	
	// Use this for initialization
	IEnumerator Start () {
		nextURL = URL+token;
		print(nextURL);
		yield return StartCoroutine("fetch",true);
	}
	
	void Menu_OutOfBounds(bool forwards) {
		if(fetching) {
			print("Already fetching...");
			return;
		}
		print("Image feed received message that menu is out of bounds.");
		StartCoroutine("fetch",forwards);//not yield return startcoroutine, so it can move on immediately
		print("Starting fetch "+((forwards)?"forwards":"backwards"));
	}
	
	// Use this for initialization
	IEnumerator fetch (bool forwards) {
		// get list
		fetching = true;
		string fullURL = (forwards)? nextURL : previousURL;
		print (fullURL);
		WWW www = new WWW(fullURL);
		yield return www;
		print("Fetched results...");
		queryResult = (Hashtable) JSON.JsonDecode(www.text);
		
		Hashtable paging = (Hashtable)queryResult["paging"];
		nextURL = 		(string)paging["next"];
		previousURL = 	(string)paging["previous"];
		
		print("nextURL: "+nextURL);
		
		feedItems = (ArrayList)queryResult["data"];
		

		foreach (Hashtable item in feedItems)
		{
			Menu.AddToEnd(item);
		}	
		print("Finished running fetch.");
		fetching = false;
	}
}
