using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour {
	private Dictionary <string, UnityEvent<Hashtable>> eventDictionary;
	private static EventManager eventManager;

	// getter, reminds to have one active eventamanager object in the scene
	public static EventManager instance{
		get{
			if (!eventManager) {
				eventManager = FindObjectOfType<EventManager> ();
				if (!eventManager) 
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				 else 
					eventManager.Init ();
			}

			return eventManager;
		}
	}

	// initialize dictionary of event
	void Init(){
		if (eventDictionary == null) 
			eventDictionary = new Dictionary<string,UnityEvent<Hashtable>> ();
	}

	// register a listener to the event manager
	public static void StartListening(string eventName, UnityAction<Hashtable> listener){
		UnityEvent<Hashtable> thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
			thisEvent.AddListener (listener);
		else {
			thisEvent = new UnityEvent<Hashtable> ();
			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	// unregister a listener from the event manager
	public static void StopListening (string eventName, UnityAction<Hashtable> listener){
		if (eventManager == null)
			return;
		UnityEvent thisEvent = null;
		if(instance.eventDictionary.TryGetValue(eventName,out thisEvent))
			thisEvent.RemoveListener(listener);
	}

	// call to trigger the event
	public static void TriggerEvent(string eventName){
		UnityEvent<Hashtable> thisEvent = null;
		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent))
			thisEvent.Invoke ();
	}
}
