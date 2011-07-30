using UnityEngine;
using System.Collections;

public class faderToGlow : MyHandPointControl {

	public Fader fader;
	public Transform linearTrack;
	public float minX = -2.5f;
	public float maxX = 2.5f;
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
		linearTrack.localPosition = new Vector3(Mathf.Lerp(minX,maxX,fader.value),lp.y,lp.z);
		
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
	
	Transform currentSelection()
	{
		return items[(int)Mathf.Round ( fader.value * (items.Length-1) )];
	}
	
	void glowEnable(Transform t)
	{
		t.gameObject.layer = LayerMask.NameToLayer("glow");
	}
	void glowDisable(Transform t)
	{
		t.gameObject.layer = LayerMask.NameToLayer("Default");
	}
	
}
