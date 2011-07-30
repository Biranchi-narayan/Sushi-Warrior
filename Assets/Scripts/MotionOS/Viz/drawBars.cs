using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class drawBars : MonoBehaviour {
	
	public int numSamples;
	float [] numberleft; 
	float[] numberright;
	private GameObject[] thebarsleft;
	private GameObject[] thebarsright;	
	public GameObject abar;
	public Transform leftChannelParent;
	public Transform rightChannelParent;
	void Start () {
		float spacing = 0.4f - (numSamples * 0.001f);
		float width = 0.3f - (numSamples * 0.001f);
		numberleft = new float[numSamples];
		numberright = new float[numSamples];
		thebarsleft = new GameObject[numSamples];
		thebarsright = new GameObject[numSamples];
		if (leftChannelParent == null)
		{
			leftChannelParent = transform;
		}  
		if (rightChannelParent == null)
		{
			rightChannelParent = transform;
		}
		for(int i=0; i < numSamples; i++){ 
			float xpos = i*spacing -8.0f;
			Vector3 positionleft = new Vector3(xpos,10, 0);
			thebarsleft[i] = (GameObject)Instantiate(abar, positionleft, Quaternion.identity);	
			thebarsleft[i].transform.localScale = new Vector3(width,1.0f,.2f);
			thebarsleft[i].transform.parent = leftChannelParent;
			Vector3 positionright = new Vector3(xpos,-10, 0);
			thebarsright[i] = (GameObject)Instantiate(abar, positionright, Quaternion.identity);	
			thebarsright[i].transform.localScale = new Vector3(width,1.0f,.2f);
			thebarsright[i].transform.parent = rightChannelParent;
		}	
	}
		
	void Update () {    
		audio.GetSpectrumData(numberleft, 0, FFTWindow.BlackmanHarris);			
		audio.GetSpectrumData(numberright, 1, FFTWindow.BlackmanHarris);
		for(int i=0; i < numSamples; i++){
			//Vector3 leftScale = thebarsleft[i].transform.localScale;
			//thebarsleft[i].transform.localScale = new Vector3(leftScale.x, numberleft[i]*30,leftScale.z);
			Vector3 lp = thebarsleft[i].transform.localPosition;
			thebarsleft[i].transform.localPosition = new Vector3(lp.x, 10+numberleft[i]*1000, lp.z);
			
			//Vector3 rightScale = thebarsright[i].transform.localScale;
			//thebarsright[i].transform.localScale = new Vector3(rightScale.x, numberright[i]*30,rightScale.z);
			Vector3 rp = thebarsright[i].transform.localPosition;
			thebarsright[i].transform.localPosition = new Vector3(rp.x, -10+numberright[i]*1000, rp.z);
			
		}
	}
}