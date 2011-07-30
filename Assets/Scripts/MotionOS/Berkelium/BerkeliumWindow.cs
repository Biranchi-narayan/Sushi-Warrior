using UnityEngine;
using System;
using System.Collections;
using System.Runtime.InteropServices;

public class BerkeliumWindow : MonoBehaviour
{
	public int width = 512;
	public int height = 512;
	public string url = "http://www.google.com";
	public bool interactive = false;
	public bool transparency = false;
	
	private Texture2D m_Texture;
	private Color[] m_Pixels;
	private GCHandle m_PixelsHandle;
	private int m_TextureID;
	
	private BerkeliumApi.SetPixelsFunc m_setPixelsFunc;
	private BerkeliumApi.ApplyTextureFunc m_applyTextureFunc;
	private BerkeliumApi.ExternalHostFunc m_externalHostFunc;
	private BerkeliumApi.LoadingStateChangedFunc m_loadingStateChangedFunc;
	
	bool isLoading = true;

	void Awake()
	{
		// force init of global berkelium context
		BerkeliumContext c = BerkeliumContext.Instance;
		
		// Create the texture that will represent the website (with optional transparency and without mipmaps)
		TextureFormat texFormat = transparency ? TextureFormat.ARGB32 : TextureFormat.RGB24;
		m_Texture = new Texture2D (width, height, texFormat, false);

		// Create the pixel array for the plugin to write into at startup    
		m_Pixels = m_Texture.GetPixels (0);
		// "pin" the array in memory, so we can pass direct pointer to it's data to the plugin,
		// without costly marshaling of array of structures.
		m_PixelsHandle = GCHandle.Alloc(m_Pixels, GCHandleType.Pinned);

		// Save the texture ID
		m_TextureID = m_Texture.GetInstanceID();
		
		// Improve rendering at shallow angles
		m_Texture.filterMode = FilterMode.Trilinear;
		m_Texture.anisoLevel = 2;
		
		
		// Assign texture to the renderer
		if (renderer)
		{
			renderer.material.mainTexture = m_Texture;
			
			// Transparency?
			if(transparency)
				renderer.material.shader = Shader.Find("Transparent/Diffuse");
			else
				renderer.material.shader = Shader.Find("Diffuse");
			
			// The texture has to be flipped
			renderer.material.mainTextureScale = new Vector2(-1,1);
		}
		// or gui texture
		else if (GetComponent(typeof(GUITexture)))
		{
			GUITexture gui = GetComponent(typeof(GUITexture)) as GUITexture;
			gui.texture = m_Texture;
		}
		else
		{
			Debug.Log("Game object has no renderer or gui texture to assign the generated texture to!");
		}
		
		// Create new web window
		BerkeliumApi.Window.create(m_TextureID, m_PixelsHandle.AddrOfPinnedObject(), transparency, width,height, url);

		// Paint callbacks
		m_setPixelsFunc = new BerkeliumApi.SetPixelsFunc(this.SetPixels);
		m_applyTextureFunc = new BerkeliumApi.ApplyTextureFunc(this.ApplyTexture);
		BerkeliumApi.Window.setPaintFunctions(m_TextureID, m_setPixelsFunc, m_applyTextureFunc);
		
		// Set the external host callback (for calling Unity functions from javascript)
		m_externalHostFunc = new BerkeliumApi.ExternalHostFunc(this.OnExternalHost);
		BerkeliumApi.Window.setExternalHostCallback(m_TextureID, m_externalHostFunc);
		
		m_loadingStateChangedFunc = new BerkeliumApi.LoadingStateChangedFunc(this.OnLoadingStateChanged);
		BerkeliumApi.Window.setLoadingStateChanged(m_TextureID, m_loadingStateChangedFunc);
	}
	
	void SetPixels(/*int left, int top, int width, int height*/)
	{
		BerkeliumApi.Rect rect = BerkeliumApi.Window.getLastDirtyRect(m_TextureID);
		m_Texture.SetPixels(rect.left, rect.top, rect.width, rect.height, m_Pixels, 0);
	}
	
	void ApplyTexture()
	{
		m_Texture.Apply();
	}
	
	void OnExternalHost(/*string message*/)
	{
	}
	
	void OnLoadingStateChanged(bool isLoading)
	{
		this.isLoading = isLoading;
	}
	
	void OnMouseEnter()
	{
	}
	
	void OnMouseOver()
	{
		// Only when interactive is enabled
		if(!interactive)
			return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			int x = width - (int) (hit.textureCoord.x * width);
			int y = /*height - */(int) (hit.textureCoord.y * height);
	
			BerkeliumApi.Window.mouseMove(m_TextureID, x, y);
		}
	}
	
	public IEnumerator WaitUntilDoneLoading()
	{
		while (this.isLoading)
		{
			yield return null;
		}
	}
	
	void OnMouseExit()
	{
	}
	
	void OnMouseDown()
	{
		// Only when interactive is enabled
		if(!interactive) return;
		
		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			int x = width - (int) (hit.textureCoord.x * width);
			int y = /*height - */(int) (hit.textureCoord.y * height);
			
			// Focus the window
			BerkeliumApi.Window.focus(m_TextureID);
	
			BerkeliumApi.Window.mouseMove(m_TextureID, x, y);
			BerkeliumApi.Window.mouseDown(m_TextureID, 0);
		}
	}
	
	void OnMouseUp()
	{
		// Only when interactive is enabled
		if(!interactive) return;

		RaycastHit hit;
		if (Physics.Raycast (Camera.main.ScreenPointToRay(Input.mousePosition), out hit))
		{
			int x = width - (int) (hit.textureCoord.x * width);
			int y = /*height - */(int) (hit.textureCoord.y * height);
	
			BerkeliumApi.Window.mouseMove(m_TextureID, x, y);
			BerkeliumApi.Window.mouseUp(m_TextureID, 0);
		}
	}
	
	public void NavigateTo(string url)
	{
		BerkeliumApi.Window.navigateTo(m_TextureID, url);
	}

	public void ExecuteJavascript(string javascript)
	{
		BerkeliumApi.Window.executeJavascript(m_TextureID, javascript);
	}
	
	public void OnApplicationQuit()
	{
		// cleanup on the window level, but berkelium stays active
		BerkeliumApi.Window.destroy(m_TextureID);
	}
}