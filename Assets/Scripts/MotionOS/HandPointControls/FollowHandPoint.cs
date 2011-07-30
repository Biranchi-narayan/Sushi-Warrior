using UnityEngine;
using System;

public class FollowHandPoint : MyHandPointControl
{
	// default scale convert openni (millimeters) to unity (meters)
	public float scale = 0.001f; 
	public Vector3 bias;
	public float damping = 5;

	private Vector3 desiredPos; 
	
	void Start()
	{
		desiredPos = transform.localPosition;
	}
	
	void Update()
	{
		transform.localPosition = Vector3.Lerp(transform.localPosition,  desiredPos, damping * Time.deltaTime);
	}
	
	void Hand_Create(Vector3 pos)
	{
		renderer.material.color = Color.green;
		desiredPos = ((pos - MySessionManager.FocusPoint) * scale) + bias;
	}
	
	void Hand_Update(Vector3 pos)
	{
		desiredPos = ((pos - MySessionManager.FocusPoint) * scale) + bias;
	}
	
	void Hand_Destroy()
	{
		renderer.material.color = Color.red;
	}
}