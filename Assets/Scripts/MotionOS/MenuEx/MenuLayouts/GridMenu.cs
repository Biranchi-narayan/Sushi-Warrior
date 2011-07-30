using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridMenu : MenuBase
{
	public int rows;
	public int cols;
	public Vector2 itemSize;
	public float padding;
	
	protected override void LayoutChildren()
	{
		Vector3 current = new Vector3(0,0,0);
		
		int row = 0;
		int col = 0;
		foreach (Transform child in Children)
		{
			current.x += padding;
			child.localPosition = current;
			current.x += itemSize.x;
			
			if (++col >= cols)
			{
				if (++row >= rows) break;
				current.x = 0;
				current.y += itemSize.y + padding;
				col = 0;
			}
		}
	}
	
	void ItemSelector_Select(ItemSelector2D selector)
	{
		ActiveItemIndex = (selector.selectionIndexY * cols) + selector.selectionIndexX;
	}	
}