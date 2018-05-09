using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAttacking : StateMachineBehaviour {
	GameObject unit;
	AI unitAI;
	Attack attackComponent;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		unit = animator.gameObject;
		unitAI = unit.GetComponent<AI> ();
		if(!unitAI)
			Debug.LogWarning ("No AI assigned to this game object: " + unit.name);
		attackComponent = unit.GetComponent<Attack> ();
		if (!attackComponent) 
			Debug.LogWarning ("No Attack component assigned to this game object: " + unit.name);
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (attackComponent) {
			if (unitAI) {
				GameObject target = unitAI.ReadFromBlackboard ("HIGHESTHREAT") as GameObject;
				if(target)
					attackComponent.AttackTarget (target);
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		
	}
}
