using UnityEngine;
using System.Collections;

public class PushDetector : MyHandPointControl {
	public float size = 150.0f;
	public float initialValue = 0.2f;
	public float driftSpeed = 1.5f;
	
	public float clickTimeFrame = 1.0f;
	public float clickPushTime { get; private set; }
	
	public bool IsClicked { get; private set; }
	public Vector3 ClickPosition { get; private set; }
	public float ClickProgress { get; private set; }
	
	Slider slider;
	public Graphable zGraphable;
	
	void Hand_Create(Vector3 pos)
	{
		slider = new Slider(-Vector3.forward, pos, size);
		slider.MoveTo(pos, initialValue);
		Hand_Update(pos);
	}
	
	void Hand_Update(Vector3 pos)
	{
		// move slider if hand is out of its bounds (that way it always feels responsive)
		slider.MoveToContain(pos);
		
		// get current progress
		ClickProgress = slider.GetValue(pos);		
		if (zGraphable)
		{
			zGraphable.setGraphValue(ClickProgress);
		}
		
		// click logic
		if (!IsClicked)
        {
            if (ClickProgress == 1.0f)
            {
				ClickPosition = pos;
                IsClicked = true;
				clickPushTime = Time.time;
				SendMessage("PushDetector_Push", SendMessageOptions.DontRequireReceiver);
            }
        }
        else // clicked
        {
            if (ClickProgress < 0.5)
            {
				if( Time.time < clickPushTime + clickTimeFrame )
				{
					SendMessage("PushDetector_Click",SendMessageOptions.DontRequireReceiver);
				}
				
				SendMessage("PushDetector_Release", SendMessageOptions.DontRequireReceiver);
                IsClicked = false;
            }
        }
		
		// drift the slider to the initial position, if we aren't clicked
		if (!IsClicked) {
			float delta = initialValue - ClickProgress;
			slider.MoveTo(pos, ClickProgress + (delta * driftSpeed * Time.deltaTime));
		}	
	}
}
