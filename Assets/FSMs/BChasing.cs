using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BChasing : StateMachineBehaviour {
	public GameObject unit;
	NavMeshAgent navMeshAgent;
	Attack attackComponent;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		unit = animator.gameObject;
		navMeshAgent = unit.GetComponent<NavMeshAgent> ();
		attackComponent = unit.GetComponent<Attack> ();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		AI unitAI = unit.GetComponent<AI> ();
		if (!unitAI)	// no AI component
			return;
		
		if (attackComponent) {
			GameObject target = unitAI.target;
			if (target) {
				navMeshAgent.stoppingDistance = attackComponent.range;
				navMeshAgent.SetDestination (target.transform.position);
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	}
}
