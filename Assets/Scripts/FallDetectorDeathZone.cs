using UnityEngine;
using System.Collections;

public class FallDetectorDeathZone : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D otherCollider) {
		if (otherCollider.gameObject.tag == "Player") {
			otherCollider.gameObject.GetComponent<Player>().Respawn();
		}
	}

	void OnDrawGizmos () {
		Gizmos.color = new Color(0f, 1f, 0f, 0.2f);
		Gizmos.DrawCube(collider2D.bounds.center, collider2D.bounds.size);

		Gizmos.color = new Color(1f, 0f, 1f, 0.2f);
		Gizmos.DrawWireCube(collider2D.bounds.center, collider2D.bounds.size);

		foreach (SphereCollider c in GetComponentsInChildren<SphereCollider>()) {
			Gizmos.color = new Color(1f, 0f, 1f, 0.2f);
			Gizmos.DrawWireSphere(c.bounds.center, c.radius * transform.localScale.x);

			Vector3 center = c.transform.position;
			Gizmos.color = new Color(0f, 0f, 1f, 0.7f);
			Gizmos.DrawLine(
				new Vector3(center.x - 0.2f, center.y, center.z),
				new Vector3(center.x + 0.2f, center.y, center.z)
			);
			Gizmos.DrawLine(
				new Vector3(center.x, center.y - 0.2f, center.z),
				new Vector3(center.x, center.y + 0.2f, center.z)
			);
		}
	}
}
