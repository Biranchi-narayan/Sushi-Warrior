using UnityEngine;
using System.Collections;

public class ChangeZMenuItem : MonoBehaviour {

	public float HighlightedZ = -2.0f;
	public float UnhighlightedZ = 0.0f;
	public float Rate = 5.0f;
	
	Vector3 targetPos;
	
	// Use this for initialization
	void Start () {
		targetPos = transform.localPosition;
	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.deltaTime * Rate);
	}
	
	void MenuItem_Activate()
	{
		targetPos = transform.localPosition;
		targetPos.z = HighlightedZ;
	}
	
	void MenuItem_Deactivate()
	{
		targetPos = transform.localPosition;
		targetPos.z = UnhighlightedZ;
	}
}
