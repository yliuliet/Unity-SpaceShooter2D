using UnityEngine;
using System.Collections;

public class DataStorage : MonoBehaviour{
	

	public const string HiScore = "HighScore";
	public const string Score = "PlayerScore";
	public const string Control ="Setting";
	public const string Diff = "Difficulty";

	public int PlayerControl{ get; set;}
	public int Difficulty{ get; set;}
	public int HighScore { get;  set; }
	private int playerScore = 0;
	public int PlayerScore 
	{ 
		get{return playerScore;}
		set
		{
			playerScore = value;
			if(value > HighScore)
			{
				HighScore = value;
			}
		}
	}

	private void Awake(){
		DontDestroyOnLoad(this.gameObject);
	}
}


