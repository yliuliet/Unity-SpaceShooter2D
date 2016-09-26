using UnityEngine;
using System.Collections;

public class GiveItemEffect : MonoBehaviour {

	public GameObject effect;

    GameObject Player;
	GameObject Shot;
	public float spinspeed;
	private string itemName;

	PlayerController pc;

	void Start () {
		Player = GameObject.FindWithTag ("Player");
		if(Player != null){
			pc = Player.GetComponent<PlayerController> ();
		}
		itemName = gameObject.name;
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0.0f,0.0f,spinspeed);
	}

	void OnDestroy(){
		if (pc != null) {
			if (itemName == "fr_upgrade(Clone)" && pc.fireRate > 0.2f) {
				seteffect (2.7f);
				pc.fireRate -= 0.01f;
			}
			if (itemName == "hp_item(Clone)" && pc.PlayerCurrentLife != pc.PlayerLife) {
				seteffect (2);
				pc.PlayerCurrentLife += 1;
			}
			if (itemName == "wp_upgrade(Clone)") {
				pc.weaponlevel += 1;
				Shot = Player.transform.FindChild ("Shot" + pc.weaponlevel).gameObject;
				if (Shot != null) {
					seteffect (2.7f);
					GameObject.FindWithTag ("Shot").SetActive (false);
					Shot.SetActive (true);
				} else {
					pc.weaponlevel -= 1;
				}
			}
		}
	}

	public void seteffect (float height){
		GameObject item_effect = Instantiate (effect,new Vector3(Player.transform.position.x, Player.transform.position.y-height,Player.transform.position.z),Quaternion.identity) as GameObject;
		item_effect.transform.parent = Player.transform;
	}
}
