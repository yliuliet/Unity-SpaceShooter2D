using UnityEngine;
using System.Collections;

public class newWC : MonoBehaviour {

	public GameObject gamecontroller;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float startwait;
	public float endwait;
	public float firecount;

	private AudioSource audioSource;
	private int wavecount;
	GameController gm;
	// Use this for initialization
	void Start () {
		gamecontroller = GameObject.FindGameObjectWithTag ("GameController");
		gm = gamecontroller.GetComponent<GameController> ();
		wavecount = gm.Wavecount;
		audioSource = GetComponent<AudioSource> ();
		//firecontrol ();
		StartCoroutine("fire");
	}
		
	IEnumerator fire (){
		while (true) {
			yield return new WaitForSeconds (startwait);
			for (int i = 0; i < firecount; i++) {
				Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
				audioSource.Play ();
				yield return new WaitForSeconds (fireRate);
			}
			yield return new WaitForSeconds (endwait);
		}
	}

	public void firecontrol(){
		if (wavecount<10) {
			fireRate = 1.2f;
		}
		if (wavecount>=10) {
			fireRate = 1.0f;
		}
		if (wavecount>=15) {
			fireRate = 0.8f;
		}
		if (wavecount>=20) {
			fireRate = 0.6f;
		}
	}

}
