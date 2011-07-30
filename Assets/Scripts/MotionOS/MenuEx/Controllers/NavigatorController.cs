using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NavigatorController : MonoBehaviour {
	public bool ActiveOnStart = false;
	public Transform HomeScreen;
	public Transform ActiveItem { get; private set; }

	List<Transform> historyStack = new List<Transform>();

	public void NavigateTo(Transform obj)
	{
		DeactivateItem(ActiveItem);
		historyStack.Add(obj);
		ActivateItem(obj);
	}
	
	public void NavigateBack()
	{
		if (historyStack.Count <= 1) return;
		
		Transform obj = historyStack[historyStack.Count - 1];
		historyStack.RemoveAt(historyStack.Count - 1);
		DeactivateItem(ActiveItem);
	}
	
	public void NavigateHome()
	{
		historyStack.Clear();
		NavigateTo(HomeScreen);
	}
	
	void ActivateItem(Transform obj)
	{
		ActiveItem = obj;
		print("Navigator: Activating item " + obj);
		obj.SendMessage("Navigator_Activate", SendMessageOptions.DontRequireReceiver);
		SendMessage("Navigator_ActivatedItem", obj, SendMessageOptions.DontRequireReceiver);
	}
	
	void DeactivateItem(Transform obj)
	{
		if (!obj) return;
		obj.SendMessage("Navigator_Deactivate", SendMessageOptions.DontRequireReceiver);
	}
	
	void Start()
	{
		if (ActiveOnStart)
		{
			NavigateHome();
		}
	}
	
	void OnGUI()
	{
		if (Event.current.Equals(Event.KeyboardEvent("h")))
		{
			NavigateHome();
		}
		
		if (Event.current.Equals(Event.KeyboardEvent("n")))
		{
			NavigateTo(transform.Find("Menu1"));
		}
		
		if (Event.current.Equals(Event.KeyboardEvent("m")))
		{
			NavigateTo(transform.Find("Menu2"));
		}
	}
	
	// allow nesting of NavigatorController's
	void Navigator_Activate()
	{
		if (null != ActiveItem)
		{
			ActivateItem(ActiveItem);
		}
	}
	
	void Navigator_Deactivate()
	{
		if (null != ActiveItem)
		{
			DeactivateItem(ActiveItem);
		}
	}
}
