using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// every unit with behaviour needs to have this script

public class AI : MonoBehaviour {
	Animator anim;
	public GameObject [] waypoints;

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

			if (percepts.Count == 0)
				anim.SetBool ("alerted", false);
			else 
				anim.SetBool ("alerted", true);

			// update threat table if object has a threat table
			ThreatTable threatTable = GetComponent<ThreatTable> ();
			if (threatTable) {
				threatTable.ProcessPercepts (percepts);
				GameObject highestThreatObject = threatTable.GetHighestThreat ();

				Attack attackComponent = GetComponent<Attack> ();
				if (attackComponent) {
					attackComponent.target = highestThreatObject;
					anim.SetBool ("reachable", attackComponent.IsTargetInrange ());
				}
			}
		}

		//anim.SetFloat ("distance", Vector3.Distance(transform.position, player.transform.position));
	}
}
