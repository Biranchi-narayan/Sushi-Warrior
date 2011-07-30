using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class MenuBase : MonoBehaviour 
{
	public Transform MenuItem;
	public Transform ActiveItem {get; private set; }

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

	// we keep an ordered list of children. the transform.children isn't ordered!
	protected List<Transform> Children = new List<Transform>();

	public void AddToEnd(object child)
	{
		InsertChild(child, Children.Count);
	}
	
	public void AddToStart(object child)
	{
		InsertChild(child, 0);
	}
	
	void InsertChild(object child, int index)
	{
		Transform childTransform = Instantiate(MenuItem) as Transform;
		childTransform.transform.parent = transform;
		Children.Insert(index, childTransform);
		childTransform.SendMessage("MenuItem_Init", child, SendMessageOptions.DontRequireReceiver);
		SendMessage("Menu_NewItem", childTransform, SendMessageOptions.DontRequireReceiver);
		LayoutChildren();
	}
	
	protected abstract void LayoutChildren();
	
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
	
	void ActivateItem(int index)
	{
		if(index == -1 || index >= Children.Count) {
			print("Tried to activate a non-existent item... How embarassing.");
			return;
		}
		activeItemIndex = index;
		ActiveItem = Children[activeItemIndex];
		ActiveItem.SendMessage("MenuItem_Activate", SendMessageOptions.DontRequireReceiver);
		SendMessage("Menu_Activate", ActiveItem, SendMessageOptions.DontRequireReceiver);
	}
	
	void OutOfBounds(bool forwards)
	{
		SendMessage("Menu_OutOfBounds", forwards, SendMessageOptions.DontRequireReceiver);
	}
		
	void Hand_Create()
	{
		ActivateItem(lastActiveItemIndex);
	}
	
	void Hand_Destroy()
	{
		Deactivate();
	}
	
	void Menu_SelectActive()
	{
		ActiveItem.SendMessage("MenuItem_Select", SendMessageOptions.DontRequireReceiver);
	}
}
