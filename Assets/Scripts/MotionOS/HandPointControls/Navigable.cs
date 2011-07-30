using UnityEngine;
using System.Collections;

public class Navigable : MonoBehaviour {
	private bool ActiveOnStart = false;
	
	void Start()
	{
		if (ActiveOnStart)
		{
			Navigator_Activate();
		}
	}
	
	void Navigator_Activate()
	{
		print("Navigable: Activating " + this.gameObject.name);
		MySessionManager.AddListener(this.gameObject);
	}

	void Navigator_Deactivate()
	{
		print("Navigable: Deactivating " + this.gameObject.name);
		MySessionManager.RemoveListener(this.gameObject);
	}
}
