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
		if (!param.ContainsKey ("NAME"))
			return;
		if (!param.ContainsKey ("POSITION"))
			return;

		string name = (string)(param ["NAME"]);
		if (name == this.gameObject.name)
			return;
		
		Vector3 position = (Vector3)(param ["POSITION"]);
		if (Vector3.SqrMagnitude (position - this.transform.position) <= radius * radius)
			alerted = true;
		else
			alerted = false;
	}
}
