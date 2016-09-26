using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Linq;

public class GameManager : MonoBehaviour {

	DataStorage ds;
	GameObject DBreload;

	public ToggleGroup Controltoggle;
	public Toggle JoyStick;
	public Toggle Accelerometer;
	public Toggle Easy;
	public Toggle Normal;

	public GameObject Buttons;
	public GameObject SureQuit;
	public GameObject Setting;
	public GameObject dataStorage;

	public Text HighScoreText;
	public Text NormalText;

	private int HighScore;
	private int Control;
	private int Difficulty;

	void Start(){
		DBreload = GameObject.FindGameObjectWithTag ("Storage");
		if (DBreload == null) {
			dataStorage.SetActive (true);
			ds = dataStorage.GetComponent<DataStorage> ();
		} else {
			ds = DBreload.GetComponent<DataStorage> ();
		}
		SureQuit.SetActive (false);
		SceneManager.LoadSceneAsync("Background", LoadSceneMode.Additive);
		Control = ds.PlayerControl;
		Difficulty = ds.Difficulty;
		updateHighScore ();
	}

	public void StartGame(){
		TitleMenu.start = true;
	}

	public void sureQuit(){
		Buttons.SetActive (false);
		SureQuit.SetActive (true);
	}

	public void yesQuit(){
		Application.Quit ();
		PlayerPrefs.Save ();
	}

	public void noQuit(){
		SureQuit.SetActive (false);
		Buttons.SetActive (true);
	}
		

	public void BacktoMN (){
		TitleMenu.btt = true;
	}

	public void GetConfig(){
		Setting.SetActive (true);
		if (ds.HighScore < 1000) {
			Difficulty = 0;
			Normal.interactable = false;
			NormalText.color = Color.gray;
		} else NormalText.color = Color.white;

		if (Control == 0)JoyStick.isOn = true;
		else Accelerometer.isOn = true;

		if (Difficulty == 0)Easy.isOn = true;
		else Normal.isOn = true;

 		TitleMenu.getset = true;
	}

	public void change_cnt(){
		/*string selectedlabel = Controltoggle.ActiveToggles ()
			.First ().GetComponentsInChildren<Text> ()
			.First (t => t.name == "Label").text;
		if (selectedlabel == "Drag") {
			Control = 0;
		} else {
			Control = 1;
		}*/
		if (JoyStick.isOn) {
			Control = 0;
		} else {
			Control = 1;
		}
		ds.PlayerControl = Control;
		PlayerPrefs.SetInt (DataStorage.Control, ds.PlayerControl);
	}

	public void change_diff(){

		if (Easy.isOn) {
			Difficulty = 0;
		} else {
			Difficulty = 1;
		}
		ds.Difficulty = Difficulty;
		PlayerPrefs.SetInt (DataStorage.Diff, ds.Difficulty);
	}

	public void ResetHighScore(){
		PlayerPrefs.DeleteKey(DataStorage.HiScore);
		PlayerPrefs.DeleteKey(DataStorage.Score);
		ds.HighScore = 0;
		ds.PlayerScore = 0;
		updateHighScore ();
	}

	public void updateHighScore(){
	   HighScore = ds.HighScore;
	   HighScoreText.text = "HighScore:" + HighScore;
	}
}
