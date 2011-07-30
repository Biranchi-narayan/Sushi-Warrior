using UnityEngine;
using System.Collections;

public class PushToSelectNoMenu : MonoBehaviour {
	

	
	public float clickTimeFrame = 1.0f;
	public float clickPushTime { get; private set; }
	
	// Use this for initialization
	void Start () 
	{
		clickPushTime = 0.0f;
		// Amir - this is for you ;) also note that PushToSelect doesn't require a push detector explicitly any more
		Debug.LogError("Dont use me!! Use PushToSelect instead, and then delete PushToSelectNoMenu");
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	void PushDetector_Push() 
	{
		clickPushTime = Time.time;
	}
	
	void PushDetector_Release()
	{
		if( Time.time < clickPushTime + clickTimeFrame )
		{
			SendMessage("Menu_SelectActive",SendMessageOptions.DontRequireReceiver);
		}
	}
}
