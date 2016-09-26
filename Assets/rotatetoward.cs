using UnityEngine;
using System.Collections;

public class rotatetoward : MonoBehaviour {

	private Transform Player;
	// Use this for initialization
	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt (Player);
	}
}
