using UnityEngine;
using System.Collections;

public class InsaveManeruar : MonoBehaviour {

	public float dodge;
	public float smooth;
	public float tilt;
	public bool boss;

	public Vector2 startWait;
	public Vector2 maneuverTime;
	public Vector2 maneuverWait;
	public Boundary boundary;

	private float targetManeurer;
	private Rigidbody rb;
	private float currentspeed;
	// Use this for initialization
	void Start () {
		rb = this.GetComponent<Rigidbody> ();
		currentspeed = rb.velocity.z;
		StartCoroutine (Evade ());
	}

	IEnumerator Evade(){
		yield return new WaitForSeconds (Random.Range(startWait.x,startWait.y));

		while (true) {
			targetManeurer = Random.Range(1,dodge)* -Mathf.Sign(transform.position.x);
			yield return new WaitForSeconds(Random.Range(maneuverTime.x,maneuverTime.y));
			targetManeurer = 0;
			yield return new WaitForSeconds(Random.Range(maneuverWait.x,maneuverWait.y));
		}
	}


	// Updateis called once per frame
	void FixedUpdate () {
		if (boss) {
			if (rb.position.z < 11)
				currentspeed = 0.0f;
		}
		float newManuver = Mathf.MoveTowards (rb.velocity.x, targetManeurer,Time.deltaTime* smooth);
		rb.velocity = new Vector3 (newManuver, 0.0f, currentspeed);
		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f,0.0f,rb.velocity.x*-tilt);
	}
}
