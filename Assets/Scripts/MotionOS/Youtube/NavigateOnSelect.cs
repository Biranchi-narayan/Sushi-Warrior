using UnityEngine;
using System.Collections;

public class NavigateOnSelect : MonoBehaviour {
	
	public NavigatorController nav;
	public Transform target;
	
	// bad naming - this comes from PushToSelect
	void Menu_SelectActive()
	{
		if (nav && target)
		{
			nav.NavigateTo(target);
		}
	}
}
