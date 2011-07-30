using UnityEngine;
using System.Collections;
//using System.Reflection;

public class disableChildren : MonoBehaviour {

	// Use this for initialization
	void Start () {
		disableChildrenRenderer = disableOnStart;
	}
	
	
	public bool disableChildrenRenderer = false;
	public bool disableOnStart = true;
	bool childrenDisabled = false;
	public bool onlyImmediateChildren = true;
	// Update is called once per frame
	void Update () {
		if ((disableChildrenRenderer) != (childrenDisabled))
		{
			
			//perhaps try with System.Reflection: Type componentType = Assembly.GetExecutingAssembly.GetType (myType)
			foreach (Renderer child in GetComponentsInChildren<Renderer>())
			{
				if ((onlyImmediateChildren) && (child.transform.parent == transform))
				{
					child.enabled = !disableChildrenRenderer;
				}
			}
							
		}			
			

	}
}
