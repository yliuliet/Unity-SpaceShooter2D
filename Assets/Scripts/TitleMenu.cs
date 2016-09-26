using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TitleMenu : MonoBehaviour {

	Animator anim;

	int bttHash = Animator.StringToHash("Backtotitle");
	int startHash = Animator.StringToHash("StartGame");
	int getsetHash = Animator.StringToHash("GetSetting");

	public static bool btt;
	public static bool getset;
	public static bool start;
	public static bool isstarted;


	void Start () {
		isstarted = false;
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isstarted) {
			SceneManager.UnloadScene ("TITLE 1");
			SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
			isstarted = false;
		}
			
		if (btt) {
			anim.SetTrigger (bttHash);
			btt = false;

		}
		if (start) {
			anim.SetTrigger (startHash);
			start = false;
		}
		if (getset) {
			anim.SetTrigger(getsetHash);
			getset = false;
		}
	}

	public void startgame(){
		isstarted = true;
	}
}
