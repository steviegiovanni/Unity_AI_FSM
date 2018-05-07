using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every unit needs to have this script

public class Unit : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		// trigger perception event to send position every time the unit moves
		EventManager.TriggerEvent ("PERCEPTION", new Hashtable (){{"OBJECT", this.gameObject}});
	}

	void OnDisable(){
		// call event DESTROY to do cleanup of any reference to this object before destroying it
		if(EventManager.instance)
			EventManager.TriggerEvent ("DESTROY", new Hashtable (){ { "OBJECT", this.gameObject } });
	}
}
