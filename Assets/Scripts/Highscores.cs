using UnityEngine;
using System.Collections;

public class Highscores : MonoBehaviour {

	const string privateCode = "XrucvouUJE-Eo8cDWrnvnAi69iUX4OcESZd_K-QDUpQQ";
	const string publicCode = "558dd5ee6e51b6085c95bd28";
	const string webURL = "http://dreamlo.com/lb/";

	public Highscore[] highscoresList;
	static Highscores instance;
	DisplayHighscore highscoresDisplay;

	void Awake(){
		instance = this;
		highscoresDisplay = GetComponent<DisplayHighscore>();
	}

	public static void AddNewHighscore(string username, int score){
		instance.StartCoroutine(instance.UploadNewHighscore(username, score));
	}

	IEnumerator UploadNewHighscore(string username, int score){
		WWW www = new WWW(webURL + privateCode + "/add/" + WWW.EscapeURL (username) + "/" + score);
		yield return www;

		if (string.IsNullOrEmpty(www.error)){
			print ("Upload Successful");
			DownloadHighScores();
		}
		else {
			print("Error uploading: " + www.error);
		}
	}

	public void DownloadHighScores(){
		StartCoroutine("DownloadHighscoreFromDatabase");
	}

	IEnumerator DownloadHighscoreFromDatabase(){
		WWW www = new WWW(webURL + publicCode + "/pipe/" + "/xml-seconds-asc");
		yield return www;
		
		if (string.IsNullOrEmpty(www.error)){
			FormatHighscores (www.text);
			highscoresDisplay.OnHighScoresDownloaded(highscoresList);
		}
		else {
			print("Error uploading: " + www.error);
		}
	}

	void FormatHighscores(string textStream){
		string[] entries = textStream.Split (new char[] {'\n'}, System.StringSplitOptions.RemoveEmptyEntries);
		highscoresList = new Highscore[entries.Length];
		for (int i = 0; i < entries.Length; i++){
			string[] entryInfo = entries[i].Split (new char[] {'|'});
			string username = entryInfo[0];
			int score = int.Parse(entryInfo[1]);
			highscoresList[i] = new Highscore(username, score);
			print (highscoresList[i].username + ": " + highscoresList[i].score);
		}
	}
}

public struct Highscore{
	public string username;
	public int score;

	public Highscore(string _username, int _score){
		username = _username;
		score = _score;
	}
}
