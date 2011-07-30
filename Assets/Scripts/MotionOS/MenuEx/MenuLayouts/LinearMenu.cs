using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinearMenu : MenuBase
{
	public Vector3 Direction;
	
	protected override void LayoutChildren()
	{
		Vector3 current = new Vector3(0,0,0);
		foreach (Transform child in Children)
		{
			child.localPosition = current;
			current += Direction;
		}
	}
	
	void ListNav_Next()
	{
		ActiveItemIndex++;	
	}
	
	void ListNav_Prev()
	{
		ActiveItemIndex--;
	}
}