using UnityEngine;
using System.Collections.Generic;

// Abstract base for NPC sensors. Concrete sensors (like sight) override
// StartSensor/UpdateSensor to react to what they detect.
public class NPCSensor_Base : MonoBehaviour {
	public NPC_Enemy npcBase;
	protected List<GameObject> sensedObjects = new List<GameObject>();

	void Start () {
		if (npcBase == null)
			npcBase = gameObject.GetComponent<NPC_Enemy> ();
		StartSensor ();
	}

	void Update () {
		UpdateSensor ();
	}

	protected virtual void StartSensor(){}
	protected virtual void UpdateSensor(){}

	protected List<GameObject> GetSensedObjects(){
		return sensedObjects;
	}
}