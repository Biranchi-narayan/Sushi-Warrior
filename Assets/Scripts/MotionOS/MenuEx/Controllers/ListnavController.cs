using UnityEngine;
using System.Collections;
using OpenNI;
using NITE;

public class ListnavController : NITEControl {
	
	public SelectableSlider1D mainSlider { get; private set; }
	public Axis SliderAxis = Axis.X;
	public float SliderSize = 300;

	public float InitialWaitTime = 0.6f;
	public float WaitTimeModifier = 0.75f;
	
	public float MinWaitTime = 0.15f;
	public float MaxWaitTime = 2.0f;
	
	public float scrollPortion = 0.2f;
	public bool Reverse = false;

	public float Value { get; private set; }
	protected float PreviousValue;
	protected float PreviousTime;
	public float Velocity { get; private set; }
	
	public float VelocityThreshold = 0.6f;
	protected bool VelocityScrolling = false;
	protected bool isVelocityScrollingEnabled = false;
	
	public float ValueFromCenter 
	{
		get 
		{
			return Value - 0.5f;
		}
	}
	
	
	void Awake ()
	{
		mainSlider = new SelectableSlider1D(1, scrollPortion, SliderAxis, false, 0, SliderSize, 0.5f, "Something");
		mainSlider.ValueChangeOnOffAxis = true;
        mainSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(mainSlider_PrimaryPointCreate);
		mainSlider.PrimaryPointDestroy += new System.EventHandler<IdEventArgs>(mainSlider_PrimaryPointDestroy);
		mainSlider.PrimaryPointUpdate += new System.EventHandler<HandEventArgs>(mainSlider_PrimaryPointUpdate);
        mainSlider.ValueChange += new System.EventHandler<ValueEventArgs>(mainSlider_ValueChange);
        mainSlider.Scroll += new System.EventHandler<ValueEventArgs>(mainSlider_Scroll);
        mainSlider.ItemHover += new System.EventHandler<IndexEventArgs>(mainSlider_ItemHover);
		
		AddListener(mainSlider);
	}

    void mainSlider_ItemHover(object sender, IndexEventArgs e)
    {
		StopScrolling();
	}

    void mainSlider_Scroll(object sender, ValueEventArgs e)
    {
		float scroll = e.Value;
		if (SliderAxis == Axis.X && !OpenNIContext.Mirror)
		{
			scroll = -scroll;
		}
		
		StartScrolling(scroll < 0);
    }
	
	void DoNext()
	{
		SendMessage("ListNav_Next", SendMessageOptions.DontRequireReceiver);
	}
	
	void DoPrev()
	{
		SendMessage("ListNav_Prev", SendMessageOptions.DontRequireReceiver);
	}
	
	IEnumerator NextLoop()
	{
		float waitTime = InitialWaitTime;
		while (true)
		{
			DoNext();
			yield return new WaitForSeconds(waitTime);
			waitTime = Mathf.Clamp(waitTime * WaitTimeModifier, MinWaitTime, MaxWaitTime);
		}
	}
	
	IEnumerator PrevLoop()
	{
		float waitTime = InitialWaitTime;
		while (true)
		{
			DoPrev();
			yield return new WaitForSeconds(waitTime);
			waitTime = Mathf.Clamp(waitTime * WaitTimeModifier, MinWaitTime, MaxWaitTime);
		}
	}

    void mainSlider_ValueChange(object sender, ValueEventArgs e)
    {
		PreviousValue = Value;
		
		Value = e.Value;
		if (SliderAxis == Axis.X && OpenNIContext.Mirror)
		{
			Value = 1.0f - Value;
		}
		
		//track timesteps
		float currentTime = Time.time;
		float timeStep = currentTime - PreviousTime;
		
		Velocity = ( Value - PreviousValue ) / timeStep;
		
		PreviousTime = currentTime;
		
		if(isVelocityScrollingEnabled)
		{
			if(Velocity > VelocityThreshold)//keep close track of signs here
			{
				StartVelocityScrolling(false);
			}else if(Velocity < -VelocityThreshold) {
				StartVelocityScrolling(true);
			}else{
				StopVelocityScrolling();
			}
		}
    }
	
	void StartVelocityScrolling(bool forward) 
	{
		if(VelocityScrolling)
			return;
		VelocityScrolling = true;
		print("Velocity scrolling...");
		StartScrolling(forward);
	}

	void StopVelocityScrolling ()
	{
		if(!VelocityScrolling)
			return;
		print("Stopped velocity scrolling.");
		StopScrolling();
		VelocityScrolling = false;
	}

	bool scrolling = false;
	
	void StartScrolling(bool forward)
	{
		if (scrolling) return;
		scrolling = true;
		
		SendMessage("ListNav_StartScrolling", forward, SendMessageOptions.DontRequireReceiver);
		if (forward ^ Reverse)
		{
			StartCoroutine("NextLoop");
		}
		else
		{
			StartCoroutine("PrevLoop");
		}
	}
	
	void StopScrolling ()
	{
		if (!scrolling)
			return;
		
		StopCoroutine ("NextLoop");
		StopCoroutine ("PrevLoop");
		SendMessage ("ListNav_StopScrolling", SendMessageOptions.DontRequireReceiver);
		scrolling = false;
	}
	
    void mainSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        mainSlider.Center = focusPoint;
		SendMessage("ListNav_Activate", SendMessageOptions.DontRequireReceiver);
    }
	
	void mainSlider_PrimaryPointDestroy(object sender, IdEventArgs e)
	{
		StopScrolling();
		SendMessage("ListNav_Deactivate", SendMessageOptions.DontRequireReceiver);
	}
	
	void mainSlider_PrimaryPointUpdate(object sender, HandEventArgs e)
	{
		// drift the slider when scrolling, so its always responsive to stop scrolling
		/*if (scrolling)
		{
			Vector3 currentCenter = new Vector3(mainSlider.Center.X, mainSlider.Center.Y, mainSlider.Center.Z);
			Vector3 currentHandPosition = new Vector3(e.Hand.Position.X, e.Hand.Position.Y, e.Hand.Position.Z);
		
			Vector3 distance = currentCenter - currentHandPosition;
			if (distance.magnitude > 
		*/
	}
	
	void Update()
	{
	}
	
	void OnGUI()
	{
		// keyboard
		if (Event.current.type == EventType.KeyDown)
		{
			if ((SliderAxis == Axis.Y && Event.current.keyCode == KeyCode.UpArrow) ||
				(SliderAxis == Axis.X && Event.current.keyCode == KeyCode.LeftArrow))
			{
				if (Reverse) {
					DoPrev();
				} else {
					DoNext();
				}
				Event.current.Use();
			}
			
			if ((SliderAxis == Axis.Y && Event.current.keyCode == KeyCode.DownArrow) ||
				(SliderAxis == Axis.X && Event.current.keyCode == KeyCode.RightArrow))
			{
				if (Reverse) {
					DoNext();
				} else {
					DoPrev();
				}
				Event.current.Use();
			}
		}	

		
		if (!SessionManager) return;
		if (SessionManager.IsInSession)
		{
			switch (SliderAxis)
			{
				case Axis.X:
				case Axis.Z:
					GUIVisualizers.ListnavVisualizer(new Rect((Screen.width - SliderSize) / 2, Screen.height - 150, SliderSize, 20), this);
					break;
				case Axis.Y:
					GUIVisualizers.ListnavVisualizer(new Rect((Screen.width - 20) / 2, (Screen.height - SliderSize) / 2, 20, SliderSize), this);
					break;
			}
		}
	}
}
