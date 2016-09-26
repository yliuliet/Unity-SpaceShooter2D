using UnityEngine;
using System.Collections;


public class Destroybycontacy : MonoBehaviour {
	
	public GameObject explosion;
	public GameObject Playergone;
	public GameObject wp_upgrade;
	GameObject item;
	GameObject player;

	public int scoreValue;
	public int Life;
	public float EnemyDamage;
	public float PlayerDamage;
	public bool boss;

	private PlayerController playercontroller;
	private float enemycurrentlife;
	private float playercurrentlife;
	private Vector3 ItemDropPosition;

	GameController gameController;
	WaveManager waveManager;

	void Start(){
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		player = GameObject.FindWithTag ("Player");
		gameController = gameControllerObject.GetComponent<GameController> ();
		waveManager = gameControllerObject.GetComponent<WaveManager> ();

		if (player != null) {
			playercontroller = player.GetComponent<PlayerController> ();
		}
		enemycurrentlife = Life;
	}

	void OnTriggerEnter(Collider other){
		if (other.CompareTag  ("Boundary") || other.CompareTag ("Enemy")||other.CompareTag ("EnemyBolt")||other.CompareTag  ("Item")) {
			return;
		}

		if (explosion != null) {
			Instantiate (explosion,transform.position,transform.rotation);
		}

		if (other.tag == "Player") {
			if (gameObject.CompareTag ("Item")) {
				Destroy (gameObject);
			} else getdamage (EnemyDamage, other);
		}

		if (other.tag == "Bolt") {
			if (gameObject.CompareTag ("Item") || gameObject.CompareTag ("Bolt")) {
				return;
			} else if(gameObject.CompareTag ("EnemyBolt") ){
				Destroy (other.gameObject);
				Destroy (gameObject);
			}else {
				PlayerDamage = 1;
				dodamage (PlayerDamage, other);
			}
		}
	}

	public void dodamage(float damage,Collider other){
		if (enemycurrentlife == 0) {
			gameController.AddScore (scoreValue);
			item = waveManager.setitems (gameObject);
			if (!item.CompareTag("Dummy")) {
				Instantiate (item, transform.position, Quaternion.identity);
			}
			if (boss) {
				Instantiate (wp_upgrade, transform.position, Quaternion.identity);
			}
			Destroy (other.gameObject);
			Destroy (gameObject);

		} else {
			enemycurrentlife = enemycurrentlife - damage;
			Destroy (other.gameObject);
		}
	}

	public void getdamage(float damage,Collider other){
		playercurrentlife = playercontroller.PlayerCurrentLife;
		playercurrentlife = playercurrentlife - damage;
		if (playercurrentlife <= 0) {
			Instantiate (Playergone, transform.position, transform.rotation);
			Destroy (other.gameObject);
			Destroy (gameObject);
			gameController.GameOver ();
		} else {
			playercontroller.PlayerCurrentLife = playercurrentlife;
			Instantiate (explosion,transform.position,transform.rotation);
			playercontroller.resetStatus ();
			//untargetable ();
		}
	}

	public void untargetable(){
		Animation anim = player.GetComponent<Animation> ();
		anim.Play ();
	}
}