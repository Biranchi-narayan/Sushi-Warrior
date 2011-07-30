using UnityEngine;
using System.Collections;

public class FaderVisualizer : MonoBehaviour {

	public Fader fader;
	
	// Use this for initialization
	void Start () {
		if (null == fader)
		{
			fader = GetComponent<Fader>();
			if (null == fader)
			{
				Debug.LogError("No fader for fader visualizer on " + gameObject.name);
			}
		}
	}
	
	void OnGUI()
	{
		if (!fader) return;
		
		if (fader.orientation ==  FaderOrientation.Horizontal)
		{
			//GUI.HorizontalSlider(new Rect(100, 300, 200, 40), fader.value, 0, 1.0f);
			GUI.HorizontalSlider(new Rect((Screen.width - 200) / 2, Screen.height - 20 - 10, 200, 20), fader.value, 0, 1.0f);
		}
		else
		{
			//GUI.VerticalSlider(new Rect(100, 300, 40, 200), fader.value, 0, 1.0f);
			GUI.VerticalSlider(new Rect(10, (Screen.height - 200) / 2, 20, 200), fader.value, 0, 1.0f);
		}

		//GUI.Label(new Rect(10, 10, 100, 20), fader.value.ToString());
	}
}
