using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class LayoutBase : MonoBehaviour {
	public abstract void LayoutItems(List<Transform> items);
}
