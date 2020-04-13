using UnityEngine;
using System.Collections;

public class BuySell : MonoBehaviour {
	bool isOwned;
	AudioSource[] gameSounds;
	AudioSource soldThis;
	BuySell thisScript;

	// Use this for initialization
	void Start(){
		gameSounds = GetComponents<AudioSource>();
		soldThis = gameSounds[0];
		thisScript = GetComponent<BuySell>();
		if ((PlayerPrefs.GetInt("Sound Bool"))==1){
			foreach(AudioSource gS in gameSounds){
				gS.mute = false;
			}
		}
		else if ((PlayerPrefs.GetInt("Sound Bool"))==0){
			foreach(AudioSource gS in gameSounds){
				gS.mute = true;
			}
		}
	}

	void Update () {
		if (Input.GetMouseButtonDown(0)){
			Ray toMouse = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit rhInfo;
			bool didHit = Physics.Raycast (toMouse, out rhInfo, 500.0f);
			if(didHit){
				HouseValue thisHouse = rhInfo.collider.GetComponent<HouseValue>();
				isOwned = thisHouse.owned;
				if (didHit && thisHouse.isMansion){
					thisHouse.buyMansion();
					if (thisHouse.winCheck){
						thisScript.enabled=false;
					}
				}
				else if (didHit && isOwned){
					//Debug.Log (rhInfo.collider.name + " -- is sold");
					thisHouse.isSold();
					Destroyable destScript = rhInfo.collider.GetComponent<Destroyable>();
					if (destScript){
						destScript.RemoveMe ();
					}
					soldThis.Play();
				}
				else if (didHit && !isOwned){
					HouseValue bought = rhInfo.collider.GetComponent<HouseValue>();
					if (bought){
						bought.isBought();
					}
				}
			}
			else {
				Debug.Log ("Clicked on nothing");
			}
		}
	}
}
