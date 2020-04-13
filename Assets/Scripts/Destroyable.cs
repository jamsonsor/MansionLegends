using UnityEngine;
using System.Collections;

public class Destroyable : MonoBehaviour {
		

	public void RemoveMe(){
		//Debug.Log ("Destroyable's RemoveMe function called on " + name);
		Destroy (gameObject);
	}
}
