using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BestTimeManager : MonoBehaviour {
	Text bestTime;
	public GameOverManager bTimes;

	// Use this for initialization
	void Start () {
		//bTimes = GetComponents<GameOverManager>();
		bestTime = GetComponent<Text>();
		bestTime.text = ("YEARS: " + bTimes.bestYear + "       MONTHS: " + bTimes.bestMonth);
	}
}
