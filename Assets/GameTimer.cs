using UnityEngine;
using System.Collections;

public class GameTimer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	
	public GameObject StartMenu;
	public GameObject Spawners;
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		if (time >= GameTime) {
			time = GameTime; // clamp to max
			StartMenu.active = true;
		
			Spawners.SetActiveRecursively(false);
		}
	}
	
	public float time = 0.0f;
	public float GameTime = 60.0f;
	
	void OnEnable() {
		time = 0.0f;
	}
	
	
}
