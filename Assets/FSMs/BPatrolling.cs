using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BPatrolling : StateMachineBehaviour {
	GameObject unit;
	NavMeshAgent agent;
	GameObject [] waypoints;
	int currentWaypoint;

	void Awake(){
		waypoints = GameObject.FindGameObjectsWithTag ("waypoint");
	}

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Patrolling entered");
		unit = animator.gameObject;
		agent = unit.GetComponent<NavMeshAgent> ();
		currentWaypoint = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Patrolling updated");
		if (waypoints.Length == 0)
			return;

		if (Vector3.Distance (waypoints [currentWaypoint].transform.position, unit.transform.position) < 1.0f) {
			currentWaypoint++;
			if (currentWaypoint >= waypoints.Length)
				currentWaypoint = 0;
		}

		agent.SetDestination (waypoints [currentWaypoint].transform.position);

		// rotate towards target
		//Vector3 direction = waypoints[currentWaypoint].transform.position - unit.transform.position;
		//unit.transform.rotation = Quaternion.Slerp (unit.transform.rotation, Quaternion.LookRotation (direction), 1.0f * Time.deltaTime);
		//unit.transform.Translate (0, 0, Time.deltaTime * 2.0f);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Patrolling exited");
	}
}
