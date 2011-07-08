using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITESoftbar : NITEControl {

	SelectableSlider1D mainSlider;
	
	public Axis SliderAxis = Axis.X;
	public float SliderSize = 200;
	
	private float value;
	public float Value
	{
		get { return value; }
	}
	
	void Awake ()
	{
		mainSlider = new SelectableSlider1D(3, 0.3f, SliderAxis, false, 0, SliderSize, 0.5f, "Something");
		mainSlider.ValueChangeOnOffAxis = true;
        mainSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(mainSlider_PrimaryPointCreate);
        mainSlider.ValueChange += new System.EventHandler<ValueEventArgs>(mainSlider_ValueChange);
        mainSlider.Scroll += new System.EventHandler<ValueEventArgs>(mainSlider_Scroll);
        mainSlider.ItemHover += new System.EventHandler<IndexEventArgs>(mainSlider_ItemHover);
		
		AddListener(mainSlider);
	}

    void mainSlider_ItemHover(object sender, IndexEventArgs e)
    {
        print("Item hover: " + e.Item);
    }

    void mainSlider_Scroll(object sender, ValueEventArgs e)
    {
        print("Scroll: " + e.Value);
    }

    void mainSlider_ValueChange(object sender, ValueEventArgs e)
    {
        value = e.Value;
    }

    void mainSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        mainSlider.Center = focusPoint;
    }
	
	// Update is called once per frame
	void Update () 
	{
	
	}
	
}
