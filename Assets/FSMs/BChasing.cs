using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BChasing : StateMachineBehaviour {
	public GameObject unit;
	public GameObject opponent;

	 // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Chasing entered");
		unit = animator.gameObject;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Chasing updated");
		Vector3 direction = opponent.transform.position - unit.transform.position;
		unit.transform.rotation = Quaternion.Slerp (unit.transform.rotation, Quaternion.LookRotation (direction), 1.0f * Time.deltaTime);
		unit.transform.Translate (0, 0, Time.deltaTime * 2.0f);
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Debug.Log ("Chasing exited");
	}
}
