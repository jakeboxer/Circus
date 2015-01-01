using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 5);
	public float airSpeedMultiplier = 0.3f;
	public bool grounded = true;
	public float jumpSpeed = 400f;
	
	private Animator animator;
	private PlayerController controller;
	
	void Start () {
		animator = GetComponent<Animator>();
		controller = GetComponent<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
		var forceX = 0f;
		var forceY = 0f;
		
		var velocityX = rigidbody2D.velocity.x;
		var absVelocityX = Mathf.Abs(velocityX);
		var absVelocityY = Mathf.Abs(rigidbody2D.velocity.y);
		
		if (controller.moving.x == 0) {
			// We're not moving horizontally.
			animator.SetInteger("AnimState", 0);
		} else {
			// We're moving horizontally.
			if (absVelocityX < maxVelocity.x) {
//				if ((controller.moving.x > 0f && velocityX < 0f) || (controller.moving.x < 0f && velocityX > 0f)) {

				// We haven't yet reached the horizontal speed limit.
				forceX = speed * controller.moving.x;
				
				// Face in the direction we're moving horizontally.
				transform.localScale = new Vector3(controller.moving.x > 0 ? 1 : -1, 1, 1);
			}
			
			animator.SetInteger("AnimState", 1);
		}

		if (controller.moving.y > 0) { 
			// We're jumping.
			if (grounded && rigidbody2D.velocity.y == 0) {
				forceY = jumpSpeed * controller.moving.y;
				grounded = false;
			}
			
			animator.SetInteger("AnimState", 2);
		} else if (absVelocityY > 0) {
			// We're not jumping but we are in the air (probably falling).
			animator.SetInteger("AnimState", 3);
		}
		
		rigidbody2D.AddForce(new Vector2(forceX, forceY));
	}
	
	void OnCollisionEnter2D (Collision2D target) {
		if (target.gameObject.tag == "Ground") {
			grounded = true;
		}
//		if (!standing) {
//			var absVelocityX = Mathf.Abs(rigidbody2D.velocity.x);
//			var absVelocityY = Mathf.Abs(rigidbody2D.velocity.y);
//			
//			if (absVelocityX <= 0.1f || absVelocityY <= 0.1f) {
////				if (thudSound) {
////					AudioSource.PlayClipAtPoint(thudSound, transform.position);
////				}
//			}
//		}
	}
	
	void PlayLeftFootSound () {
//		if (leftFootSound) {
//			AudioSource.PlayClipAtPoint(leftFootSound, transform.position);
//		}
	}
	
	void PlayRightFootSound () {
//		if (rightFootSound) {
//			AudioSource.PlayClipAtPoint(rightFootSound, transform.position);
//		}
	}
}
