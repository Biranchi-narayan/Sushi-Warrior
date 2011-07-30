using UnityEngine;
using System.Collections;

public class NITECursorVisualizer : MonoBehaviour {

	public NITECursor Cursor;
	public float CursorSize = 30.0f;

	void OnGUI()
	{
		if (Cursor.CursorActive)
		{
			// Make cursor smaller the closer it is to touch plane
			float currentCursorSize = CursorSize - (Cursor.ClickProgress * CursorSize * 0.3f);
			float halfCursorSize = (currentCursorSize / 2.0f);
		
			GUI.Box(new Rect(Cursor.CursorPosition.x * Screen.width - halfCursorSize, Cursor.CursorPosition.y * Screen.height - halfCursorSize, currentCursorSize, currentCursorSize), "X");
		}
	}
}
