using UnityEngine;
using System.Collections;

public class CameraLookAtActive : MonoBehaviour {
	public SmoothLookAt targetCamera;
		// Use this for initialization
	
	void Start () {
		if (targetCamera == null)
		{
			Debug.LogWarning("Camera effect on GameObject " + gameObject.name + " is Missing a Camera, resorting to Main Camera!");
			targetCamera = Camera.main.GetComponent<SmoothLookAt>();
			if (targetCamera == null)
			{
				Debug.LogWarning("SmoothLookAt Navigator effect on GameObject " + gameObject.name + " is Missing a SmoothLookAt Camera, adding one to Main Camera!");
				targetCamera = Camera.main.gameObject.AddComponent(typeof(SmoothLookAt)) as SmoothLookAt;
				
			}
		}
	}
	
	// Update is called once per frame
	
	void Update()
	{

	}
	
	void Navigator_ActivatedItem(Transform child)
	{
		Debug.Log("Activated Image: " + child.gameObject.name);
		targetCamera.target= child;
	}
	
	void Navigator_Activate()
	{
		this.enabled = true;
	}

	void Navigator_Deactivate()
	{
		this.enabled = false;
	}
}
