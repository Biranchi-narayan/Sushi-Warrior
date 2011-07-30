using UnityEngine;
using System.Collections;

using Google.GData.Client;
using Google.GData.Extensions;
using Google.GData.YouTube;
using Google.GData.Extensions.MediaRss;
using Google.YouTube;

public class MyYoutubeFeed : FeedBase {
	public string keyword = "Monkeys";
	public int ChunkSize = 10;
	public string DeveloperKey = "AI39si5pqetR3Oz-JzgEcnRUmFxl1sb_x8TBX4H9E7Y1wIUxlaYIb3sHp3_n_fPN1H0UE_ylwWDmGeEzGsS-X07HfAZF8sLmfg";
	
	int CurrentIndex = 0;
	
	void Start()
	{
		fetch();
	}
	
	void fetch () {
		YouTubeQuery query = new YouTubeQuery(YouTubeQuery.DefaultVideoUri);
		YouTubeRequest request = new YouTubeRequest(new YouTubeRequestSettings("", DeveloperKey));

		query.Query = keyword;
		query.NumberToRetrieve = ChunkSize;
		query.StartIndex = CurrentIndex;
		query.SafeSearch = YouTubeQuery.SafeSearchValues.None; // porn
		
		// TODO: find out how to async this
		Feed<Video> videoFeed = request.Get<Video>(query);
		
		foreach (Video vid in videoFeed.Entries)
		{
			menu.AddToEnd(CreateItem(vid));
			CurrentIndex++;
		}
	}
}
