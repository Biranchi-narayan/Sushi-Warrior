using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class BerkeliumContext : MonoBehaviour
{
	static BerkeliumContext instance;
	
	public static BerkeliumContext Instance
	{
		get 
		{
			if (null == instance)
            {
                instance = GameObject.FindObjectOfType(typeof(BerkeliumContext)) as BerkeliumContext;
                if (null == instance)
                {
                    GameObject container = new GameObject();
					DontDestroyOnLoad (container);
                    container.name = "BerkeliumContextContainer";
                    instance = container.AddComponent(typeof(BerkeliumContext)) as BerkeliumContext;
                }
				DontDestroyOnLoad(instance);
            }
			return instance;
		}
	}
	
	public BerkeliumContext()
	{
		BerkeliumApi.init();
	}
	
	void Update () {
		BerkeliumApi.update();
	}

	public void OnApplicationQuit()
	{
		// NOTE: Not really shutting down berkelium - it cant restart after destroy in the same process
		// (this happens every time you start/stop the game in the editor)
		//BerkeliumApi.destroy();
		BerkeliumContext.instance = null;
	}
}
