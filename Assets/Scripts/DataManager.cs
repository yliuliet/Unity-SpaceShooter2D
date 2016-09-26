using UnityEngine;
using System.Linq;

public class DataManager :MonoBehaviour{
	
	private DataStorage dataStorage = null;

	private void Awake(){
		this.dataStorage = this.gameObject.GetComponent<DataStorage>();
		LoadData ();
	}


	public void LoadData(){
		this.dataStorage.PlayerScore = (PlayerPrefs.HasKey (DataStorage.HiScore) == true) ?
			PlayerPrefs.GetInt (DataStorage.HiScore) : 0;
		this.dataStorage.PlayerScore = (PlayerPrefs.HasKey (DataStorage.Score) == true) ?
			PlayerPrefs.GetInt (DataStorage.Score) : 0;
		this.dataStorage.PlayerControl = (PlayerPrefs.HasKey (DataStorage.Control) == true) ?
			PlayerPrefs.GetInt (DataStorage.Control) : 0;
		this.dataStorage.Difficulty = (PlayerPrefs.HasKey (DataStorage.Diff) == true) ?
			PlayerPrefs.GetInt (DataStorage.Diff) : 0;
	}

	public void RemoveDataStorage(){
		Destroy(this.dataStorage);
		this.dataStorage = null;
	}
	public void AddDataStorage(){
		this.dataStorage = this.gameObject.AddComponent<DataStorage>();
	}
}
