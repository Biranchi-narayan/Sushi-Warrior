using UnityEngine;
using System.Collections;

public class Graphable : MonoBehaviour {

	public float graphValue;
	public float minValue;
	public float maxValue;
	public bool debugOutput = true;
	
	public void setGraphValue(float newValue)
	{
		if(debugOutput) print("Graphable newValue (unclamped): "+newValue);
		graphValue = Mathf.Clamp(newValue,minValue,maxValue);
		gameObject.SendMessage("GraphUpdate",graphValue,SendMessageOptions.DontRequireReceiver);
	}
	
	void Start()
	{
		graphValue = ( minValue + maxValue ) / 2.0f;
	}
}
