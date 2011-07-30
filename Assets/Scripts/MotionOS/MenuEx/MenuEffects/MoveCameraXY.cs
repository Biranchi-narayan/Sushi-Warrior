using UnityEngine;
using System.Collections;

public class MoveCameraXY : MonoBehaviour {
	
	public Camera targetCamera;
	public float Rate = 5.0f;
	public float BounceAmount = 0.5f;

	MenuBase MyMenu { get { return GetComponent<MenuBase>(); } }
	
	Vector3 targetPos = new Vector3();
	
	void Start()
	{
		if (targetCamera == null)
		{
			Debug.LogWarning("MoveCameraXY on GameObject " + gameObject.name + " is Missing a Camera, resorting to Main Camera!");
			targetCamera = Camera.main;
		}
		targetPos = targetCamera.transform.position;
	}
	
	void Update()
	{
		targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, targetPos, Rate * Time.deltaTime);
	}
	
	void Menu_Activate(Transform child)
	{
		targetPos.x = child.transform.position.x;
		targetPos.y = child.transform.position.y;
	}
	
	void Navigator_ActivatedItem(Transform child)
	{
		targetPos.x = child.transform.position.x;
		targetPos.y = child.transform.position.y;
	}
	
	void Navigator_Activate()
	{
		this.enabled = true;
	}

	void Navigator_Deactivate()
	{
		this.enabled = false;
	}
	
	/*
	void Menu_OutOfBounds(bool forward)
	{
		targetPos.x = Parent.ActiveItem.position.x;
		if (forward)
		{
			 targetPos.x += BounceAmount;
		}
		else
		{
			targetPos.x -= BounceAmount;
		}
	}*/
	
	/*void ListNav_StopScrolling()
	{
		targetPos.x = MyMenu.ActiveItem.position.x;
	}*/
}
