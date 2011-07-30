using UnityEngine;
using System.Collections;

public class SmoothLookAtOnNavigate : MonoBehaviour {

	public SmoothLookAt smoothLookAt;
	
	void Navigator_ActivatedItem(Transform item)
	{
		if (smoothLookAt)
		{
			smoothLookAt.target = item;
		}
	}
}
