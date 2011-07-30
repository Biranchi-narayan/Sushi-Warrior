using UnityEngine;
using System.Collections;

public class PushToSelect : MonoBehaviour 
{
	public float clickTimeFrame = 1.0f;
	public float clickPushTime { get; private set; }
	
	// Use this for initialization
	void Start () 
	{
		clickPushTime = 0.0f;
		
		if (null == GetComponent<PushDetector>())
		{
			Debug.LogWarning("No push detector in " + gameObject.name + ". Adding...");
			gameObject.AddComponent(typeof(PushDetector));
		}
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
