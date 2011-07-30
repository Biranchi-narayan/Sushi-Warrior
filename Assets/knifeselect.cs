using UnityEngine;
using System.Collections;

public class knifeselect : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (startColorFade && timer < delay)
		{
			timer += Time.deltaTime;
			renderer.material.color = Color.Lerp(color1,color2,timer/delay);
			if (timer > delay)
			{	
				Debug.Log("timer done, select " + gameObject.name);
				SendMessage("Selected");
			}
		}
		else if (!startColorFade && timer < delay2)
		{
			timer += Time.deltaTime;
			renderer.material.color = Color.Lerp(color2,color1,timer/delay2);	
			if (timer > delay2)
			{
				Debug.Log("timer done, deselect " + gameObject.name);
			}
		}
		
		
	}
	public Color color1 = Color.yellow;
	public Color color2 = Color.green;
	public float delay = 1.0f;
	public float delay2 = .2f;
	Color targetColor;
	float timer;
	bool startColorFade;
	void OnTriggerEnter(Collider c)
	{
		startColorFade = true;
		timer = 0.0f;
	}
	void OnTriggerExit(Collider c)
	{
		startColorFade = false;
		timer = 0.0f;
	}
}
