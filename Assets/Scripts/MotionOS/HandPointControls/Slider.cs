using UnityEngine;
using System.Collections;

public class Slider {
	public Vector3 start;
	public Vector3 direction;
	public float size;
		
	// move the slider to contain pos within its bounds
	public void MoveToContain(Vector3 pos)
	{
		float dot = Vector3.Dot(direction, pos - start);
		if (dot > size)
		{
			start += direction * (dot - size);
		}
		if (dot < 0)
		{
			start += direction * dot;
		}
	}
	
	// move the slider so that pos will be mapped to val (0-1)
	public void MoveTo(Vector3 pos, float val)
	{
		start = pos - (direction * (val * size));
	}
	
	public float GetValue(Vector3 pos)
	{
		float dot = Vector3.Dot(direction, pos - start);
		//Debug.Log(string.Format("Dot: {0}. Start: {1}. Pos: {2}.  Diff: {3}", dot, start, pos, pos - start));
		return Mathf.Clamp01(dot / size);
	}
	
	public Vector3 GetPosition(float val)
	{
		return start + (direction * (val * size));
	}
	
	public Slider()
	{
	}
	
	public Slider(Vector3 dir, Vector3 start, float size)
	{
		this.start = start;
		this.direction = dir;
		this.size = size;
	}
}
