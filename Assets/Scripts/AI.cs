using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// every unit with behaviour needs to have this script

public class AI : MonoBehaviour {
	Animator anim;
	public GameObject [] waypoints;
	public Type activeComponent = null;
	public GameObject target = null;
	public int health;
	public Dictionary<string,object> blackboard;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		blackboard = new Dictionary<string,object> ();
	}
	
	// Update is called once per frame
	void Update () {
		// get percepts if object has perception component 


		anim.SetBool ("special", (activeComponent != null));

		//anim.SetFloat ("distance", Vector3.Distance(transform.position, player.transform.position));
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
