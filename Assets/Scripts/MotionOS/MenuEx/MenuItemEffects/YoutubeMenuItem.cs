using UnityEngine;
using System.Collections;
using Google.YouTube;
using Google.GData;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;

public class YoutubeMenuItem : MonoBehaviour {
	public Video video;
	
	IEnumerator FeedItem_Init(Video vid)
	{
		video = vid;

		// find a textmesh for the description
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		if (textMesh)
		{
			//textMesh.text = vid.Description.Substring(0,Mathf.Min(30, vid.Description.Length));
			textMesh.text = vid.Title;
		}
		
		// find a thumbnail child
		Transform thumb = transform.Find("Thumbnail");
		if (thumb)
		{
			ExtensionCollection<MediaThumbnail> thumbnails = video.Thumbnails;
			MediaThumbnail thumbnail = thumbnails[0];
			
			string urlString = thumbnail.Url;
			WWW req = new WWW(urlString);
			yield return req;
			
			thumb.gameObject.renderer.material.mainTexture = req.texture;
			thumb.gameObject.renderer.material.SetTextureScale("_MainTex",new Vector2(1.0f,-1.0f));
		}
	}
}
