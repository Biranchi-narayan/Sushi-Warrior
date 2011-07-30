using UnityEngine;
using System.Collections;

public class ItemSelector2D : ItemSelector {

	public Fader faderX;
	public Fader faderY;
	
	public int numItemsX = 3;
	public int numItemsY = 2;
	public int selectionIndexX = 0;
	public int selectionIndexY = 0;

	public float hysterisisX = .1f; //percent expansion
	public float hysterisisY = .1f; //percent expansion
	float minValueX;
	float maxValueX;
	float minValueY;
	float maxValueY;
	void Update(){
		float width = (float) 1.0f / numItemsX;
		float height = (float) 1.0f / numItemsY;
		
		minValueX = (selectionIndexX * width) - hysterisisX*width;			
		maxValueX =((selectionIndexX+1) * width) + hysterisisY*width;
		
		minValueY = (selectionIndexY * height) - hysterisisY*height;			
		maxValueY =((selectionIndexY+1) * height) + hysterisisY*height;
		
		
		if (faderX.value < minValueX)
		{
			Left();
		}
		else if (faderX.value > maxValueX)
		{
			Right();
		}
		if (faderY.value < minValueY)
		{
			Up();
		}
		else if (faderY.value > maxValueY)
		{
			Down();
		}
			
	}
	
	void Hand_Create(Vector3 pos)
	{
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void Left()
	{
		selectionIndexX--;
		selectionIndex--;
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void Right()
	{
		selectionIndexX++;
		selectionIndex++;
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void Up()
	{
		selectionIndexY--;	//top left to 00
		
		selectionIndex -= numItemsX;
		//Debug.Log("UP SelectionIndexX: " + selectionIndexX.ToString() + "SelectionIndexY: " + selectionIndexY.ToString() + "SelectionIndex: " + selectionIndex.ToString());
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}

	void Down()
	{
		selectionIndexY++;
		
		selectionIndex += numItemsX;
		//Debug.Log("DOWN SelectionIndexX: " + selectionIndexX.ToString() + "SelectionIndexY: " + selectionIndexY.ToString() + "SelectionIndex: " + selectionIndex.ToString());
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void OnGUI()
	{
		GUI.Label(new Rect(10, 25, 300, 20), "minx: " + minValueX.ToString() + " maxx: " + maxValueX.ToString());
		GUI.Label(new Rect(10, 50, 300, 20), "miny: " + minValueY.ToString() + " maxy: " + maxValueY.ToString());
	}
}