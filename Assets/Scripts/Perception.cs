using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Perception : AIComponent {
	public float radius;
	public Dictionary<GameObject,Vector3> percepts;

	override public void Start(){
		base.Start ();
		percepts = new Dictionary<GameObject,Vector3> ();
	}

	void Update(){
		if (unitAI)
			unitAI.WriteToBlackboard ("PERCEPTS", percepts);
		if (FSM)
			FSM.SetBool ("perceiving", (percepts.Count != 0));
	}

	void OnEnable(){
		EventManager.StartListening ("PERCEPTION", Perceived);
		EventManager.StartListening ("DESTROY", PerceptDestroyed);
	}

	void OnDisable(){
		EventManager.StopListening ("PERCEPTION", Perceived);
		EventManager.StopListening ("DESTROY", PerceptDestroyed);
	}

	// percept is detected
	void Perceived(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject perceivedObject = (GameObject)(param ["OBJECT"]);
		if (perceivedObject == this.gameObject)
			return;

		Vector3 position = perceivedObject.transform.position;
		Vector3 prevPos;
		if (percepts.TryGetValue (perceivedObject, out prevPos)) {
			if (Vector3.SqrMagnitude (position - this.transform.position) <= radius * radius)
				percepts [perceivedObject] = position;
			else 
				percepts.Remove (perceivedObject);
		}else {
			if (Vector3.SqrMagnitude (position - this.transform.position) <= radius * radius)
				percepts.Add (perceivedObject, position);
		}
	}

	// remove percept from percepts if the object is destroyed
	void PerceptDestroyed(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject destroyedPercept = (GameObject)(param ["OBJECT"]);
		Vector3 pos;
		if(percepts.TryGetValue(destroyedPercept, out pos))
			percepts.Remove (destroyedPercept);
	}

	// return the nearest percept
	public GameObject GetNearestPercept(){
		GameObject nearest = null;
		float maxDistance = 999.0f;
		foreach (GameObject key in percepts.Keys) {
			if (key) {
				float sqrDistance = Vector3.SqrMagnitude (key.transform.position - this.transform.position);
				if (sqrDistance <= maxDistance) {
					maxDistance = sqrDistance;
					nearest =  key;
				}
			}
		}

		return nearest;
	}
}
