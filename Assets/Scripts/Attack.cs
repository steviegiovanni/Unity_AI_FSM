using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : AIComponent {
	public float range = 1.0f;
	public float cooldown = 5.0f;
	private float cooldownRemaining = 0.0f;

	override public void Start(){
		base.Start ();
	}
		
	void Update(){
		cooldownRemaining -= Time.deltaTime;
		if (unitAI) {
			GameObject highestThreat = unitAI.ReadFromBlackboard ("HIGHESTTHREAT") as GameObject;
			if (highestThreat) {
				if (FSM)
					FSM.SetBool ("inrange", (Vector2.SqrMagnitude (highestThreat.transform.position - this.transform.position) <= range * range));
			} 
		}
	}

	// attack target
	public void AttackTarget(GameObject target){
		if (cooldownRemaining <= 0.0f) {
			Debug.Log ("Attacking taget");
			cooldownRemaining = cooldown;
		}
	}
}
