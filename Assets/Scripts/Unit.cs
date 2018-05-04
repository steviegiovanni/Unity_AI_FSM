using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every unit needs to have this script

public class Unit : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// trigger perception event to send position every time the unit moves
		EventManager.TriggerEvent ("PERCEPTION", new Hashtable (){{"NAME",this.gameObject.name},{"POSITION", transform.position}});
	}
}
