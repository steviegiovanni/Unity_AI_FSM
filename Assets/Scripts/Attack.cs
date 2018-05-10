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
			} else{
				Dictionary<GameObject,Vector3> percepts = unitAI.ReadFromBlackboard ("PERCEPTS") as Dictionary<GameObject, Vector3>;
				if (percepts != null) {
					GameObject nearest = null;
					float maxDistance = 999.0f;
					foreach (GameObject key in percepts.Keys) {
						if (key) {
							float sqrDistance = Vector3.SqrMagnitude (key.transform.position - this.transform.position);
							if ((sqrDistance <= maxDistance) && (sqrDistance <= range*range)) {
								maxDistance = sqrDistance;
								nearest =  key;
							}
						}
					}

					unitAI.WriteToBlackboard ("NEARESTTHREAT", nearest);
					if (FSM) {
						if (nearest) 
							FSM.SetBool ("inrange", true);
						else 
							FSM.SetBool ("inrange", false);
					}
				}
			}
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
