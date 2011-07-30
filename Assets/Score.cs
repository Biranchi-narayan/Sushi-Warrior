using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	public int FishFired = 0;
	public int FishCut = 0;
	public GameTimer gt;
	// Update is called once per frame
	void Update () {
		guiText.text = FishCut.ToString() + " / " + FishFired.ToString() 
		+ "  Time: " + (gt.GameTime - gt.time).ToString();
	}
	public void FireFish()
	{
		FishFired++;
	}
	public void CutFish()
	{
		FishCut++;
	}
}
