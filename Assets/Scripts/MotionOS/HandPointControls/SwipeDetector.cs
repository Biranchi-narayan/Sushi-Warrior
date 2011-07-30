using UnityEngine;
using System.Collections;

public class SwipeDetector : MyHandPointControl {
	
	public enum SwipeDirection
	{
		Left = 0,
		Right,
		Up,
		Down
	}
	
	public bool flipX = true;
	public bool flipY = true;
	public Slider xSlider;
	public Slider ySlider;
	
	public float resetSeconds = 0.5f;
	
	public float xSliderSize = 150.0f;
	public float xDriftSpeed = 1.5f;
	public float xInitialValue = 0.5f;
	public float xReleaseThreshold = 0.3f;
	public Vector3 xClickPosition { get; private set; }
	public float xClickProgress { get; private set; }
	
	public float ySliderSize = 150.0f;
	public float yDriftSpeed = 1.5f;
	public float yInitialValue = 0.5f;
	public float yReleaseThreshold = 0.3f;
	public Vector3 yClickPosition { get; private set; }
	public float yClickProgress { get; private set; }
	
	public bool leftIsClicked;// { get; private set; }
	public bool rightIsClicked;// { get; private set; }
	public bool upIsClicked;// { get; private set; }
	public bool downIsClicked;// { get; private set; }

	public bool debugOutput = true;//if true, print out stuff
	
	IEnumerator WaitAndSetFalse(SwipeDirection flag)
	{
		if(debugOutput) print("Started wait coroutine.");
		yield return new WaitForSeconds(resetSeconds);
		if(debugOutput) print("Finished wait coroutine.");
		switch(flag)
		{
		case SwipeDirection.Left:
			OnLeftUnclick();
			break;
		case SwipeDirection.Right:
			OnRightUnclick();
			break;
		case SwipeDirection.Up:
			OnUpUnclick();
			break;
		case SwipeDirection.Down:
			OnDownUnclick();
			break;
		}
	}
	
	void OnRightClick(Vector3 pos)
	{
		xClickPosition = pos;
        rightIsClicked = true;
		SendMessage("Right_Swipe", SendMessageOptions.DontRequireReceiver);
		StartCoroutine("WaitAndSetFalse",SwipeDirection.Right);
		if(debugOutput) print("Right_Swipe");
	}
	
	void OnRightUnclick()
	{
		SendMessage("Right_Swipe_Release", SendMessageOptions.DontRequireReceiver);
		if(debugOutput) print("Right_Swipe_Release");
		rightIsClicked = false;
	}
	
	void OnLeftClick(Vector3 pos)
	{
		xClickPosition = pos;
        leftIsClicked = true;
		SendMessage("Left_Swipe", SendMessageOptions.DontRequireReceiver);
		StartCoroutine("WaitAndSetFalse",SwipeDirection.Left);
		if(debugOutput) print("Left_Swipe");
	}
	
	void OnLeftUnclick()
	{
		SendMessage("Left_Swipe_Release", SendMessageOptions.DontRequireReceiver);
		if(debugOutput) print("Left_Swipe_Release");
	    leftIsClicked = false;
	}
	
	void OnUpClick(Vector3 pos)
	{
		yClickPosition = pos;
        upIsClicked = true;
		SendMessage("Up_Swipe", SendMessageOptions.DontRequireReceiver);
		StartCoroutine("WaitAndSetFalse",SwipeDirection.Up);
		if(debugOutput) print("Up_Swipe");
	}
	
	void OnUpUnclick()
	{
		SendMessage("Up_Swipe_Release", SendMessageOptions.DontRequireReceiver);
		if(debugOutput) print("Up_Swipe_Release");
		upIsClicked = false;
	}
	
	void OnDownClick(Vector3 pos)
	{
		yClickPosition = pos;
	    downIsClicked = true;
		SendMessage("Down_Swipe", SendMessageOptions.DontRequireReceiver);
		StartCoroutine("WaitAndSetFalse",SwipeDirection.Down);
		if(debugOutput) print("Down_Swipe");
	}
	
	void OnDownUnclick()
	{
		SendMessage("Down_Swipe_Release", SendMessageOptions.DontRequireReceiver);
		if(debugOutput) print("Down_Swipe_Release");
	    downIsClicked = false;
	}
	
	void Hand_Create(Vector3 pos)
	{
		leftIsClicked = false;
		rightIsClicked = false;
		upIsClicked = false;
		downIsClicked = false;
		
		Vector3 xDirection = (flipX)? Vector3.left : Vector3.right;
		Vector3 yDirection = (flipY)? Vector3.up   : Vector3.down;
		
		xSlider = new Slider(xDirection, pos, xSliderSize);
		xSlider.MoveTo(pos, xInitialValue);
		
		ySlider = new Slider(yDirection, pos, ySliderSize);
		ySlider.MoveTo(pos, yInitialValue);
		
		Hand_Update(pos);
	}
	
	void Hand_Update(Vector3 pos)
	{
		xSlider.MoveToContain(pos);
		ySlider.MoveToContain(pos);
		
		// get current progress
		xClickProgress = xSlider.GetValue(pos);
		yClickProgress = ySlider.GetValue(pos);
		
		// check x slider
		if (!rightIsClicked && !leftIsClicked)
        {
			if(debugOutput) print("Right isn't clicked and left isn't clicked in x slider.");
			//right click
            if (xClickProgress == 1.0f)
            {
				OnRightClick(pos);
            }
			
			//left click
			if (xClickProgress == 0.0f)
            {
				OnLeftClick(pos);
            }
        }
		/*
        else // leftIsClicked or rightIsClicked
        {
			if(rightIsClicked)
			{
				if(debugOutput) print("Right is clicked.");
				if (xClickProgress < (1.0f - xReleaseThreshold))
				{
					OnRightUnclick();
				}
			}
			else // leftIsClicked
	        {
				if(debugOutput) print("Left is clicked.");
				if (xClickProgress > xReleaseThreshold)
	            {
					OnLeftUnclick();
	            }
	        }
        }
        */
		
		//check y slider
		if (!upIsClicked && !downIsClicked)
        {
			if(debugOutput) print("Up is unclicked and down is unclicked in y slider.");
			//up click
            if (yClickProgress == 1.0f)
            {
				OnUpClick(pos);
			}
			
			//down click
			if (yClickProgress == 0.0f)
	        {
				OnDownClick(pos);
	        }
		}
		/*
		else // upIsClicked or downIsClicked
        {
			if(upIsClicked)
			{
			if(debugOutput) print("Up is clicked.");
				if (yClickProgress < (1.0f - yReleaseThreshold))
				{
					OnUpUnclick();
				}
			}
			else // downIsClicked
	        {
				if(debugOutput) print("Down is clicked.");
	            if (yClickProgress > yReleaseThreshold)
	            {
					OnDownUnclick();
	            }
	        }
        }
	    */ 
			
		// drift the slider to the initial position, if we aren't clicked
		if (!leftIsClicked && !rightIsClicked) 
		{
			//if(debugOutput) print("Drifting x...");
			float delta = xInitialValue - xClickProgress;
			xSlider.MoveTo(pos, xClickProgress + (delta * xDriftSpeed * Time.deltaTime));
		}
		
		// drift the slider to the initial position, if we aren't clicked
		if (!upIsClicked && !downIsClicked) 
		{
			//if(debugOutput) print("Drifting y...");
			float delta = yInitialValue - yClickProgress;
			ySlider.MoveTo(pos, yClickProgress + (delta * yDriftSpeed * Time.deltaTime));
		}
	}
	
	void Hand_Destroy()
	{
		
	}
}
