public class VelocityGraphJS extends GraphJS
{
	protected var previousTime : float;
	protected var previousGraphValue : float;
	var velocityScale : float;
	
	function Start()
	{
		previousTime = Time.time;
		heightOffset = 0.5;
		previousGraphValue = heightOffset;
		super.Start();
	}
	
	function GraphUpdate(newValue : float)
	{
		if(debugOutput) print("Running subclass graphupdate.");
		var currentTime = Time.time;
		var timeStep = currentTime - previousTime;
		graphValue = velocityScale * ( newValue - previousGraphValue ) / timeStep;
		previousTime = currentTime;
		previousGraphValue = newValue;
	}
}