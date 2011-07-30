using UnityEngine;
using System.Collections;

public class NavigateToOnSelect : MonoBehaviour {

	public NavigatorController Navigator;
	public Transform Target;
	
	// Use this for initialization
	void Start () {
		if (!Navigator)
		{
			print("Need navigator!");
		}
		
		if (!Target)
		{
			print("Need target");
		}
	}

	void MenuItem_Select()
	{
		Navigator.NavigateTo(Target);
	}
}
