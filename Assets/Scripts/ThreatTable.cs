using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreatTable : AIComponent {
	public Dictionary<GameObject, int> threats;

	override public void Start(){
		base.Start ();
		threats = new Dictionary<GameObject,int> ();
	}

	void Update(){
		if (FSM) {
			if (unitAI) {
				Dictionary<GameObject,Vector3> percepts = unitAI.ReadFromBlackboard ("PERCEPTS") as Dictionary<GameObject, Vector3>;
				if (percepts != null) {
					ProcessPercepts (percepts);
					GameObject highestThreat = GetHighestThreat ();
					unitAI.WriteToBlackboard ("HIGHESTTHREAT", highestThreat);

					if (highestThreat)
						FSM.SetBool ("aggroed", true);
					else
						FSM.SetBool ("aggroed", false);
				}
			}
		}
	}

	void OnEnable(){
		EventManager.StartListening ("DESTROY", ThreatDestroyed);
	}

	void OnDisable(){
		EventManager.StopListening ("DESTROY", ThreatDestroyed);
	}

	// receives percepts and update threat table
	public void ProcessPercepts(Dictionary<GameObject,Vector3> percepts){
		// for each perceived unity, add 0 threat to thread table
		foreach(GameObject key in percepts.Keys){
			int threat;
			if (!threats.TryGetValue (key, out threat)) 
				threats.Add (key, 0);
		}

		// for each unit in threat table, if it's not perceived anymore, remove it
		List<GameObject> keys = new List<GameObject>(threats.Keys);
		for (int i = 0; i < keys.Count; i++) {
			Vector3 pos;
			if (!percepts.TryGetValue (keys [i], out pos)) {
				Debug.Log ("reached reached reached reached");
				threats.Remove (keys [i]);
			}
		}
	}

	// remove threat from threats if the object is destroyed
	void ThreatDestroyed(Hashtable param){
		if (!param.ContainsKey ("OBJECT"))
			return;

		GameObject destroyedThreat = (GameObject)(param ["OBJECT"]);
		int threat;
		if(threats.TryGetValue(destroyedThreat, out threat))
			threats.Remove (destroyedThreat);
	}

	// get object with highest threat
	public GameObject GetHighestThreat(){
		GameObject highestThreatObject = null;
		float maxDistance = 999.0f;
		int highestThreat = 0;

		foreach (GameObject key in threats.Keys) {
			if (key) {
				float sqrDistance = Vector3.SqrMagnitude (key.transform.position - this.transform.position);
				if ((highestThreat <= threats [key]) && (sqrDistance <= maxDistance)) {
					maxDistance = sqrDistance;
					highestThreat = threats [key];
					highestThreatObject = key;
				}
			}
		}

		return highestThreatObject;
	}
}
