using UnityEngine;
using System.Collections;

public class renderlight : MonoBehaviour {
	Camera camera;
	public Light adjustlight;

	void Start(){
		camera = GetComponent<Camera> ();
	}

	void OnPreCull(){
		if(adjustlight != null){
			adjustlight.enabled = false;
		}
	}

	void OnPreRender(){
		if(adjustlight != null){
			adjustlight.enabled = false;
		}
	}

	void OnPostRender(){
		if(adjustlight != null){
			adjustlight.enabled = true;
		}
	}
}
