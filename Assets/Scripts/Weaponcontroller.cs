using UnityEngine;
using System.Collections;

public class Weaponcontroller : MonoBehaviour {

	public GameObject gamecontroller;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	public float delay;
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
		InvokeRepeating ("Fire",delay,fireRate);
	}

	void Fire(){
		Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
		audioSource.Play ();
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
