using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SelectableMenu : MonoBehaviour {
	public LayoutBase MenuLayout;
	public Transform ActiveItem {get; private set; }

	// we keep an ordered list of children. the transform.children isn't ordered!
	List<Transform> Children = new List<Transform>();
	
	int lastActiveItemIndex = 0;
	int activeItemIndex = -1;
	
	public int ActiveItemIndex
	{
		get { return activeItemIndex; }
		set
		{
			int clamped = (int)Mathf.Clamp(value, 0, Children.Count - 1);
			if (clamped != activeItemIndex)
			{
				Deactivate();
				ActivateItem(clamped);
			}
			
			if (value != activeItemIndex)
			{
				OutOfBounds(value - activeItemIndex > 0);
			}
		}
	}
	
	public void AddToEnd(Transform child)
	{
		InsertChild(child, Children.Count);
	}
	
	public void AddToStart(Transform child)
	{
		InsertChild(child, 0);
	}
	
	public void SelectActive()
	{
		if (null == ActiveItem) return;
		ActiveItem.SendMessage("MenuItem_Select", SendMessageOptions.DontRequireReceiver);
		SendMessage("Menu_Select", ActiveItem, SendMessageOptions.DontRequireReceiver);
	}

	void InsertChild(Transform child, int index)
	{
		Children.Insert(index, child);
		if (null != MenuLayout)
		{
			MenuLayout.LayoutItems(Children);
		}
	}
	
	void ActivateItem(int index)
	{
		activeItemIndex = index;
		ActiveItem = Children[activeItemIndex];
		
		ActiveItem.SendMessage("MenuItem_Activate", SendMessageOptions.DontRequireReceiver);
		SendMessage("Menu_Activate", ActiveItem, SendMessageOptions.DontRequireReceiver);
	}
	
	void Deactivate()
	{
		if (activeItemIndex == -1)
		{
			return;
		}
		
		Children[activeItemIndex].SendMessage("MenuItem_Deactivate", SendMessageOptions.DontRequireReceiver);
		SendMessage("Menu_Deactivate", Children[activeItemIndex], SendMessageOptions.DontRequireReceiver);
		lastActiveItemIndex = activeItemIndex;
		activeItemIndex = -1;
	}
	
	
	// NOTE: I think this belongs in the Layout code, not in the menu
	void OutOfBounds(bool forwards)
	{
		SendMessage("Menu_OutOfBounds", forwards, SendMessageOptions.DontRequireReceiver);
	}
	
	// consume hand events
	void Hand_Create()
	{
		ActivateItem(lastActiveItemIndex);
	}
	
	void Hand_Destroy()
	{
		Deactivate();
	}
	
	void ListNav_Next()
	{
		ActiveItemIndex++;	
	}
	
	void ListNav_Prev()
	{
		ActiveItemIndex--;
	}
	
	void PushDetector_Click()
	{
		SelectActive();
	}
	
	void Menu_SelectActive()
	{
		SelectActive();
	}
}
