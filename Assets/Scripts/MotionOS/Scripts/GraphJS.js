@script RequireComponent(Camera)

static var lineMaterial : Material; 
var numberOfPoints = 400;
protected var points : Vector3[];
var graphable : Graphable;
var graphValue = 0.0;
var debugOutput = true;
var heightScale : float;
var widthScale : float;
var width : float;
var height : float;
var heightOffset : float;
var color = Color.yellow;

function GraphUpdate (newValue : float)
{   
    graphValue = newValue * heightScale;
    if(debugOutput) print("graphValue:" + graphValue);
}

function Start () 
{
	heightScale = 1.0;//heightScale = height / ( graphable.maxValue - graphable.minValue );
	widthScale = width / numberOfPoints;
	points = new Vector3[numberOfPoints];
	var nearClip = camera.nearClipPlane - .00001f;
	for (i = 0; i < numberOfPoints; i++) 
	{
		points[i] = new Vector3(x01(i), heightOffset, nearClip);
	}
	CreateLineMaterial();
}

function x01(i : int)
{
	return Mathf.InverseLerp(0, numberOfPoints, i);
}

function Update () 
{
	var i = numberOfPoints - 1;
	points[i] = new Vector3(points[i].x, graphValue + heightOffset, points[i].z);
	if(debugOutput) print(points[i].ToString());
	ScrollGraph();
}

function ScrollGraph()
{
	for (x = 1; x < points.Length; ++x)
    {
    	points[x-1] = new Vector3(points[x-1].x, points[x].y, points[x-1].z);
    }
}

function CreateLineMaterial () 
{
	lineMaterial = new Material( "Shader \"Lines/Colored Blended\" {" +
		"SubShader { Pass {" +
		"	BindChannels { Bind \"Color\",color }" +
		"	Blend SrcAlpha OneMinusSrcAlpha" +
		"	ZWrite Off Cull Off Fog { Mode Off }" +
		"} } }");
	lineMaterial.hideFlags = HideFlags.HideAndDontSave;
	lineMaterial.shader.hideFlags = HideFlags.HideAndDontSave;
}


function OnPostRender () 
{ 
	GL.PushMatrix();
	GL.LoadOrtho();
	lineMaterial.SetPass(0);
	GL.Begin(GL.LINES);
	GL.Color(color);
	for (i = 0; i < numberOfPoints-1; i++) 
	{
		//GL.Vertex3(points[i].x, points[i].y, points[i].z);
		//GL.Vertex3(points[i+1].x, points[i+1].y, points[i+1].z);
		GL.Vertex(points[i]);
		GL.Vertex(points[i+1]);
	}
	GL.End(); 
	GL.PopMatrix();
}