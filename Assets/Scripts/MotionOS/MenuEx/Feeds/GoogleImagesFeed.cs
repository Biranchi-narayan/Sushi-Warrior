using UnityEngine;
using System.Collections;

public class GoogleImagesFeed : MonoBehaviour 
{
	public MenuBase Menu;
	public string keyword = "Monkeys";
	public int chunkSize;//must be between 1 and 8 for google api
	public string URL = "https://ajax.googleapis.com/ajax/services/search/images";
	
	Hashtable imageQueryResult;
	ArrayList results; 
	
	IEnumerator Start() {
		yield return StartCoroutine("fetch");
	}
	
	void Menu_OutOfBounds(bool forwards) {
		print("Image feed received message that menu is out of bounds.");
		if(forwards) {
			print("The feed is hungry, fetching...");
			StartCoroutine("fetch");
			print("Ran fetch!");
		}else {
			print("false alarm sent to feed.");
		}
	}
	
	// Use this for initialization
	IEnumerator fetch () {
		// get list
		int index = Menu.GetComponentsInChildren<Transform>().Length;
		string Query = "?v=1.0&q="+keyword+"&rsz="+chunkSize+"&start="+index;
		string fullURL = URL+Query;
		print(fullURL);
		WWW www = new WWW(fullURL);
		yield return www;
		print("Fetched results...");
		imageQueryResult = (Hashtable) JSON.JsonDecode(www.text);
		results = (ArrayList)((Hashtable)imageQueryResult["responseData"])["results"];

		foreach (Hashtable result in results)
		{
			Menu.AddToEnd(result);
		}	
	}
}
