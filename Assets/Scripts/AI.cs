using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// every unit with behaviour needs to have this script

public class AI : MonoBehaviour {
	public GameObject [] waypoints;
	public Dictionary<string,object> blackboard;

	// Use this for initialization
	void Start () {
		blackboard = new Dictionary<string,object> ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void WriteToBlackboard(string key, object value){
		object val;
		if (blackboard.TryGetValue (key, out val))
			blackboard [key] = value;
		else
			blackboard.Add (key, value);
	}

	public object ReadFromBlackboard(string key){
		object val;
		if (blackboard.TryGetValue (key, out val))
			return val;
		else
			return null;
	}
}
