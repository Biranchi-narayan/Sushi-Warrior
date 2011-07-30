using UnityEngine;
using System;

public class MyHandPointControl : MonoBehaviour
{
	void Hand_Create(Vector3 pos)
	{
		
	}
	
	void Hand_Update(Vector3 pos)
	{
		
	}
	
	void Hand_Destroy()
	{
		
	}
	
	void OnEnable()
	{
		// only add to session manager by default if not navigable
		if (null == GetComponent<Navigable>())
		{
			MySessionManager.AddListener(this.gameObject);
		}
	}
	
	void OnDisable()
	{
		MySessionManager.RemoveListener(this.gameObject);
	}
		
	protected Vector3 FocusPoint { get { return MySessionManager.FocusPoint; } }
}