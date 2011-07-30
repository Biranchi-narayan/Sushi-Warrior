using UnityEngine;
using System.Collections;

public class LightDuringSession : MonoBehaviour {

	public Light targetLight;
	public float initIntensity = 0.1f;
	public float sessionIntensity = 1.0f;
	public Color initColor = Color.red;
	public Color sessionColor = Color.white;
	public float rate = 2.1f;
	public float targetIntensity;
	public Color targetColor;
	// Use this for initialization
	void Start () {
		targetLight.intensity = initIntensity;
		targetIntensity = initIntensity;
		targetLight.color = initColor;
		targetColor = initColor;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if  (targetIntensity != targetLight.intensity)
		{
			targetLight.intensity = Mathf.Lerp(targetLight.intensity, targetIntensity, rate*Time.deltaTime);			
		}
		if (targetColor != targetLight.color)
		{
			targetLight.color = Color.Lerp(targetLight.color, targetColor, rate*Time.deltaTime);
		}
	}
	
	public void Hand_Create()
	{
		targetIntensity = sessionIntensity;
		targetColor = sessionColor;
	}
	
	public void Hand_Destroy()
	{
		targetIntensity = initIntensity;
		targetColor = initColor;
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
