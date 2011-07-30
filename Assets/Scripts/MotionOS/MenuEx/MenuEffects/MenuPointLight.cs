using UnityEngine;
using System.Collections;

public class MenuPointLight : MonoBehaviour {
	
	public Light light;
	public MenuBase menu;
	public Vector3 lightOffset;
	public float lightIntensity;
	// Use this for initialization
	
	void Start() {
		GameObject lightGameObject = new GameObject("Point Light");
		light = lightGameObject.AddComponent<Light>();
		light.type = LightType.Point;
		light.color = Color.white;
		light.type = LightType.Point;
		light.intensity = 0.0f;//off
	}
	
	void Update() {
		if (menu.ActiveItemIndex != -1) {
			light.intensity = lightIntensity;
			light.transform.position = menu.ActiveItem.position + lightOffset;
		}
	}
}
