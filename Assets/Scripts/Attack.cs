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
		if (unitAI && FSM) {
			GameObject highestThreat = unitAI.ReadFromBlackboard ("HIGHESTTHREAT") as GameObject;
			GameObject nearestThreat = unitAI.ReadFromBlackboard ("NEAREST") as GameObject;
			if (highestThreat)
				FSM.SetBool ("inrange", (Vector2.SqrMagnitude (highestThreat.transform.position - this.transform.position) <= range * range));
			else if (nearestThreat) 
				FSM.SetBool ("inrange", true);
			else 
				FSM.SetBool ("inrange", false);
		}
	}

	// attack target
	public void AttackTarget(GameObject target){
		if (cooldownRemaining <= 0.0f) {
			Debug.Log (this.gameObject.name + " is attacking "+target.name);
			cooldownRemaining = cooldown;
		}
	}
}
