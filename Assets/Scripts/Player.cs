using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed = 10f;
	public Vector2 maxVelocity = new Vector2(3, 10);
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
		
		if (controller.moving.x != 0) {
			// We're moving horizontally.
			forceX = speed * controller.moving.x;
			
			// Face in the direction we're moving horizontally.
			transform.localScale = new Vector3(controller.moving.x > 0 ? 1 : -1, 1, 1);
		}

		if (controller.moving.y > 0) {
			// We're jumping.
			if (grounded && rigidbody2D.velocity.y == 0) {
				forceY = jumpSpeed * controller.moving.y;
				grounded = false;
			}
		}
		
		rigidbody2D.AddForce(new Vector2(forceX, forceY));
		ClampVelocity();

		if (grounded) {
			// We're on the ground.
			if (absVelocityX > 0) {
				// We're moving horizontally.
				animator.SetInteger("AnimState", 1);
			} else {
				// We're standing still.
				animator.SetInteger("AnimState", 0);
			}
		} else {
			// We're in the air.
			animator.SetInteger("AnimState", 2);
		}
	}
	
	void OnCollisionEnter2D (Collision2D target) {
		if (target.gameObject.tag == "Ground") {
			grounded = true;
		}
	}

	private void ClampVelocity () {
		if (Mathf.Abs(rigidbody2D.velocity.x) > maxVelocity.x || Mathf.Abs(rigidbody2D.velocity.y) > maxVelocity.y) {
			rigidbody2D.velocity = new Vector2(
				Mathf.Clamp(rigidbody2D.velocity.x, -maxVelocity.x, maxVelocity.x),
				Mathf.Clamp(rigidbody2D.velocity.y, -maxVelocity.y, maxVelocity.y)
			);
		}
	}
}
