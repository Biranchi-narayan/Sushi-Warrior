using UnityEngine;
using System;

public class CircularMenu : MenuBase
{
	public float Radius = 5.0f;
	
	protected override void LayoutChildren()
	{
		int index = 0;
		foreach (Transform child in Children)
		{
			float angle = (float)index * 2 * Mathf.PI / (float)Children.Count;
			Vector3 pos = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * Radius;
			
			child.localPosition = pos;
			index++;
		}
	}
}

