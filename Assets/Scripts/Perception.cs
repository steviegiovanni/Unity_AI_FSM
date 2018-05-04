using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : MonoBehaviour {
	public float radius;
	public bool alerted = false;

	void OnEnable(){
		EventManager.StartListening ("PERCEPTION", Perceived);
	}

	void OnDisable(){
		EventManager.StopListening ("PERCEPTION", Perceived);
	}

	void Perceived(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject perceivedObject = (GameObject)(param ["OBJECT"]);
		if (perceivedObject == this.gameObject)
			return;
		Vector3 position = perceivedObject.transform.position;
		if (Vector3.SqrMagnitude (position - this.transform.position) <= radius * radius)
			alerted = true;
		else
			alerted = false;
	}
}
