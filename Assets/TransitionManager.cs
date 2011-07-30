using UnityEngine;
using System.Collections;
using OpenNI;

public class TransitionManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	bool poseDetectBoxActive = false;
	void Update () {
		
	}
	public GotoView cam;
	
	public GameObject poseDetected;
	void CalibrationFailed(CalibrationEndEventArgs e)
	{
		poseDetectBoxActive = false;
		poseDetected.SetActiveRecursively(false);
	}
	
	void CalibrationComplete (CalibrationEndEventArgs e)
	{
		cam.gotoView(3);	
		StartCoroutine(gotoGameMenu());
		poseDetected.SetActiveRecursively(false);
	}

	
	void PoseDetected(PoseDetectedEventArgs e)
	{
		poseDetected.SetActiveRecursively(true);
	}
	
	IEnumerator gotoCalibration()
	{
		yield return new WaitForSeconds(.8f);
				
		cam.gotoView(2);
	}
	public GameObject dwe;
	bool atMainCam = false;
	IEnumerator gotoGameMenu()
	{
		yield return new WaitForSeconds(1f);

		SendMessage("TurnOffDepthMap");
		dwe.SendMessage("DoFade");
		atMainCam = true;
	}
	
	
	bool firstUserDetected = false;
	void UserDetected(NewUserEventArgs e)
	{
		Debug.Log("USER DETECTED Rcvd");
		if (firstUserDetected)
		{
			Debug.Log("First User already Detected");
		}
		if (!firstUserDetected)
		{
			firstUserDetected = true;
			cam.gotoView(1);
			StartCoroutine(gotoCalibration());
		}
	}
	void AllUsersLost(UserLostEventArgs e)
	{
		SendMessage("TurnOnDepthMap");
		firstUserDetected = false;
		Debug.Log("AllUsersLost Rcvd");
		cam.gotoView(0);		
		if (atMainCam)
		{
			dwe.SendMessage("DoFade");
			atMainCam = false;
		}
	}
	void CalibratedUserLost(UserLostEventArgs e)
	{
		SendMessage("TurnOnDepthMap");
		Debug.Log("CalibratedUserLost Rcvd");
		cam.gotoView(2);
		if (atMainCam)
		{
			dwe.SendMessage("DoFade");
			atMainCam = false;
		}
	
	}
	
	
	public GameObject TooCloseMessage;
	void UserTooClose() {
		TooCloseMessage.SetActiveRecursively(true);
	}
	void UserNotTooClose() {
		TooCloseMessage.SetActiveRecursively(false);
	}
	public GameObject TooCloseCalibratedMessage;
	void CalibratedUserTooClose() {
		TooCloseCalibratedMessage.SetActiveRecursively(true);
	}
	void CalibratedUserNotTooClose() {
		TooCloseCalibratedMessage.SetActiveRecursively(false);
	}
}
