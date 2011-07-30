using UnityEngine;
using System.Collections;
using OpenNI;
using NITE;

public class GUIVisualizers
{
	public static void SoftbarVisualizer(Rect pos, NITESoftbar softbar)
	{
		GUI.Box(pos, "");
		
		if (softbar.SliderAxis == Axis.X)
		{
			// start with slider
			GUI.HorizontalSlider(pos, softbar.Value, 1.0f, 0);
		
			// boxes for scroll regions
			float scrollRegionSize = softbar.mainSlider.BorderWidth * pos.width;
			if (scrollRegionSize > 0)
			{
				GUI.Box(new Rect(pos.x, pos.y, scrollRegionSize, pos.height), "L");
				GUI.Box(new Rect(pos.x + pos.width - scrollRegionSize, pos.y, scrollRegionSize, pos.height), "R");
			}
		}
		else
		{
			GUI.VerticalSlider(pos, softbar.Value, 1.0f, 0);

			// boxes for scroll regions
			float scrollRegionSize = softbar.mainSlider.BorderWidth * pos.width;
			if (scrollRegionSize > 0)
			{
				GUI.Box(new Rect(pos.x, pos.y, pos.width, scrollRegionSize), "T");
				GUI.Box(new Rect(pos.x, pos.y + pos.height - scrollRegionSize, pos.width, scrollRegionSize), "B");
			}
		}
	}
	
	public static void ListnavVisualizer(Rect pos, ListnavController listnav)
	{
		GUI.Box(pos, "");
		
		if (listnav.SliderAxis == Axis.X)
		{
			// start with slider
			GUI.HorizontalSlider(pos, listnav.Value, 1.0f, 0);
		
			// boxes for scroll regions
			float scrollRegionSize = listnav.mainSlider.BorderWidth * pos.width;
			if (scrollRegionSize > 0)
			{
				GUI.Box(new Rect(pos.x, pos.y, scrollRegionSize, pos.height), "L");
				GUI.Box(new Rect(pos.x + pos.width - scrollRegionSize, pos.y, scrollRegionSize, pos.height), "R");
			}
		}
		else
		{
			GUI.VerticalSlider(pos, listnav.Value, 1.0f, 0);

			// boxes for scroll regions
			float scrollRegionSize = listnav.mainSlider.BorderWidth * pos.width;
			if (scrollRegionSize > 0)
			{
				GUI.Box(new Rect(pos.x, pos.y, pos.width, scrollRegionSize), "T");
				GUI.Box(new Rect(pos.x, pos.y + pos.height - scrollRegionSize, pos.width, scrollRegionSize), "B");
			}
		}
	}

}

