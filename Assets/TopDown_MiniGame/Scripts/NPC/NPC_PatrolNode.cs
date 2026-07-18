using UnityEngine;

public class NPC_PatrolNode : MonoBehaviour {
	public NPC_PatrolNode nextNode;

	public Vector3 GetPosition(){
		return transform.position;
	}
	public Vector3 GetNextNodePosition(){
		return nextNode.GetPosition ();
	}

	void OnDrawGizmos(){
		Gizmos.color = Color.yellow;
		Gizmos.DrawSphere(transform.position, 0.25f);
		if (nextNode != null) {
			Gizmos.color = Color.cyan;
			Gizmos.DrawLine (GetPosition (), GetNextNodePosition ());
		}
	}
}