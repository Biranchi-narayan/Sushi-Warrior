using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using OpenNI;

public class MySessionManager {
	// singleton
	private static MySessionManager instance = null;
	public static MySessionManager Instance
	{
		get {
			if (null == instance)
			{
				instance = new MySessionManager();
			}
			return instance;
		}
	}
	
	private List<GameObject> Listeners = new List<GameObject>();
	
	public Vector3 focusPoint { get; private set; }
	public bool inSession { get; private set; }
	
	public static Vector3 FocusPoint
	{
		get { return Instance.focusPoint; }
	}

	public static bool InSession
	{
		get { return Instance.inSession; }
	}

	public bool DetectWave = true;
	public bool DetectPush = true;
	
	private int handId = -1;
	private Point3D handPos = new Point3D(0,0,0);
	
	private HandsGenerator hands { get { return OpenNIContext.OpenNode(NodeType.Hands) as HandsGenerator; }}
	private GestureGenerator gestures { get { return OpenNIContext.OpenNode(NodeType.Gesture) as GestureGenerator; }}
	
	private MySessionManager()
	{
		// private ctor, this is a singleton!
		this.hands.HandCreate += new EventHandler<HandCreateEventArgs>(hands_HandCreate);
		this.hands.HandUpdate += new EventHandler<HandUpdateEventArgs>(hands_HandUpdate);
		this.hands.HandDestroy += new EventHandler<HandDestroyEventArgs>(hands_HandDestroy);
		
		if (DetectWave) {
			this.gestures.AddGesture ("Wave");
		}
		if (DetectPush) {
			this.gestures.AddGesture ("Click");
		}
		
		this.gestures.GestureRecognized += new EventHandler<GestureRecognizedEventArgs> (gestures_GestureRecognized);
	}
		
	void gestures_GestureRecognized (object Sender, GestureRecognizedEventArgs e)
	{
		if (handId == -1)
		{
			this.hands.StartTracking (e.IdentifiedPosition);
		}
	}

	void hands_HandCreate (object Sender, HandCreateEventArgs e)
	{
		// Only support one hand at the moment
		if (handId != -1 && e.UserID != handId) return;
		handId = e.UserID;
		
		OnSessionStarted(e.Position);
		
		foreach (GameObject obj in new List<GameObject>(Listeners))
		{
			NotifyHandCreate(obj, e.Position);
		}
	}
	
	void hands_HandUpdate (object Sender, HandUpdateEventArgs e)
	{
		handPos = e.Position;
		foreach (GameObject obj in new List<GameObject>(Listeners)) {
			NotifyHandUpdate(obj, e.Position);
		}
	}
	
	void hands_HandDestroy (object Sender, HandDestroyEventArgs e)
	{
		handId = -1;
		
		// TODO: foreach on a copy of the list
		foreach (GameObject obj in Listeners) {
			NotifyHandDestroy(obj);
		}
		OnSessionEnded();
		this.gestures.StartGenerating();
	}

	public static void AddListener(GameObject obj)
	{
		if (!MySessionManager.Instance.Listeners.Contains(obj)) {
			MySessionManager.Instance.Listeners.Add(obj);
		}
		
		if (MySessionManager.InSession) {
			MySessionManager.Instance.NotifyHandCreate(obj, MySessionManager.Instance.handPos);
		}
	}
	
	public static void RemoveListener(GameObject obj)
	{
		if (null == MySessionManager.instance) return;

		if (MySessionManager.Instance.Listeners.Contains(obj))	{
			MySessionManager.Instance.Listeners.Remove(obj);
		}
		
		if (MySessionManager.InSession) {
			MySessionManager.Instance.NotifyHandDestroy(obj);
		}
	}
	
	public void StartSession(Point3D pos)
	{
		EndSession();
		// doesn't guarantee session will start
		this.hands.StartTracking(pos);
	}
	
	public void EndSession()
	{
		Debug.LogError("Someone is ending the session");
		if (!InSession) return;
		
		// lose the current point
		if (-1 != handId) {
			this.hands.StopTracking(handId);
		}
	}
	
	void OnSessionStarted(Point3D pos)
	{
		Debug.Log("Session started");
		inSession = true;
		focusPoint = Point3DToVector3(pos);
		foreach (GameObject obj in new List<GameObject>(Listeners)) {
			NotifySessionStart(obj);
			NotifyHandCreate(obj, pos);
		}
	}
	
	void OnSessionEnded()
	{
		Debug.Log("Session ended");
		inSession = false;
		foreach (GameObject obj in new List<GameObject>(Listeners)) {
			NotifyHandDestroy(obj);
			NotifySessionEnd(obj);
		}
	}
	
	void NotifyHandCreate(GameObject obj, Point3D pos)
	{
		obj.SendMessage("Hand_Create", Point3DToVector3(pos), SendMessageOptions.DontRequireReceiver);
	}

	void NotifyHandUpdate(GameObject obj, Point3D pos)
	{
		obj.SendMessage("Hand_Update", Point3DToVector3(pos), SendMessageOptions.DontRequireReceiver);
	}

	void NotifyHandDestroy(GameObject obj)
	{
		obj.SendMessage("Hand_Destroy", SendMessageOptions.DontRequireReceiver);
	}
	
	void NotifySessionStart(GameObject obj)
	{
		obj.SendMessage("Session_Start", SendMessageOptions.DontRequireReceiver);
	}
	
	void NotifySessionEnd(GameObject obj)
	{
		obj.SendMessage("Session_End", SendMessageOptions.DontRequireReceiver);
	}
	
	Vector3 Point3DToVector3(Point3D pos)
	{
		return new Vector3(pos.X, pos.Y, pos.Z);
	}
}
