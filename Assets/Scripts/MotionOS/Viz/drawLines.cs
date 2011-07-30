using UnityEngine;
using System.Collections;

[RequireComponent (typeof (LineRenderer))]
public class drawLines : MonoBehaviour {


	// Use this for initialization
	//public AudioListener aud;
	public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    public int numSamples = 256;
	private float[]numberleft;
	private float[]numberright;
	public float width = .1f;
	LineRenderer lineRenderer;
	
	void Start () {
		numberleft = new float[numSamples];
		numberright = new float[numSamples];
		lineRenderer = GetComponent<LineRenderer>();
		lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
		lineRenderer.SetColors(c1, c2);
        lineRenderer.SetWidth(0.2F, 0.2F);
        lineRenderer.SetVertexCount(numSamples);
		
	}
	
	// Update is called once per frame
	void Update () {
		AudioListener.GetOutputData(numberleft, 0);//, FFTWindow.BlackmanHarris);			
		//audio.GetOutputData(numberright, 1);//, FFTWindow.BlackmanHarris);
		float currX = 0;
		
		
		for(int i=0; i < numSamples; i++){
			lineRenderer.SetPosition(i, new Vector3(currX,numberleft[i]*30,0));
			currX += width;
		}
	}
}
