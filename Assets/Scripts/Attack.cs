using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
	public GameObject target = null;
	public float range = 1.0f;

	// check target is in range
	public bool IsTargetInrange(){
		bool inRange = false;
		if (target) {
			Debug.Log (Vector2.SqrMagnitude (target.transform.position - this.transform.position));
			if (Vector2.SqrMagnitude (target.transform.position - this.transform.position) <= range * range)
				inRange = true;
		}

		return inRange;
	}
}
