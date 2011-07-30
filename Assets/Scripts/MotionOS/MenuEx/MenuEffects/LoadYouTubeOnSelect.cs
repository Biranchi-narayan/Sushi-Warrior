using UnityEngine;
using System.Collections;

public class LoadYouTubeOnSelect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
	public Transform YouTubePlayer;
	public NavigatorController nav;
	void MenuItem_Select(){
		//Application.LoadLevel(1);
		nav.NavigateTo(YouTubePlayer);
		Camera.main.animation.Play();
	}
}
