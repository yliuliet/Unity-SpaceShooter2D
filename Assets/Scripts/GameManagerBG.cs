using UnityEngine;
using System.Collections;

public class GameManagerBG : MonoBehaviour {

	// Use this for initialization
	void Start () {
			UnityEngine.SceneManagement.SceneManager.LoadSceneAsync ("TITLE 1", UnityEngine.SceneManagement.LoadSceneMode.Additive);
	}
}
