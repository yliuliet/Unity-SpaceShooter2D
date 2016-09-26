using UnityEngine;
using System.Collections;

public class BGsc : MonoBehaviour {

	public float controll;
	public float tileSizeZ;

	private Vector3 startPositions;
	// Use this for initialization
	void Start () {
		startPositions = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		float newposition = Mathf.Repeat (Time.time*controll,tileSizeZ);
		transform.position = startPositions + Vector3.forward * newposition;
	}
}
