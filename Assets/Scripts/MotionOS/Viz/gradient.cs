using UnityEngine;
using System.Collections;

public class gradient : MonoBehaviour {
	public Material targetMaterial;
	private Texture2D gradTexture;
	public Color color1;
	public Color color2;
	public int sizeX = 256;
	public int sizeY = 256;
	public float relativeWidth = .2f;
	// Use this for initialization
	void Start () {
		gradTexture = new Texture2D(sizeX,sizeY);
		if (guiTexture)
		{
			guiTexture.texture = gradTexture;
			
		}
		if (targetMaterial)
		{
			targetMaterial.mainTexture = gradTexture;
		}
		Color[] img = new Color[sizeX*sizeY];
		int i = 0;
		int j = 0;
		int k = 0;
		Color col;
		for (i = 0; i <sizeX; i++)
		{
			col = Color.Lerp(color1,color2, (float)i / sizeX);
			for (j = 0; j<sizeY;j++)
			{		k = sizeX*j+i;				
					img[k] = col;
					
			}
			gradTexture.filterMode = FilterMode.Point;
	
		}
		
		
		gradTexture.SetPixels(img);
		gradTexture.Apply();
	}
	
	private float targetWidth = 0.0f;
	private float overlaywidth = 0.0f;
	public float growrate = 1.0f;
	// Update is called once per frame
	void Update () {
		if (guiTexture)
		{
			guiTexture.pixelInset = new Rect(0, 0, Screen.width, Screen.height);
		}
		overlaywidth=Mathf.Lerp(overlaywidth,targetWidth,Time.deltaTime*growrate);
		
	}
	
	void OnDisable()
	{
		targetWidth = 0.0f;
	}
	void OnEnable()
	{
		targetWidth = relativeWidth;
		overlaywidth = 0.0f;
	}
	
	
	public static int guiDepth = -1;
	void OnGUI()
	{
		if (targetMaterial)
		{
			
			GUI.depth = guiDepth;
			GUI.DrawTexture(new Rect(0, 0, overlaywidth*Screen.width, Screen.height), gradTexture, ScaleMode.ScaleToFit, true, (float)(overlaywidth*Screen.width)/Screen.height);
		}
	}
}
