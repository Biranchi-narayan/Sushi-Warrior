using UnityEngine;
using System.Collections;

public class ItemSelectorMenu : MonoBehaviour {

	// Use this for initialization
	void OnEnable () {
		Debug.Log("MenuItem_Select sent");
		activeItem.SendMessage("MenuItem_Select",  SendMessageOptions.DontRequireReceiver);
	}
	public Transform[] itemList;
	public Transform activeItem;
	void ItemSelector_Select(int selectionIndex)
	{
		
		activeItem.SendMessage("MenuItem_Deselect",  SendMessageOptions.DontRequireReceiver);
		activeItem = itemList[selectionIndex];		
		activeItem.SendMessage("MenuItem_Select",  SendMessageOptions.DontRequireReceiver);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	
}
