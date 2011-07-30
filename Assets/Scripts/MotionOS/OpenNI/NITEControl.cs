using UnityEngine;
using System.Collections;
using NITE;

public class NITEControl : MonoBehaviour
{
	public NITESessionManager SessionManager;

	Broadcaster internalBroadcaster = new Broadcaster();
	
	protected void AddListener(MessageListener listener)
	{
		internalBroadcaster.AddListener(listener);
	}
	
	protected void RemoveListener(MessageListener listener)
	{
		internalBroadcaster.RemoveListener(listener);
	}
	
	void OnEnable()
	{
		if (null != SessionManager)
		{
			SessionManager.AddListener(internalBroadcaster);
		}
	}
		
	void OnDisable()
	{
		if (null != SessionManager)
		{
			SessionManager.RemoveListener(internalBroadcaster);
		}
	}
	
	void Navigator_Activate()
	{
		this.enabled = true;
	}
	
	void Navigator_Deactivate()
	{
		this.enabled = false;
	}
}
