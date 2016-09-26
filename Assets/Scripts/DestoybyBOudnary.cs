using UnityEngine;
using System.Collections;

public class DestoybyBOudnary : MonoBehaviour {

	void OnTriggerExit(Collider other){
		Destroy (other.gameObject);
	}
}
