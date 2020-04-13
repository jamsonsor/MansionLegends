using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour {
	public static int month;
	public static int year;
	float time;
	
	Text text;
	// Use this for initialization
	void Awake () {
		text = GetComponent<Text>();
		year = 0;
		month = 0;
	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime/2f;
		month = (int)time;
		if (month == 13){
			year += 1;
			month = 1;
			time = 1f;
		}
		text.text = "YEAR: " + year + "        " + "MONTH: " + month;
	}
}
