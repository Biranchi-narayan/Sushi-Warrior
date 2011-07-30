using UnityEngine;
using System.Collections;

public class GotoView : MonoBehaviour {

	// Use this for initialization
	void Start () {
		startT = views[currentView];
		targetT = views[currentView];
	}
	public Transform [] views;
	public int currentView = 0;
	public int targetView = 0;
	public float currTime = 0.0f;
	public float maxTime = 0.3f;
	private bool moving = true;

	private Transform targetT;
	private Transform startT;
	// Update is called once per frame
	public float springDamp = 5.0f;
	

	public float minDistance = 0.01f;
	
	public InterpolationType m_InterpolationType  = InterpolationType.Exponential;
	
	public enum InterpolationType
    {
        Linear,
        Sinusoidal,
        Hermite,
		Exponential
    }
	
	private static Vector3 Sinerp(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Sinerp(start.x, end.x, value), Sinerp(start.y, end.y, value), Sinerp(start.z, end.z, value));
    }
   
	public void gotoView(int n)
	{
		targetView = n;
	}
	
    private static Vector3 Hermite(Vector3 start, Vector3 end, float value)
    {
        return new Vector3(Hermite(start.x, end.x, value), Hermite(start.y, end.y, value), Hermite(start.z, end.z, value));
    }
   
    /* The following functions are also in the Mathfx script on the UnifyWiki, but are included here so the script is self sufficient. */
   
    private static float Sinerp(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, Mathf.Sin(value * Mathf.PI * 0.5f));
    }

    private static float Hermite(float start, float end, float value)
    {
        return Mathf.Lerp(start, end, value * value * (3.0f - 2.0f * value));
    }
	public void OnSessionEnd()
 {
  targetView = 0;
 }
 
 public void OnItemHover(int Item)
 {
  targetView = Item + 1;
 }
	
	void Update () {
		if (currentView != targetView)
		{
			
			if ((targetView > views.Length) || (targetView < 0)) //Some sorta error
			{
				Debug.Log("targetView out of range!!");
				targetView = 0;
			}
			else
			{
				
				
				moving = true;
				startT = views[currentView];
				targetT = views[targetView];
				currTime = 0.0f;
				currentView = targetView;
				
			}
		}
		
		
			
		if (moving)
		{
			currTime += Time.deltaTime;	
		
			switch (m_InterpolationType)
				{
					case InterpolationType.Linear:
						transform.position = Vector3.Lerp(startT.position,targetT.position, currTime / maxTime);
						transform.rotation = Quaternion.Slerp(startT.rotation,targetT.rotation, currTime / maxTime);

						if (currTime > maxTime)
						{
							transform.position = targetT.position;
							transform.rotation = targetT.rotation;
							moving = false;
						}
						break;
					case InterpolationType.Sinusoidal:
						transform.position = Sinerp(startT.position,targetT.position, currTime / maxTime);
						transform.rotation = Quaternion.Slerp(startT.rotation,targetT.rotation, currTime / maxTime);

						if (currTime > maxTime)
						{
							transform.position = targetT.position;
							transform.rotation = targetT.rotation;
							moving = false;
						}
						break;
					case InterpolationType.Hermite:
						transform.position = Hermite(startT.position,targetT.position, currTime / maxTime);
						transform.rotation = Quaternion.Slerp(startT.rotation,targetT.rotation, currTime / maxTime);

						if (currTime > maxTime)
						{
							transform.position = targetT.position;
							transform.rotation = targetT.rotation;
							moving = false;
						}
						break;
					case InterpolationType.Exponential:
						transform.position = Vector3.Lerp(transform.position,targetT.position,Time.deltaTime*springDamp);
						transform.rotation = Quaternion.Slerp(transform.rotation,targetT.rotation,Time.deltaTime*springDamp);
						if ((transform.position - targetT.position).magnitude < minDistance)
						{
							transform.position = targetT.position;
							transform.rotation = targetT.rotation;
							moving = false;
						}
						break;
				}	
			
		}
	}
}
