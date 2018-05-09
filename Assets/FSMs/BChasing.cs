using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BChasing : StateMachineBehaviour {
	GameObject unit;
	AI unitAI;
	NavMeshAgent navMeshAgent;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		unit = animator.gameObject;
		unitAI = unit.GetComponent<AI> ();
		if(!unitAI)
			Debug.LogWarning ("No AI assigned to this game object: " + unit.name);
		navMeshAgent = unit.GetComponent<NavMeshAgent> ();
		if (!navMeshAgent)
			Debug.LogWarning ("No navmesh agent assigned to this game object: " + unit.name);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (unitAI) {
			GameObject target = unitAI.ReadFromBlackboard ("HIGHESTTHREAT") as GameObject;
			if (target && navMeshAgent) 
				navMeshAgent.SetDestination (target.transform.position);
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
}
