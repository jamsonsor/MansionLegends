using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayHighscore : MonoBehaviour {

	public Text[] highscoreText;
	Highscores highscoreManager;

	void Start () {
		for (int i = 0; i < highscoreText.Length; i++){
			highscoreText[i].text = i+1 + ". Fetching...";
		}

		highscoreManager = GetComponent<Highscores>();

		StartCoroutine ("RefreshHighscores");
	}

	public void OnHighScoresDownloaded(Highscore[] highscoreList){
		for (int i = 0; i < highscoreText.Length; i++){
			highscoreText[i].text = i+1 + ". ";
			if (highscoreList.Length > i){
				highscoreText[i].text += highscoreList[i].username + " - " + highscoreList[i].score;
			}
		}
	}

	IEnumerator RefreshHighscores(){
		while(true){
			highscoreManager.DownloadHighScores();
			yield return new WaitForSeconds(5);
		}
	}
}
