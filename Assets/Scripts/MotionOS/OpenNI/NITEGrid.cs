using UnityEngine;
using System.Collections;

using OpenNI;
using NITE;

public class NITEGrid : NITEControl {

	SelectableSlider2D mainSlider;
	
	public Vector2 SliderSize = new Vector2(300.0f, 200.0f);
	public Vector2 SliderItems = new Vector2(3, 2);
	
	private Vector2 value;
	public Vector2 Value
	{
		get { return value; }
	}
	
	void Awake ()
	{
		mainSlider = new SelectableSlider2D((int)SliderItems.x, (int)SliderItems.y, SliderSize.x, SliderSize.y, 0.3f, "Something");
		mainSlider.ValueChangeOnOffAxis = true;
        mainSlider.PrimaryPointCreate += new System.EventHandler<HandFocusEventArgs>(mainSlider_PrimaryPointCreate);
        mainSlider.ValueChange += new System.EventHandler<Value2EventArgs>(mainSlider_ValueChange);
        mainSlider.Scroll += new System.EventHandler<Value2EventArgs>(mainSlider_Scroll);
        mainSlider.ItemHover += new System.EventHandler<Index2EventArgs>(mainSlider_ItemHover);
		
		AddListener(mainSlider);
	}

    void mainSlider_ItemHover(object sender, Index2EventArgs e)
    {
		Vector2 hoveredItem = new Vector2(e.IndexX, e.IndexY);
		if (!OpenNIContext.Mirror)
		{
			hoveredItem.x = SliderItems.x - hoveredItem.x - 1;
		}
		//print("Hover: " + hoveredItem);
        SendMessage("Grid_OnItemHover", hoveredItem, SendMessageOptions.DontRequireReceiver);
    }

    void mainSlider_Scroll(object sender, Value2EventArgs e)
    {
        
    }

    void mainSlider_ValueChange(object sender, Value2EventArgs e)
    {
		value.x = e.ValueX;
		value.y = e.ValueY;

		if (OpenNIContext.Mirror)
		{
			value.x = 1.0f - value.x;
		}
    }

    void mainSlider_PrimaryPointCreate(object sender, HandFocusEventArgs e)
    {
        Point3D focusPoint = SessionManager.FocusPoint;
        mainSlider.Center = focusPoint;
    }
}
