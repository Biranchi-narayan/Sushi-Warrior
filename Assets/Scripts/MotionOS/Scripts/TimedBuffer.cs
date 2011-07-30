using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TimestampedObject <T>
{
	public T obj { get; private set; }
	public float timeStamp { get; private set; }
	
	public TimestampedObject(T _obj, float _timeStamp) 
	{
		obj = _obj;
		timeStamp = _timeStamp;
	}
	
	public TimestampedObject(T _obj)
	{
		obj = _obj;
		timeStamp = Time.time;
	}
}

public class TimedBuffer<T>{
	
	protected float timeFrame;

	protected List<TimestampedObject<T>> buffer = new List<TimestampedObject<T>>();
	
	public TimedBuffer(float _timeFrame)
	{
		timeFrame = _timeFrame;
	}
	
	public void AddDataPoint(T obj, float timeStamp)
	{
		buffer.Add(new TimestampedObject<T>(obj,timeStamp));
	}
	
	public void AddDataPoint(T obj)
	{
		buffer.Add(new TimestampedObject<T>(obj));
	}
	
	public void Prune()
	{
		foreach (TimestampedObject<T> point in buffer)
		{
			if(Time.time > point.timeStamp + timeFrame)
			{
				buffer.Remove(point);
			}
		}
	}
	
	public List<TimestampedObject<T>> Buffer	
	{
		get 
		{
			Prune();
			return buffer;
		}
	}
}
