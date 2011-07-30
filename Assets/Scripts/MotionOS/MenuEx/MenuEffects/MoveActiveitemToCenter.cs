using UnityEngine;
using System.Collections;

public class MoveActiveitemToCenter : MonoBehaviour {
	
	public float rate = 5.0f;
	
	private Transform target;
	private Vector3 targetPos;
	
	void Update()
	{
		if (target)
		{
			target.position = Vector3.Lerp(target.position, targetPos, Time.deltaTime * rate);
		}
	}
	
	void Menu_Activate(Transform item)
	{
		Transform itemsContainer = item.parent;
		Transform menuTransform = itemsContainer.parent;
		
		Vector3 diff = item.position - menuTransform.position;
		target = itemsContainer;
		targetPos = itemsContainer.position - diff;
	}
}
