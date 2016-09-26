using UnityEngine;
using System.Collections;

public class RondomRotate : MonoBehaviour {

	public float tamble;

	void Start (){
		GetComponent<Rigidbody>().angularVelocity = Random.insideUnitSphere * tamble;
	}
}
