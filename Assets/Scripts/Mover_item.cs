using UnityEngine;
using System.Collections;

public class Mover_item : MonoBehaviour {
	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f,0.0f,speed);
	}
}
