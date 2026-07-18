using UnityEngine;

public class NPCSensor_Sight : NPCSensor_Base {
	const float SIGHT_DIRECT_ANGLE = 120.0f;
	const float SIGHT_MAX_DISTANCE = 20.0f;
	float height = 2.0f;
	public LayerMask hitTestMask;
	public Color idleColor, alertedColor, attackColor;
	public Material material;

	protected override void StartSensor(){
		material.SetColor ("_Color", Color.green);
	}
	protected override void UpdateSensor(){
		GetTargetInSight ();
	}

	void GetTargetInSight(){
		Collider[] overlapedObjects = Physics.OverlapSphere (transform.position, SIGHT_MAX_DISTANCE);

		for (int i=0; i<overlapedObjects.Length; i++) {
			if (overlapedObjects [i].tag != "Player") {
				material.SetColor ("_Color", idleColor);
				continue;
			}

			Vector3 direction = overlapedObjects [i].transform.position - transform.position;
			float objAngle = Vector3.Angle (direction, transform.forward);

			if (objAngle < SIGHT_DIRECT_ANGLE && TargetInSight (overlapedObjects [i].transform, SIGHT_MAX_DISTANCE)) {
				npcBase.SetTargetPos (overlapedObjects [i].transform.position);
				material.SetColor ("_Color", attackColor);
			} else {
				material.SetColor ("_Color", idleColor);
			}
		}
	}

	bool TargetInSight(Transform target, float distance){
		Vector3 sightPosition = transform.position;
		sightPosition.y += height;
		RaycastHit hit = new RaycastHit ();
		Vector3 dir = target.position - sightPosition;
		Physics.Raycast (sightPosition, dir, out hit, distance, hitTestMask);
		return hit.collider != null && target.gameObject == hit.collider.gameObject;
	}
}