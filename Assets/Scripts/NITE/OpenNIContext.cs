using UnityEngine;
using System;
using System.Collections;
using OpenNI;

public class OpenNIContext
{
	static readonly OpenNIContext instance = new OpenNIContext();
	
	static OpenNIContext()
	{ 
		MonoBehaviour.print("Static constructor");
	}
	
	OpenNIContext()
	{
		MonoBehaviour.print("normal constructor");
		Init();
	}
	~OpenNIContext()
	{
		MonoBehaviour.print("Destroying context");
	}
	public static OpenNIContext Instance
	{
		get {return instance;}
	}
	
	public string OpenNIXMLFilename = "OpenNI.xml";
	public Context context;
	
	public DepthGenerator Depth;
	public bool Mirror
	{
		get { return mirror.IsMirrored(); }
		set { mirror.SetMirror(value); }
	}
	
	private bool validContext = false;
	public bool ValidContext
	{
		get { return validContext; }
	}
	
	private MirrorCapability mirror;
	
	private void Init()
	{
		this.context = new Context(OpenNIXMLFilename);
		if (null == context)
		{
			return;
		}
		
		this.Depth = new DepthGenerator(this.context);
		this.mirror = this.Depth.MirrorCapability;
		
		MonoBehaviour.print("OpenNI inited");
		
		validContext = true;
		
		Start();
	}

	void Start () 
	{
		if (validContext)
		{
			this.context.StartGeneratingAll();
		}
	}
	
	// Update is called once per frame
	public void Update () 
	{
		if (validContext)
		{
			//Debug.Log("updateall");
			this.context.WaitNoneUpdateAll();
		}
	}
}
