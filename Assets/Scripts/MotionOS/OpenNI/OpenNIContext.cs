using UnityEngine;
using System;
using System.Collections;
using OpenNI;

public class OpenNIContext : MonoBehaviour
{
	static OpenNIContext instance;
	
	public static OpenNIContext Instance
	{
		get 
		{
			if (null == instance)
            {
                instance = GameObject.FindObjectOfType(typeof(OpenNIContext)) as OpenNIContext;
                if (null == instance)
                {
                    GameObject container = new GameObject();
					DontDestroyOnLoad (container);
                    container.name = "OpenNIContextContainer";
                    instance = container.AddComponent(typeof(OpenNIContext)) as OpenNIContext;
                }
				DontDestroyOnLoad(instance);
            }
			return instance;
		}
	}
	
	private Context context;
	public static Context Context 
	{
		get { return Instance.context; }
	}
	
	public DepthGenerator Depth;
	public bool mirror
	{
		get { return mirrorCap.IsMirrored(); }
		set { if (!LoadFromRecording) mirrorCap.SetMirror(value); }
	}
	
	public static bool Mirror
	{
		get { return Instance.mirror; }
		set { Instance.mirror = value; }
	}
	
	public static bool ValidContext
	{
		get { return instance != null; }
	}
	
	private MirrorCapability mirrorCap;
	
	public bool LoadFromRecording = false;
	public string RecordingFilename = "";
	public float RecordingFramerate = 30.0f;
	public bool initialMirror = true;
	
	public OpenNIContext()
	{
		print("Initing OpenNI");
		this.context = new Context();
		if (null == context)
		{
			Debug.LogError("Error opening OpenNI context");
			return;
		}
		
		// NITE license from OpenNI.org
		License ll = new License();
		ll.Key = "0KOIk2JeIBYClPWVnMoRKn5cdY4=";
		ll.Vendor = "PrimeSense";
		context.AddLicense(ll);
	}

	private ProductionNode openNode(NodeType nt)
	{
		ProductionNode ret=null;
		try
		{
			ret = context.FindExistingNode(nt);
		}
		catch
		{
			ret = context.CreateAnyProductionTree(nt, null);
			Generator g = ret as Generator;
			if (null != g)
			{
				g.StartGenerating();
			}
		}
		return ret;
	}
	
	public static ProductionNode OpenNode(NodeType nt)
	{
		return Instance.openNode(nt);
	}
	
	public void Awake()
	{
		if (LoadFromRecording)
		{
			context.OpenFileRecording(RecordingFilename);
			Player player = openNode(NodeType.Player) as Player;
			player.PlaybackSpeed = 0.0;
			StartCoroutine(ReadNextFrameFromRecording(player));
		}
		
		this.Depth = openNode(NodeType.Depth) as DepthGenerator;
		this.mirrorCap = this.Depth.MirrorCapability;
		if (!LoadFromRecording) this.mirrorCap.SetMirror(initialMirror);
	}
	
	IEnumerator ReadNextFrameFromRecording(Player player)
	{
		while (true)
		{
			float waitTime = 1.0f / RecordingFramerate;
			yield return new WaitForSeconds (waitTime);
			player.ReadNext();
		}
	}
	
	// (Since we add OpenNIContext singleton to a container GameObject, we get the MonoBehaviour functionality)
	public void Update () 
	{
		this.context.WaitNoneUpdateAll();
	}
	
	public void OnApplicationQuit()
	{
		print("Shutting down OpenNI");
		if (!LoadFromRecording) 
		{
			context.StopGeneratingAll();
		}
		// shutdown is deprecated, but release doesn't do the job
		context.Shutdown();
		context = null;
		OpenNIContext.instance = null;
	}
}
