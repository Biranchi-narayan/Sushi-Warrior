using UnityEngine;
using System.Collections;

public class OnYoutubeItemSelect : MonoBehaviour {
	public NavigatorController nav;
	public PlayYoutubeVideo YoutubePlayer;
	
	void Menu_Select(Transform item)
	{
		print("Menu item selected: " + item.gameObject.name);
		YoutubeMenuItem menuItem = item.GetComponent<YoutubeMenuItem>();
		if (!menuItem) {
			Debug.LogError("Selected a non-youtube item");
			return;
		}
		
		PlayYoutubeVideo player = YoutubePlayer.GetComponent<PlayYoutubeVideo>();
		if (!YoutubePlayer) {
			Debug.LogError("Invalid youtube player");
			return;
		}
			
		YoutubePlayer.LoadVideo(menuItem.video.VideoId);
		
		if (nav) {
			nav.NavigateTo(YoutubePlayer.transform);
		}
	}
}
