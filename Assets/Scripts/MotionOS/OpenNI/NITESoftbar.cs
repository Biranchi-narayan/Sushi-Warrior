using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITESoftbar : NITEControl {

	public SelectableSlider1D mainSlider { get; private set; }
	
	public Axis SliderAxis = Axis.X;
	public float SliderSize = 200;
	public int SliderItems = 3;
	
	private float value;
	public float Value
	{
		get { return value; }
	}
	
	void Awake ()
	{
		mainSlider = new SelectableSlider1D(SliderItems, 0.3f, SliderAxis, false, 0, SliderSize, 0.5f, "Something");
		mainSlider.ValueChangeOnOffAxis = true;
        mainSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(mainSlider_PrimaryPointCreate);
		mainSlider.PrimaryPointDestroy += new System.EventHandler<IdEventArgs>(mainSlider_PrimaryPointDestroy);
        mainSlider.ValueChange += new System.EventHandler<ValueEventArgs>(mainSlider_ValueChange);
        mainSlider.Scroll += new System.EventHandler<ValueEventArgs>(mainSlider_Scroll);
        mainSlider.ItemHover += new System.EventHandler<IndexEventArgs>(mainSlider_ItemHover);
		
		AddListener(mainSlider);
	}

    void mainSlider_ItemHover(object sender, IndexEventArgs e)
    {
		int item = e.Item;
		if (SliderAxis == Axis.X && !OpenNIContext.Mirror)
		{
			item = SliderItems - item - 1;
		}
        SendMessage("Softbar_OnItemHover", item, SendMessageOptions.DontRequireReceiver);
    }

    void mainSlider_Scroll(object sender, ValueEventArgs e)
    {
		SendMessage("Softbar_OnItemScroll", e.Value, SendMessageOptions.DontRequireReceiver);
    }

    void mainSlider_ValueChange(object sender, ValueEventArgs e)
    {
		value = e.Value;
		if (SliderAxis == Axis.X && OpenNIContext.Mirror)
		{
			value = 1.0f - value;
		}
    }

    void mainSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        mainSlider.Center = focusPoint;
		SendMessage("Softbar_Activate", SendMessageOptions.DontRequireReceiver);
    }
	
	void mainSlider_PrimaryPointDestroy(object sender, IdEventArgs e)
	{
		SendMessage("Softbar_Deactivate", SendMessageOptions.DontRequireReceiver);
	}
}
