using UnityEngine;
using System.Collections;

// add this to a 3D Text object
public class SetTextMenuItem : MonoBehaviour {
	void MenuItem_Init(string str)
	{
		TextMesh textMesh = GetComponentInChildren<TextMesh>();
		if (textMesh)
		{
			textMesh.text = str;
		}
	}
}
