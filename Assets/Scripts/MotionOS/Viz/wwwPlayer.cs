using UnityEngine;
using System.Collections;

public class wwwPlayer : MonoBehaviour {


    public string url;
	private bool startingDownload = false;
    void Start() {
        WWW www = new WWW(url);
        audio.clip = www.audioClip;
		startingDownload = true;
    }
    void Update() {
		
		if (!audio.clip.isReadyToPlay)
		{
			startingDownload = true;
		}
		else if (startingDownload)
		{	
			startingDownload = false;
			audio.Play();
		}
     
    }
	public static int guiDepth = -2;
	void OnGUI()
	{
		GUI.depth = guiDepth;
		if (startingDownload)
		{
			GUI.TextField(new Rect(10, 10, 150, 100), "Buffering...");
		}
		else
		{
			if (!audio.isPlaying)
			{
				if (GUI.Button(new Rect(10, 10, 150, 100), "Play"))
				{
					audio.Play();
				}
			}
			else
			{
				if (GUI.Button(new Rect(10, 10, 150, 100), "Pause"))
				{
					audio.Pause();
				}
			}
		}
	}
}