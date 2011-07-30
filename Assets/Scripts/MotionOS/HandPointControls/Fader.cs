using UnityEngine;
using System.Collections;

public enum FaderOrientation
{
	Vertical,
	Horizontal,
}

public class Fader : MyHandPointControl 
{
	Slider slider;
	
	public float value { get; private set; }
	
	public float size = 250.0f; //mm of area around hand
	public FaderOrientation orientation = FaderOrientation.Horizontal;
	public float initialValue = 0.5f;
	
	void Hand_Create(Vector3 pos)
	{
		Vector3 dir = new Vector3(0,0,0);
		switch (orientation)
		{
			case FaderOrientation.Vertical: dir = new Vector3(0,-1,0); break;
			case FaderOrientation.Horizontal: dir = (OpenNIContext.Mirror) ? new  Vector3(1,0,0) : new Vector3(-1,0,0); break;
		}
			
		slider = new Slider(dir, FocusPoint, size);
		slider.MoveTo(FocusPoint, initialValue);
		Hand_Update(pos);
	}
	
	void Hand_Update(Vector3 pos)
	{
		value = slider.GetValue(pos);
		SendMessage("Fader_ValueChanged", value, SendMessageOptions.DontRequireReceiver);
	}
}
