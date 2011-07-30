using UnityEngine;
using System.Collections;

public class FeedBase : MonoBehaviour {
	public SelectableMenu menu;
	public Transform MenuItemAsset;
	
	void Awake()
	{
		if (null == menu)
		{
			menu = GetComponent<SelectableMenu>();
			if (null == menu)
			{
				Debug.LogError("No menu object found for this feed");
			}
		}
	}
	
	GameObject ItemsParent;
	
	protected Transform CreateItem(object feedItemContext)
	{
		if (null == ItemsParent)
		{
			ItemsParent = new GameObject();
			ItemsParent.name = "FeedItems";
			ItemsParent.transform.parent = transform;
			ItemsParent.transform.position = transform.position;
			ItemsParent.transform.rotation = transform.rotation;
		}
		
		Transform childTransform = Instantiate(MenuItemAsset) as Transform;
		childTransform.transform.parent = ItemsParent.transform;
		childTransform.gameObject.SendMessage("FeedItem_Init", feedItemContext);

		return childTransform;
	}
}
