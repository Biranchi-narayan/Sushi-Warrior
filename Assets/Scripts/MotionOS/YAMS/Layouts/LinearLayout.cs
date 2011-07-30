using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LinearLayout : LayoutBase
{
	public Vector3 Direction;
	
	public override void LayoutItems(List<Transform> items)
	{
		Vector3 current = new Vector3(0,0,0);
		foreach (Transform item in items)
		{
			item.localPosition = current;
			current += Direction;
		}
	}
}