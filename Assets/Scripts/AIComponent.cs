using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComponent : MonoBehaviour {
	GameObject owner;
	public AI unitAI;
	public Animator FSM;

	// Use this for initialization
	virtual public void Start () {
		owner = this.gameObject;

		unitAI = owner.GetComponent<AI> ();
		if(!unitAI)
			Debug.LogWarning ("No AI attached to game object: " + this.gameObject.name);

		FSM = owner.GetComponent<Animator> ();
		if (!FSM)
			Debug.LogWarning ("No FSM attached to game object: " + this.gameObject.name);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
