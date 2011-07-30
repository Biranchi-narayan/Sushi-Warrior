using UnityEngine;
using System.Collections;

public class ItemSelector : MyHandPointControl {

	public Fader fader;
	
	public int numItems = 3;
	public int selectionIndex = 0;
	public float hysterisis = .1f; //percent expansion
	float minValue;
	float maxValue;
	void Update(){
		float width = (float) 1.0f / numItems;
	
		minValue = (selectionIndex * width) - hysterisis*width;			
		maxValue =((selectionIndex+1) * width) + hysterisis*width;
		if (fader.value < minValue)
		{
			Prev();
		}
		else if (fader.value > maxValue)
		{
			Next();
		}
			
	}
	
	void Hand_Create(Vector3 pos)
	{
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void Prev()
	{
		selectionIndex--;
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
	void Next()
	{
		selectionIndex++;
		SendMessage("ItemSelector_Select", this, SendMessageOptions.DontRequireReceiver);
	}
}