using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MainMenu : MonoBehaviour {
	public Canvas instrMenu;
	public Canvas quitMenu;
	public Button startText;
	public Button instrText;
	public Button quitButton;
	public Toggle musicToggle;
	public Toggle soundToggle;
	public Canvas loadingBox;
	public Canvas highscoreScreen;
	public Button highscoreButton;
	AudioSource[] menuSounds;
	AudioSource clicked;
	AudioSource menuMusic;
	
	void Start (){
		instrMenu = instrMenu.GetComponent<Canvas>(); 
		quitMenu = quitMenu.GetComponent<Canvas>();
		startText = startText.GetComponent<Button> ();
		instrText = instrText.GetComponent<Button> ();
		quitButton = quitButton.GetComponent<Button>();
		instrMenu.enabled = false;
		quitMenu.enabled = false;
		menuSounds = GetComponents<AudioSource>();
		menuMusic = menuSounds[0];
		clicked = menuSounds[1];
		musicToggle = musicToggle.GetComponent<Toggle>();
		soundToggle = soundToggle.GetComponent<Toggle>();
		loadingBox = loadingBox.GetComponent<Canvas>();
		loadingBox.enabled=false;
		highscoreScreen = highscoreScreen.GetComponent<Canvas>();
		highscoreScreen.enabled = false;
		highscoreButton = highscoreButton.GetComponent<Button>();

		if ((PlayerPrefs.GetInt("Music Bool"))==0){
			musicToggle.isOn = false;
		}
		else if ((PlayerPrefs.GetInt("Music Bool"))==1) {
			musicToggle.isOn = true;
		}
		
		if ((PlayerPrefs.GetInt("Sound Bool"))==1){
			soundToggle.isOn = true;
		}
		else if ((PlayerPrefs.GetInt("Sound Bool"))==0){
			soundToggle.isOn = false;
		}
	}

	public void musicPress(){
		if(musicToggle.isOn == true){
			PlayerPrefs.SetInt("Music Bool", 1);
			Debug.Log("Music Enabled");
			menuMusic.mute = false;
		}
		else{
			PlayerPrefs.SetInt("Music Bool", 0);
			Debug.Log("Music Disabled");
			menuMusic.mute = true;
		}
	}

	public void soundPress() {
		if(soundToggle.isOn == true){
			PlayerPrefs.SetInt("Sound Bool", 1);
			Debug.Log("Sound Enabled");
			clicked.mute = false;
		}
		else{
			PlayerPrefs.SetInt("Sound Bool", 0);
			Debug.Log("Sound Disabled");
			clicked.mute = true;
		}
	}

	public void InstructPress(){
		clicked.Play();
		instrMenu.enabled = true; 
		startText.enabled = false; 
		instrText.enabled = false;
		quitMenu.enabled = false;
		quitButton.enabled = false;
	}
	
	public void QuitPress() {
		clicked.Play();
		instrMenu.enabled = false; 
		startText.enabled = false; 
		instrText.enabled = false;
		quitMenu.enabled = true;
		quitButton.enabled = false;
	}

	public void NoPress(){
		clicked.Play ();
		instrMenu.enabled = false; 
		startText.enabled = true; 
		instrText.enabled = true;
		quitMenu.enabled = false;
		quitButton.enabled = true;
	}
	
	public void yesPress(){
		clicked.Play();
		Application.Quit();
	}
	
	public void StartLevel () {
		loadingBox.enabled=true;
		clicked.Play ();
		Application.LoadLevel (1); 
	}
	
	public void BackButton () {
		clicked.Play();
		instrMenu.enabled = false; 
		startText.enabled = true; 
		instrText.enabled = true;
		quitMenu.enabled = false;
		quitButton.enabled = true;
		highscoreScreen.enabled = false;
		highscoreButton.enabled = true;
	}

	public void HighscoreScreen(){
		clicked.Play();
		highscoreScreen.enabled = true;
		instrMenu.enabled = false; 
		startText.enabled = false; 
		instrText.enabled = false;
		quitMenu.enabled = false;
		quitButton.enabled = false;
	}
}
