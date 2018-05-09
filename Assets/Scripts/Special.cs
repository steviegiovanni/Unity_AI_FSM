using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour {
	AI unitAI;

	// Use this for initialization
	void Start () {
		unitAI = GetComponent<AI> ();	
	}
	
	// Update is called once per frame
	void Update () {
		if (unitAI) {
			if (unitAI.health < 50) 
				unitAI.activeComponent = this.GetType();
		}
	}
}
