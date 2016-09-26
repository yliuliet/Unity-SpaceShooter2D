using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameController : MonoBehaviour {
	public Vector3 spawnValues;
	private int hazardcount;
	public float spawnWait;
	public float startWait;
	public float waveWait;

	public int highscore;

	public Text scoreText;
	public Text highscoreText;

	public GameObject gameOverText;
	public GameObject newHighScoreText;
	public GameObject restartbutton;
	public GameObject MenuButton;
	public GameObject MenuPanel;
	public GameObject Menu;
	public GameObject SureQuit;
	public GameObject Setting;
	public ToggleGroup Controltoggle;
	public Toggle JoyStick;
	public Toggle Accelerometer;
	public Slider PlayerHpSlider;

	GameObject Enemy;
    GameObject refObj;
	GameObject Player;

	DataStorage ds;
	WaveManager wm;
	PlayerController pc;

	Scene Game_scene;

	public bool gameOver;
	public bool debug;
	public int Wavecount;

	private int score;
	private int Control;
	private int Difficulty;
	private int currentvalue;

	void Start(){
		Game_scene = SceneManager.GetSceneByName ("Main");
		SceneManager.SetActiveScene (Game_scene);

		Player = GameObject.Find ("Player");
		refObj = GameObject.Find ("DataStorage");

		ds = refObj.GetComponent<DataStorage> ();
		pc = Player.GetComponent<PlayerController> ();
		wm = GetComponent<WaveManager> ();

		highscore = ds.HighScore;

		gameOver = false;
		score = 0;
		scoreText.text ="";
		highscoreText.text = "HighScore: "+ highscore;
		Control = ds.PlayerControl;
		Difficulty = ds.Difficulty;

		restartbutton.SetActive (false);
		MenuPanel.SetActive (false);
		SureQuit.SetActive (false);
		Setting.SetActive (false);

		UpdateScore ();
		StartCoroutine (SpawnWaves ());
	}

	void Update(){
		if (gameOver) {
			currentvalue = 0;
		} else {
			currentvalue = (int)(100 * pc.PlayerCurrentLife / pc.PlayerLife) ;
		}

		if (PlayerHpSlider.value > currentvalue) {
			PlayerHpSlider.value -= 1;
		} else if (PlayerHpSlider.value < currentvalue) {
			PlayerHpSlider.value += 1;
		}

	}
	IEnumerator SpawnWaves(){
		if (debug) {
			Wavecount = -1;
		} else {
			if (Difficulty == 0)
				Wavecount = 0;
			else
				Wavecount = 15;

		yield return new WaitForSeconds (startWait);
		while (true) {
			Wavecount = Wavecount + 1;
			hazard_count (Wavecount);
			for (int i = 0; i < hazardcount; i++) {
				GameObject hazard = wm.sethazard (Wavecount);
				if (Wavecount % 7 == 0) {
					spawnValues = new Vector3 (3.5f, 0.0f, 19);
				} else {
					spawnValues = new Vector3 (5.5f, 0.0f, 16);
				}
				Vector3 spawnPosition = new Vector3 (Random.Range (-spawnValues.x, spawnValues.x), Random.Range (-spawnValues.y, spawnValues.y), spawnValues.z);

				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			//Wait till all enemies are gone
			for (int i = 0; i < 50; i++) {
				yield return new WaitForSeconds (waveWait);
				if (GameObject.FindWithTag ("Enemy") == null) {
					break;
				} 
			}
			if (gameOver) {
				break;
			}
		}
	}
	}


		

	public void hazard_count(int wavecount){
		if (wavecount < 0) {
			hazardcount = 0;
		}
		if (wavecount < 3) {
			hazardcount = 5;
		}
		if (wavecount>=3) {
			hazardcount = 10;
		}
		if (wavecount>=10) {
			hazardcount = 12;
		}
		if (wavecount>=15) {
			hazardcount = 15;
		}
		if (wavecount>=20) {
			hazardcount = 18;
		}
		if (Wavecount %7 == 0) {
			hazardcount = 1;
		}
	}





	public void AddScore(int newScoreValue){
		score += newScoreValue;
		if (Difficulty == 1) {
			score = score + 10;
		}
		UpdateScore ();
	}

	void UpdateScore(){
		scoreText.text = score.ToString();

	}

	public void GameOver(){
		SendScoreData ();
		gameOverText.SetActive(true);
		if (score > highscore) {
			newHighScoreText.SetActive(true);
		}
		restartbutton.SetActive(true);
		gameOver = true;
	}

	public void showMenu(){
		if (gameOver) {
			restartbutton.SetActive (false);
		}
		Time.timeScale = 0;
		MenuPanel.SetActive(true);
	}

	public void RestartGame(){
		SceneManager.UnloadScene("Main");
		SceneManager.LoadSceneAsync("Main", LoadSceneMode.Additive);
		Time.timeScale = 1;
	}

	public void ResumeGame(){
		if (gameOver) {
			restartbutton.SetActive (true);
		}
		MenuPanel.SetActive (false);
//		PlayerController p = GetComponent<PlayerController> ();
//		p.CalibrateAcclerometer ();
		Time.timeScale = 1;
	}

	public void BacktoTitle(){
		SceneManager.LoadScene ("TITLE 1");
		Time.timeScale = 1;
	}

	public void GetSetting(){
		Menu.SetActive (false);
		Setting.SetActive (true);
		if (Control == 0)JoyStick.isOn = true;
		else Accelerometer.isOn = true;
	}

	public void changed(){
		string selectedlabel = Controltoggle.ActiveToggles ()
			.First ().GetComponentsInChildren<Text> ()
			.First (t => t.name == "Label").text;
		if (selectedlabel == "Drag") {
			Control = 0;
		} else {
			Control = 1;
		}
		ds.PlayerControl = Control;
		PlayerPrefs.SetInt (DataStorage.Control, ds.PlayerControl);
	}

	public void BackToMenu(){
		Setting.SetActive (false);
		Menu.SetActive (true);
	}

	public void sureQuit(){
		Menu.SetActive (false);
		SureQuit.SetActive (true);
	}

	public void yesQuit(){
		Application.Quit ();
		PlayerPrefs.Save ();
	}

	public void noQuit(){
		SureQuit.SetActive (false);
		Menu.SetActive (true);
	}

	public void SendScoreData(){
		ds.PlayerScore = score;
		PlayerPrefs.SetInt (DataStorage.Score, ds.PlayerScore);
		PlayerPrefs.SetInt (DataStorage.HiScore, ds.HighScore);
	}
}
