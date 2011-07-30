using UnityEngine;
using System.Collections;

public class FovDuringSession : MonoBehaviour {

	public Camera camera;
	public float DuringSession = 40;
	public float NotInSession = 60;
	public float WhenClicked = 25;
	
	public bool ZoomInWhenPushed = false;
	
	float targetFov;
	
	// Use this for initialization
	void Start () {
		targetFov = NotInSession;
		
		if (ZoomInWhenPushed)
		{
			if (null == GetComponent<PushDetector>())
			{
				Debug.LogWarning("Adding push detector to " + gameObject.name + " for ZoomInWhenPushed behaviour");
			}
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		if (targetFov != camera.fieldOfView)
		{
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView, targetFov, 0.1f);
		}
	}
	
	public void PushDetector_Push()
	{
		if (ZoomInWhenPushed) {
			targetFov = WhenClicked;
		}
	}
	
	public void PushDetector_Release()
	{
		if (ZoomInWhenPushed) {
			targetFov = DuringSession;
		}
	}
	
	void Navigator_Activate()
	{
		this.enabled = true;
	}

	void Navigator_Deactivate()
	{
		this.enabled = false;
	}
	
	public void Hand_Create()
	{
		targetFov = DuringSession;
	}
	
	public void Hand_Destroy()
	{
		targetFov = NotInSession;
	}
}
