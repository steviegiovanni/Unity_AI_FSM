using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public float range = 1.0f;
	public float cooldown = 5.0f;
	private float cooldownRemaining = 0.0f;

	// check target is in range
	public bool IsTargetInrange(GameObject target){
		bool inRange = false;
		if (target) {
			if (Vector2.SqrMagnitude (target.transform.position - this.transform.position) <= range * range)
				inRange = true;
		}
		return inRange;
	}

	void Update(){
		cooldownRemaining -= Time.deltaTime;
	}

	// attack target
	public void AttackTarget(){
		if (cooldownRemaining <= 0.0f) {
			Debug.Log ("Attacking taget");
			cooldownRemaining = cooldown;
		}
	}
}
