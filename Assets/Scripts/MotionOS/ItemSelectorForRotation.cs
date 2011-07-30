using UnityEngine;
using System.Collections;

public class ItemSelectorForRotation : MonoBehaviour {

	public rotateToSelect fader;
	
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
			
		guiText.text = //measureRotation.rotation.eulerAngles.ToString() +
			" v: " + fader.value.ToString()	
			+ "k: " + selectionIndex.ToString();
	}
	
	void Prev()
	{
		selectionIndex--;
		SendMessage("ItemSelector_Select", selectionIndex, SendMessageOptions.DontRequireReceiver);
	}
	void Next()
	{
		selectionIndex++;
		SendMessage("ItemSelector_Select", selectionIndex, SendMessageOptions.DontRequireReceiver);
	}
}