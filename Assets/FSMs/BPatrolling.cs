using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BPatrolling : StateMachineBehaviour {
	GameObject unit;				// the unit the this behaviour is attached to
	NavMeshAgent navMeshAgent;		// to handle movement
	GameObject [] waypoints;		// list of patrol points
	int currentWaypoint = 0;		// current waypoint index
	float stoppingDistance = 0.1f;	// accepted distance before moving to the next waypoint

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		// get the waypoints
		unit = animator.gameObject;
		AI unitAI = unit.GetComponent<AI> ();
		if (!unitAI)	// no AI component
			return;
		waypoints = unitAI.waypoints;

		// get the movement component
		navMeshAgent = unit.GetComponent<NavMeshAgent> ();

		if ((waypoints == null) || (waypoints.Length == 0))	// no waypoints (length = 0 or no AI) 
			return;

		if (!navMeshAgent)	// no movement component
			return;

		// set unit to move to the first waypoint
		navMeshAgent.stoppingDistance = stoppingDistance;
		navMeshAgent.SetDestination (waypoints [currentWaypoint % waypoints.Length].transform.position);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if ((waypoints == null) || (waypoints.Length == 0))	// no waypoints (length = 0 or no AI) 
			return;

		if (!navMeshAgent)	// no movement component
			return;

		if ((navMeshAgent.remainingDistance <= stoppingDistance) && !navMeshAgent.pathPending) { // has reached the current waypoint
			navMeshAgent.stoppingDistance = stoppingDistance;
			currentWaypoint++;
			navMeshAgent.SetDestination (waypoints [currentWaypoint % waypoints.Length].transform.position);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
}
