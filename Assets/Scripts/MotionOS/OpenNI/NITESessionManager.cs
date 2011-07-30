using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using OpenNI;
using NITE;

public class NITESessionManager : MonoBehaviour 
{
	SessionManager sessionManager;
	Broadcaster broadcaster = new Broadcaster();
	
	private bool isInSession;
	public bool IsInSession
	{
		get { return isInSession; }
	}
	
	public Point3D FocusPoint
	{
		get { return sessionManager.FocusPoint; }
	}
	
	public void AddListener(MessageListener listener)
	{
		broadcaster.AddListener(listener);
	}
	
	public void RemoveListener(MessageListener listener)
	{
		// HACK: prevent exception when removing a nonexisting listener
		try
		{
			broadcaster.RemoveListener(listener);
		}
		catch { }
	}
	
	public void ForceSessionStart(Point3D startPoint)
	{
		sessionManager.ForceSession(startPoint);
	}
	
	public void ForceSessionEnd()
	{
		sessionManager.EndSession();
	}
	
	public int QuickRefocusTimeout
	{
		get { return sessionManager.QuickRefocusTimeout ; }
		set { sessionManager.QuickRefocusTimeout = value; }
	}
		
	// Use this for initialization
	void Start () {
	
		// create hand & gesture generators, if we dont have them yet
		OpenNIContext.OpenNode(NodeType.Hands);
		OpenNIContext.OpenNode(NodeType.Gesture);

		// init session manager
		sessionManager = new SessionManager(OpenNIContext.Context, "Click", "RaiseHand");
		sessionManager.QuickRefocusTimeout = 5;
		sessionManager.SessionStart += new System.EventHandler<PositionEventArgs>(sessionManager_SessionStart);
		sessionManager.SessionEnd += new System.EventHandler(sessionManager_SessionEnd);
		
		sessionManager.AddListener(broadcaster);
	}

	void sessionManager_SessionEnd(object sender, System.EventArgs e)
	{
		isInSession = false;
		SendMessage("OnSessionEnd", SendMessageOptions.DontRequireReceiver);
		foreach (NITEControl go in GameObject.FindObjectsOfType(typeof(NITEControl)) as NITEControl[])
		{
			if (!go.enabled) continue;
			go.SendMessage("OnSessionEnd", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	
	void sessionManager_SessionStart(object sender, PositionEventArgs e)
	{
		isInSession = true;
		SendMessage("OnSessionStart", SendMessageOptions.DontRequireReceiver);
		foreach (NITEControl go in GameObject.FindObjectsOfType(typeof(NITEControl)) as NITEControl[])
		{
			if (!go.enabled) continue;
			go.SendMessage("OnSessionStart", SendMessageOptions.DontRequireReceiver);
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (OpenNIContext.ValidContext)
		{
			sessionManager.Update(OpenNIContext.Context);
		}
	}
	
	public void OnApplicationQuit()
	{
		sessionManager = null;
	}
}
