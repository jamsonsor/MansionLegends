using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameOverManager : MonoBehaviour {
	public HouseValue winCond;
	public float restartDelay;
	public bool gameOver = false;
	Animator anim;
	float restartTimer;
	AudioSource[] menuSounds;
	AudioSource bg;
	AudioSource clicked;
	AudioSource errorSubmit;
	public Canvas quitMenu;
	public Button quitButton;
	public Canvas loadingBox;
	public int bestYear;
	public int bestMonth;
	public Canvas highscoreScreen;
	public Canvas highscoreBox;
	public Button highscoresButton;
	public Button playAgainButton;
	public Button submitScoreButton;
	public Button backButton;
	public InputField nameInput;
	public Text errorMsg;
	public Text gameScore;
	private string username;
	private int score;
	
	void Awake () {
		anim = GetComponent<Animator>();
		bg = GetComponent<AudioSource>();
		quitMenu = quitMenu.GetComponent<Canvas>();
		quitButton = quitButton.GetComponent<Button>();
		quitMenu.enabled=false;
		loadingBox = loadingBox.GetComponent<Canvas>();
		loadingBox.enabled=false;
		highscoreScreen = highscoreScreen.GetComponent<Canvas>();
		highscoreBox = highscoreBox.GetComponent<Canvas>();
		highscoresButton = highscoresButton.GetComponent<Button>();
		playAgainButton = playAgainButton.GetComponent<Button>();
		submitScoreButton = submitScoreButton.GetComponent<Button>();
		nameInput = nameInput.GetComponent<InputField>();
		backButton = backButton.GetComponent<Button>();
		highscoreScreen.enabled = false;
		highscoreBox.enabled = false;
		menuSounds = GetComponents<AudioSource>();
		errorMsg = errorMsg.GetComponent<Text>();
		errorMsg.enabled = false;
		gameScore = gameScore.GetComponent<Text>();
		bg = menuSounds[0];
		clicked = menuSounds[1];
		errorSubmit = menuSounds[2];
//		PlayerPrefs.DeleteAll();
//		PlayerPrefs.SetInt("Best Time", 99);
//		PlayerPrefs.SetInt("Best Month", 12);
		bestYear = PlayerPrefs.GetInt("Best Time", 99);
		bestMonth = PlayerPrefs.GetInt("Best Month", 12);

		if ((PlayerPrefs.GetInt("Music Bool"))==1){
			bg.mute = false;
		}
		else if ((PlayerPrefs.GetInt("Music Bool"))==0) {
			bg.mute = true;
		}
		
		if ((PlayerPrefs.GetInt("Sound Bool"))==1){
			clicked.mute = false;
		}
		else if ((PlayerPrefs.GetInt("Sound Bool"))==0){
			clicked.mute = true;
		}
	}

	public void quitPress(){
		clicked.Play ();
		quitMenu.enabled = true;
		loadingBox.enabled=false;
		quitButton.enabled = false;
		Time.timeScale = 0;
	}

	public void NoPress(){
		clicked.Play ();
		quitMenu.enabled = false;
		loadingBox.enabled=false;
		quitButton.enabled = true;
		Time.timeScale = 1;
	}

	public void yesPress(){
		clicked.Play();
		loadingBox.enabled=true;
		quitMenu.enabled=false;
		Time.timeScale = 1;
		Application.LoadLevel(0);
	}

	public void playAgainPress(){
		clicked.Play();
		Application.LoadLevel (Application.loadedLevel);
	}

	public void displayHighscore(){
		clicked.Play();
		highscoreScreen.enabled = true;
		backButton.enabled = true;
	}

	public void backButtonPress(){
		clicked.Play();
		highscoreScreen.enabled = false;
		highscoreBox.enabled = true;
	}

	public void submitscorePress(){
		if (nameInput.text == "" || nameInput.text.Contains(" ") || 
		    	nameInput.text.Length > 10 || nameInput.text.Length <3){
			errorSubmit.Play ();
			errorMsg.enabled = true;
		}
		else{
			clicked.Play();
			username = nameInput.text;
			errorMsg.enabled = false;
			errorMsg.text = "SUBMITTED!";
			errorMsg.color = Color.white;
			errorMsg.enabled = true;
			Highscores.AddNewHighscore(username, score);
			submitScoreButton.interactable = false;
			nameInput.interactable = false;
		}
	}

	void Update () {
		if (winCond.winCheck == true){
			bg.enabled=false;
			gameOver = true;
			if (restartTimer >= restartDelay){
				highscoreBox.enabled= true;
				score = (10000/TimeManager.year) - (10*TimeManager.month);
				gameScore.text = "SCORE: " + score;
			}
			if (TimeManager.year <= bestYear && gameOver){
				if ((TimeManager.year == bestYear && TimeManager.month < bestMonth) || TimeManager.year < bestYear){
					PlayerPrefs.SetInt ("Best Time", TimeManager.year);
					PlayerPrefs.SetInt("Best Month", TimeManager.month);
					//Debug.Log ("New high score!");
				}
			}
			anim.SetTrigger ("GameWon");
			GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("House");
			foreach (GameObject target in gameObjects){
				GameObject.Destroy (target);
			}
			restartTimer += Time.deltaTime;
			if (restartTimer >= 1.7){
				GameObject mansion = GameObject.FindGameObjectWithTag("Mansion");
				GameObject.Destroy (mansion);
			}
		}
	}
}
