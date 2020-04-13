using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HouseValue : MonoBehaviour {
	public int minStartValue;
	public int maxStartValue;
	public int currentValue;
	public bool owned;
	public bool isMansion;
	public bool winCheck = false;
	int minTimeInc =6;
	int maxTimeInc = 12;
	float minSpeedOfInc = 0.5f;
	float maxSpeedOfInc = 1f;
	int rateOfInc;
	int rateOfDec;
	float speedOfInc;
	int timeOfInc;
	int timeAvailable;
	float time = 0f;
	AudioSource[] aSources;
	AudioSource error;
	AudioSource bought;
	AudioSource winner;
	bool decreasing = false;
	ParticleSystem buyable;
	
	void Start () {
		owned = false;
		currentValue = Random.Range (minStartValue, maxStartValue);
		if (isMansion==true){
			rateOfInc=0;
			rateOfDec=0;
		}
		else{
			if (currentValue > 2000){
			rateOfInc = (int)Random.Range (currentValue*0.025f, currentValue*0.05f);
			rateOfDec = (int)Random.Range (currentValue*0.01f, currentValue*0.03f);
			}
			else if (currentValue > 500 && currentValue < 2000){
				rateOfInc = (int)Random.Range (currentValue*0.03f, currentValue*0.06f);
				rateOfDec = (int)Random.Range (currentValue*0.015f, currentValue*0.035f);
			}
			else {
				rateOfInc = (int)Random.Range (currentValue*0.05f, currentValue*0.08f);
				rateOfDec = (int)Random.Range (currentValue*0.03f, currentValue*0.055f);
			}
			timeOfInc = Random.Range (minTimeInc, maxTimeInc);
			speedOfInc = Random.Range (minSpeedOfInc, maxSpeedOfInc);
			timeAvailable = Random.Range (10, 18);
		}
		InvokeRepeating("houseAlive", speedOfInc, speedOfInc);
		aSources = GetComponents<AudioSource>();
		error = aSources[0];
		bought = aSources[1];
		winner = aSources[1];
		buyable = GetComponent<ParticleSystem>();
		if ((PlayerPrefs.GetInt("Sound Bool"))==1){
			foreach(AudioSource aS in aSources){
			aS.mute = false;
			}
		}
		else if ((PlayerPrefs.GetInt("Sound Bool"))==0){
			foreach(AudioSource aS in aSources){
				aS.mute = true;
			}
		}
	}

	public void isBought(){
		if (owned || currentValue > ScoreManager.score)
			{
				error.Play ();
				//Debug.Log("Not enough money!");
				return;
			}
		owned = true;
		bought.Play();
		buyable.enableEmission=false;
		ScoreManager.score -= currentValue;
		//Debug.Log ("bought for -- " + currentValue);
	}

	public void isSold(){
		owned = false;
		ScoreManager.score += currentValue;
		//Debug.Log("Sold for -- " + currentValue);
	}

	void houseAlive(){
		if (time <= timeOfInc){
			currentValue += rateOfInc;
		}
		else if (time > timeOfInc){
			decreasing = true;
			currentValue -= rateOfDec;
		}
	}

	public void buyMansion(){
		if (ScoreManager.score >= 10000 && isMansion){
			winCheck = true;
			winner.Play ();
		}
		else{
			error.Play ();
			//Debug.Log ("Not Ready to win");
		}
	}

	void OnGUI(){
		Vector3 point = Camera.main.WorldToScreenPoint(transform.position);
		GUIStyle houseFonts = new GUIStyle(GUI.skin.button);
		GUIStyle mansionFont = new GUIStyle(GUI.skin.button);
		houseFonts.fontSize = 40;
		mansionFont.fontSize = 50;

		if (owned){
			GUI.backgroundColor=Color.blue;	
			GUI.contentColor = Color.green;
		}
		if (decreasing && !isMansion){
			GUI.contentColor = Color.Lerp(Color.red, Color.white, 0.3f);
			GUI.Button(new Rect(point.x-50, Screen.currentResolution.height - point.y-110, 200, 50), "▼ " + currentValue + " K", houseFonts); 
		}
		else if (!isMansion){
			GUI.Button(new Rect(point.x-50, Screen.currentResolution.height - point.y-110, 200, 50), "▲ " + currentValue + " K", houseFonts); 
		}
		else {
			GUI.Button(new Rect(point.x-70, Screen.currentResolution.height - point.y-100, 250, 60), currentValue + " K", mansionFont);
		}
	}

	void Update () {
		time += Time.deltaTime;
		if (time > timeAvailable && !owned && !isMansion){
			Destroy (gameObject);
		}
	}
}
