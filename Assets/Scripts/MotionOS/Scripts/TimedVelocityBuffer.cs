using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//the type can be a float, vector, or anything that implements subtraction
public class TimedVelocityBuffer: TimedBuffer<float>
{
	public TimedVelocityBuffer(float timeFrame): base(timeFrame)
	{
		
	}
	
	public float GetAverageVelocity()
	{
		Prune();
		
		if(buffer.Count < 2)
		{
			//print("Fewer than 2 points, returning zero velocity...");
			return 0.0f;
		}
		
        float pointAverage = 0.0f;
		float pointTotal = 0.0f;
        float timeAverage = 0.0f;
		float timeTotal = 0.0f;
		
        foreach (TimestampedObject<float> timestampedPoint in buffer)
        {
            pointTotal += timestampedPoint.obj;
            timeTotal  += timestampedPoint.timeStamp;
        }
		
		float deltaTime = buffer[buffer.Count - 1].timeStamp - buffer[0].timeStamp;
		
        pointAverage = pointTotal / buffer.Count;
        timeAverage = timeTotal / deltaTime;
 
        float v1 = 0.0f;
        float v2 = 0.0f;
 
        foreach (TimestampedObject<float> timestampedPoint in buffer)
        {
			float timeDeviation = timestampedPoint.timeStamp - timeAverage;
            v1 += timeDeviation * (timestampedPoint.obj - pointAverage);
            v2 += timeDeviation * timeDeviation;
        }
 
        //float a = v1 / v2;
        //float b = yAvg - a * xAvg;
		float averageVelocity = v1 / v2;
 
		//print("y = ax + b");
        
		//print("averageVelocity = {0}, the slope of the trend line.", Math.Round(averageVelocity, 2));
        
		//print("b = {0}, the intercept of the trend line.", Math.Round(b, 2));
		
		return averageVelocity;
	}
}
