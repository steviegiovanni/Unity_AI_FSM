using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIComponent : MonoBehaviour {
	GameObject owner;
	public Animator FSM;

	// Use this for initialization
	public void Start () {
		owner = this.gameObject;
		FSM = owner.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
