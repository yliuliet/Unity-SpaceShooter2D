using UnityEngine;
using System.Collections.Generic;



public class WaveManager : MonoBehaviour {
	public GameObject[] hazards;
	public GameObject[] items;


	GameController gamecontroller;


	int i;
	int j;
	float[] chance;
	float[] item_chance;

	void Start(){
	}

	public GameObject sethazard(int WaveCount){
		if (WaveCount <= 20) {
			if (WaveCount == 1) {
				chance = new float[] {
					3, 1
				};
			}
			if (WaveCount == 2 || WaveCount == 3) {
				chance = new float[] {
					3, 1, 1, 0, 0
				};
			}
			if (WaveCount > 3 && WaveCount <= 6) {
				chance = new float[] {
					3, 3, 0, 1, 0
				};
			}
			if (WaveCount > 6 && WaveCount <= 11) {
				chance = new float[] {
					4, 2, 1, 2, 0
				};
			}
			if (WaveCount > 11 && WaveCount <= 13) {
				chance = new float[] {
					1, 1, 1, 2, 0
				};
			}
			if (WaveCount == 14 || WaveCount == 15) {
				chance = new float[] {
					0, 2, 1, 2, 0
				};
			}
			if (WaveCount > 15) {
				chance = new float[] {
					0, 3, 0, 3, 0, 1
				};
			}
		}

		if(WaveCount>18){
			chance = new float[] {
				0,3,0,3,0,1,1
			};
		}
		if (WaveCount % 7 == 0 && WaveCount > 0) {
			chance = new float[] {
				0,0,0,0,1
			};
		}
		i = probscontroller (chance);
		return hazards [i];
	}

	public GameObject setitems(GameObject hazard){
		if (hazard.name == hazards [0].name + "(Clone)") {
			item_chance = new float[] {
				10, 0, 0
			};

		} else if (hazard.name == hazards [1].name + "(Clone)") {
			item_chance = new float[] {
				8, 1, 0
			};

		} else if (hazard.name == hazards [2].name + "(Clone)") {
			item_chance = new float[] {
				7, 2, 0
			};

		} else if (hazard.name == hazards [3].name + "(Clone)") {
			item_chance = new float[] {
				9, 4, 0.8f
			};
		} else if (hazard.name == hazards [4].name + "(Clone)") {
			item_chance = new float[] {
				0, 2, 2
			};
		} else if (hazard.name == hazards [5].name + "(Clone)") {

			item_chance = new float[] {
				8, 4, 0.8f
			};
		} else if (hazard.name == hazards [6].name + "(Clone)") {
			item_chance = new float[] {
				7, 3, 0.8f
			};
		} else if (hazard.name == hazards [7].name ) {
			item_chance = new float[] {
				0, 0, 1
			};
		}

		j = probscontroller (item_chance);
		return items [j];

	}
		

	public  int probscontroller(float[] probs){
		float total = 0;

		foreach (float elem in probs) {
			total += elem;
		}
		float randomPoint = Random.value * total;

		for (int i=0; i < probs.Length; i++) {
			if(randomPoint<probs[i]){
				return i;
			}
			else{
				randomPoint -= probs[i];
			}
		}
		return probs.Length - 1;
	}
} 

