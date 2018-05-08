using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// every unit with behaviour needs to have this script

public class AI : MonoBehaviour {
	Animator anim;
	public GameObject [] waypoints;
	public GameObject target = null;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		// get percepts if object has perception component 
		Perception perception = GetComponent<Perception> ();
		if (perception) {
			Dictionary<GameObject,Vector3> percepts = perception.percepts;

			// update threat table if object has a threat table
			ThreatTable threatTable = GetComponent<ThreatTable> ();
			if (threatTable) {
				threatTable.ProcessPercepts (percepts);
				target = threatTable.GetHighestThreat ();

				if (target)
					anim.SetBool ("alerted", true);
				else 
					anim.SetBool ("alerted", false);

				Attack attackComponent = GetComponent<Attack> ();
				if (attackComponent)
					anim.SetBool ("reachable", attackComponent.IsTargetInrange (target));
			}
		}

		//anim.SetFloat ("distance", Vector3.Distance(transform.position, player.transform.position));
	}
}
