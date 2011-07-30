using UnityEngine;
using System.Collections;
using OpenNI;
using NITE;

public class MyListnavController : MyHandPointControl {
	public Axis axis = Axis.X;
	public bool Reverse = false;
	
	public float InitialWaitTime = 0.6f;
	public float WaitTimeModifier = 0.75f;
	
	public float MinWaitTime = 0.15f;
	public float MaxWaitTime = 2.0f;
	
	public float Value { get; private set; }
	
	bool scrolling = false;

	ItemSelector selector;
	
	void Start()
	{
		// if no item selector in this game object, create one
		if (null == GetComponent<ItemSelector>())
		{
			Debug.LogWarning("No item selector for listnav controller in " + gameObject.name + ". Adding...");
			
			// first create a fader
			Fader fader = gameObject.AddComponent(typeof(Fader)) as Fader;
			switch (axis)
			{
				case Axis.X: fader.orientation = FaderOrientation.Horizontal; break;
				case Axis.Y: fader.orientation = FaderOrientation.Vertical; break;
			}
			
			// create the selector, attach the fader, and no hysterisis (list nav should always be responsive)
			ItemSelector selector = gameObject.AddComponent(typeof(ItemSelector)) as ItemSelector;
			selector.fader = fader;
			selector.hysterisis = 0.0f;
		}
	}
	
	void ItemSelector_Select(ItemSelector itemSelector)
	{
		if (0 == itemSelector.selectionIndex)
		{
			StartScrolling(false);
		}
		else if (itemSelector.selectionIndex == (itemSelector.numItems-1))
		{
			StartScrolling(true);
		}
		else
		{
			StopScrolling();
		}
	}

	void DoNext()
	{
		SendMessage("ListNav_Next", SendMessageOptions.DontRequireReceiver);
	}
	
	void DoPrev()
	{
		SendMessage("ListNav_Prev", SendMessageOptions.DontRequireReceiver);
	}
	
	IEnumerator NextLoop()
	{
		float waitTime = InitialWaitTime;
		while (true)
		{
			DoNext();
			yield return new WaitForSeconds(waitTime);
			waitTime = Mathf.Clamp(waitTime * WaitTimeModifier, MinWaitTime, MaxWaitTime);
		}
	}
	
	IEnumerator PrevLoop()
	{
		float waitTime = InitialWaitTime;
		while (true)
		{
			DoPrev();
			yield return new WaitForSeconds(waitTime);
			waitTime = Mathf.Clamp(waitTime * WaitTimeModifier, MinWaitTime, MaxWaitTime);
		}
	}

	void StartScrolling(bool forward)
	{
		if (scrolling) return;
		scrolling = true;
		
		SendMessage("ListNav_StartScrolling", forward, SendMessageOptions.DontRequireReceiver);
		if (forward ^ Reverse)
		{
			StartCoroutine("NextLoop");
		}
		else
		{
			StartCoroutine("PrevLoop");
		}
	}
	
	void StopScrolling ()
	{
		if (!scrolling)
			return;
		
		StopCoroutine ("NextLoop");
		StopCoroutine ("PrevLoop");
		SendMessage ("ListNav_StopScrolling", SendMessageOptions.DontRequireReceiver);
		scrolling = false;
	}
	
	void Hand_Destroy()
	{
		StopScrolling();
	}

	void OnGUI()
	{
	}
}
