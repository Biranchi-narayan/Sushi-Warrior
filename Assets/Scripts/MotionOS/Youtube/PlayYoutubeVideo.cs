using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayYoutubeVideo : MonoBehaviour {
	public string VideoId = "CxlG3sspWBI";
	public int playerWidth = 500;
	public int playerHeight = 500;
	
	BerkeliumWindow browser;
	// Use this for initialization
	IEnumerator Start () {
		browser = GetComponent<BerkeliumWindow>();
		if (!browser) {
			print("Need a browser");
			yield return null;
		}
		
		browser.NavigateTo("http://www.motionos.com/chromelessplayer.html");
		yield return StartCoroutine(browser.WaitUntilDoneLoading());
		LoadPlayer(playerWidth, playerHeight);
	}
	
	void LoadPlayer(int width, int height)
	{
		browser.ExecuteJavascript(string.Format("loadPlayer({0},{1});", width, height));
	}
	
	public void LoadVideo(string videoId)
	{
		browser.ExecuteJavascript(string.Format("loadVideo(\"{0}\");", videoId));
	}
	
	public void Play()
	{
		browser.ExecuteJavascript("playVideo();");
	}
	
	public void Pause()
	{
		browser.ExecuteJavascript("pauseVideo();");
	}
	
	public void Stop()
	{
		browser.ExecuteJavascript("stopVideo();");
	}
}
