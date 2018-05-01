using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AI : MonoBehaviour {
	Animator anim;
	public GameObject player;

	public GameObject [] waypoints;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		anim.GetBehaviour<BChasing> ().opponent = player;
	}
	
	// Update is called once per frame
	void Update () {
		anim.SetFloat ("distance", Vector3.Distance(transform.position, player.transform.position));
	}
}
