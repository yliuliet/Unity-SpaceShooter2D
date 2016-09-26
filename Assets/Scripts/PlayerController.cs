using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary{
	public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {
	public float speed;
	public float tilt;
	public Boundary boundary;

	public GameObject shot;
	public GameObject[] shotSpawns;

	public Transform ShotSpawn;
	GameObject refObj;
	GameObject gcObj;
	DataStorage ds;
	GameController gc;

	public float fireRate;
	public float PlayerLife;
	public float PlayerCurrentLife;
	public int weaponlevel;

	private float nextFire;
	private int difficulty;
	private bool debug;
	private Quaternion calibrationQuaternion;


	Vector3 movement;
	private Vector3 screenPoint;
	private Vector3 offset;


	void Start () {
		refObj = GameObject.Find ("DataStorage");
		gcObj = GameObject.Find ("GameController");

		ds = refObj.GetComponent<DataStorage> ();
		gc = gcObj.GetComponent<GameController> ();

		debug = gc.debug;
		difficulty = ds.Difficulty;
		resetStatus ();
		CalibrateAcclerometer ();
		PlayerCurrentLife = PlayerLife;
	}
	// Update is called once per frame
	void Update () {
		if (/*Input.GetButton ("Fire1")&& */ Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			shotSpawns = GameObject.FindGameObjectsWithTag ("ShotSpawn");
			foreach (var shotSpawn in shotSpawns) {
				ShotSpawn = shotSpawn.transform;
				Instantiate (shot, ShotSpawn.position, ShotSpawn.rotation);
			}
		}
	}

	void FixedUpdate(){
		
		if (!debug) {
			if (ds.PlayerControl == 0) {
				if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Moved) {
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition * 0.15f;
					movement = new Vector3 (touchDeltaPosition.x, 0.0f, touchDeltaPosition.y);
				} else
					movement = Vector3.zero;
			}

			if (ds.PlayerControl == 1) {
				Vector3 acclerationRaw = Input.acceleration;
				Vector3 accleration = FixAccleration (acclerationRaw);
				movement = new Vector3 (accleration.x, 0.0f, accleration.y);
			}
			GetComponent<Rigidbody>().velocity = movement*speed;
			GetComponent<Rigidbody>().position = new Vector3
				(	
					Mathf.Clamp(GetComponent<Rigidbody>().position.x,boundary.xMin,boundary.xMax),
					0.0f,
					Mathf.Clamp(GetComponent<Rigidbody>().position.z,boundary.zMin,boundary.zMax)
				);
		}
		GetComponent<Rigidbody>().rotation = Quaternion.Euler (0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt*ds.PlayerControl);
	}
	void OnMouseDown() {
		this.screenPoint = Camera.main.WorldToScreenPoint(transform.position);
		this.offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
	}

	void OnMouseDrag() {
		Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
		Vector3 currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + this.offset;
		transform.position = currentPosition;
	}

	public void CalibrateAcclerometer(){
		Vector3 accellerationSnapshot = Input.acceleration;
		Quaternion rotateQuaternion = Quaternion.FromToRotation (new Vector3 (0.0f, 0.0f, -1.0f), accellerationSnapshot);
		calibrationQuaternion = Quaternion.Inverse (rotateQuaternion);
	}

	Vector3 FixAccleration (Vector3 acceleration) {
		Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
		return fixedAcceleration;
	}

	public void resetStatus(){
		if (difficulty == 0) {
			fireRate = 0.5f;
			weaponlevel = 1;
		}else if (difficulty == 1) {
			fireRate = 0.35f;
			weaponlevel = 2;
		}
		GameObject.FindWithTag ("Shot").SetActive (false);
		gameObject.transform.FindChild ("Shot" + weaponlevel).gameObject.SetActive (true);
	}
}

