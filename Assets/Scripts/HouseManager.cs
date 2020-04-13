using UnityEngine;
using System.Collections;

public class HouseManager : MonoBehaviour {
	public GameObject house;
	GameObject objects;
	float spawnTime;
	public float maxTime;
	public float minTime;
	public Transform[] spawnPoints;
	public GameOverManager gameCheck;
	HouseManager thisScript;

	private float time;

	// Use this for initialization
	void Start () {
		SetRandomTime();
		time = 0f;
		thisScript = GetComponent<HouseManager>();
	}

	void FixedUpdate(){
		time += Time.deltaTime;
		if (gameCheck.gameOver){
			thisScript.enabled=false;
		}
		else if (time >= spawnTime){
			Spawn ();
			SetRandomTime();
		}
	}

	void Spawn () {
		time = 0f;
		int spawnPointIndex = Random.Range (0, spawnPoints.Length);
		if (checkIfPosEmpty(spawnPoints[spawnPointIndex].position)){
			//Debug.Log ("Spawn Allowed");
			Instantiate(house, spawnPoints[spawnPointIndex].position, Quaternion.identity);
		}
	}

	void SetRandomTime(){
		spawnTime = Random.Range (minTime, maxTime);
		//Debug.Log ("spawntime is " + spawnTime);
	}

	public bool checkIfPosEmpty(Vector3 targetPos){
		GameObject[] allHouses = GameObject.FindGameObjectsWithTag("House");
		foreach(GameObject current in allHouses){
			if (targetPos == current.transform.position){
				//Debug.Log ("Spawn not allowed, something already there");
				return false;
			}
		}
		return true;
	}
}
