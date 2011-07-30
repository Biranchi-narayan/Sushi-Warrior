using UnityEngine;
using System.Collections;

public class DummyMenuFeed : MonoBehaviour {

	public MenuBase Menu;
	public int InitialItems = 6;
	public int ChunkItems = 3;
		
	int currentIndex = 0;
	
	// Use this for initialization
	void Start () {
		if (!Menu)
		{
			Menu = GetComponent<MenuBase>();
			if (!Menu)
			{
				print("I need a menu");
				return;
			}
		}
		
		for (int i=0; i<InitialItems; i++)
		{
			Menu.AddToEnd(currentIndex.ToString());
			currentIndex++;
		}
	}
	
	void Menu_OutOfBounds(bool forwards)
	{
		if (forwards)
		{
			for (int i=0; i<ChunkItems; i++)
			{
				Menu.AddToEnd(currentIndex.ToString());
				currentIndex++;
			}
		}
	}
}
