using UnityEngine;
using System.Collections;
using NITE;//included for the axis class

public class MoveCameraXYBouncy : MonoBehaviour {
	
	public Camera targetCamera;
	public float Rate = 5.0f;
	public float BounceAmount = 0.5f;
	public bool isScrolling = false;
	public ListnavController controller;
	public Axis axis;
	
	//ScrollingMenu Parent { get { return GetComponent<ScrollingMenu>() as ScrollingMenu; } }

	public LinearMenu MyMenu;// { get { return GetComponent<LinearMenu>(); } }
	
	Vector3 targetPos = new Vector3();
	
	void Start()
	{
		targetPos = targetCamera.transform.position;
	}
	
	void Update()
	{
		if( ! isScrolling )  
		{
			Vector3 sliderPosition = Vector3.zero;
			float sliderValue = controller.ValueFromCenter;
			if(controller.Reverse) {
				sliderValue = - sliderValue;
			}
			
			switch (axis)
			{
				case Axis.X:
					sliderPosition = new Vector3(sliderValue,0,0);
					break;
				case Axis.Y:
					sliderPosition = new Vector3(0,sliderValue,0);
					break;
				case Axis.Z:
					print("wtf: axis is set to z...");
					sliderPosition = new Vector3(0,0,sliderValue);
					break;
			}
			if(MyMenu.ActiveItem)
			{
				targetPos.x = MyMenu.ActiveItem.position.x + sliderPosition.x;
				targetPos.y = MyMenu.ActiveItem.position.y + sliderPosition.y;
				
				//targetCamera.transform.position = new Vector3(x,y,targetCamera.transform.position.z);
				
				//print("Slider position: "+sliderPosition);
			}else {
				//print("Menu's active item does not appear to exist...");
			}
		}
		
		targetCamera.transform.position = Vector3.Lerp(targetCamera.transform.position, targetPos, Rate * Time.deltaTime);
	}
	
	void Menu_Activate(Transform child)
	{
		targetPos.x = child.transform.position.x;
		targetPos.y = child.transform.position.y;
	}
	
	void ListNav_StartScrolling() {
		isScrolling = true;
	}
	
	void ListNav_StopScrolling()
	{
		isScrolling = false;
		//targetPos.x = MyMenu.ActiveItem.position.x;
	}
}