using UnityEngine;
using System.Collections;

public class WinPoint : MonoBehaviour {
	void OnTriggerEnter2D (Collider2D otherCollider) {
		if (otherCollider.gameObject.tag == "Player") {
			Debug.Log("U WIN");
			GetComponentInChildren<ParticleSystem>().Stop();
		}
	}
}
