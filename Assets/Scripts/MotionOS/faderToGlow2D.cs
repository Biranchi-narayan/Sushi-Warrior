using UnityEngine;
using System.Collections;

public class faderToGlow2D : MyHandPointControl {

	public Fader faderX;
	public Fader faderY;
	public Transform linearTrack;
	public Vector2 minPos = new Vector2(-2.5f,-1.7f);
	public Vector2 maxPos = new Vector2(2.5f, 1.2f);
	// Use this for initialization
	void Start () {
	
	}
	void Hand_Create(Vector3 pos)
	{
		Debug.Log("hand create");
		//glowEnable(currentSelection());
		linearTrack.gameObject.SetActiveRecursively(true);
	}
	void Hand_Destroy()
	{
		glowDisable(prevSelection);
		linearTrack.gameObject.SetActiveRecursively(false);
	}
	void Hand_Update(Vector3 pos)
	{
	/*	if (this.currentSelection() != prevSelection)
		{
			glowDisable(prevSelection);
			prevSelection = currentSelection();
			glowEnable(prevSelection);
		}
		*/
		Vector3 lp = linearTrack.localPosition;
		linearTrack.localPosition = new Vector3(Mathf.Lerp(minPos.x,maxPos.x,faderX.value),Mathf.Lerp(maxPos.y,minPos.y,faderY.value),lp.z);
		
	}
	
	void ItemSelector_Select(ItemSelector itemSelector)
	{
		glowDisable(prevSelection);
		prevSelection = items[itemSelector.selectionIndex];
		glowEnable(prevSelection);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public Transform prevSelection;
	public Transform[] items;
	
	void glowEnable(Transform t)
	{
		t.gameObject.layer = LayerMask.NameToLayer("glow");
	}
	void glowDisable(Transform t)
	{
		t.gameObject.layer = LayerMask.NameToLayer("Default");
	}
	void PushDetector_Click()
	{
		prevSelection.SendMessage("MenuItem_Select",SendMessageOptions.DontRequireReceiver);
	}
}
