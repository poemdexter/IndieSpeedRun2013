using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float currentSpeed = 10.0f;
	public float runSpeed = 10.0f;
	public float jumpSpeed = 5.0f;
	public float stumbleSpeed = 5.0f;
	public float recoverSpeed = 0.1f;
	public float gravity = -0.1f;
	public float currentGravity = 0;
	
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	public bool isHit = false;
	public bool isStumbling = false;
	public bool isThrowing = false;
	
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 jumpDirection = Vector2.zero;
	
	void FixedUpdate()
	{
		
		if (IsGrounded())
		{
			isGrounded = true;
			isJumping = false;
			inAir = false;
			currentGravity = 0;
			jumpDirection = Vector2.zero;
		}
		else if (!inAir) // first frame of jump
		{
			inAir = true;
			isGrounded = false;
			isThrowing = false;		// no throwing while jumping
			currentGravity = gravity;
		}
		
		CalculateMoveDirection();
	}
	
	void Update()
	{
		if(isGrounded && Input.GetKeyDown(KeyCode.X))	// is throwing if key pressed while on ground
		{
			isThrowing = true;
		}
		else
		{
			isThrowing = false;
		}
	}
	
	void CalculateMoveDirection()
	{
		moveDirection = new Vector2(currentSpeed, 0); // gotta go fast (to the right)
		
		if (isStumbling)
		{
			if (currentSpeed < runSpeed) currentSpeed += recoverSpeed;
			else
			{
				currentSpeed = runSpeed; // stop us from overshooting max speed
				isStumbling = false;
			}
		}
		
		// we're on the ground and hit spacebar
		if (isGrounded && Input.GetKeyDown(KeyCode.Z))
		{
			isJumping = true;
			isGrounded = false;
			jumpDirection += new Vector2(0, jumpSpeed);
		}
		
		// we're moving vertically, start applying gravity
		if (isJumping || inAir)
		{
			jumpDirection += new Vector2(0, currentGravity);
			moveDirection += jumpDirection;
		}
		
		// move us for real
		transform.Translate(moveDirection * Time.deltaTime);
	}
	
	bool IsGrounded()
	{
		Ray ray = new Ray(transform.position, Vector3.down);
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, collider.bounds.extents.y + .1f))
		{
			if (hit.collider.gameObject.CompareTag("Ground"))
			{
				transform.position = new Vector3(transform.position.x, hit.point.y + collider.bounds.extents.y, 0);
				return true;
			}
		}
		return false;
	}
	
	void OnCollisionEnter(Collision collision)
	{
		// we hit an obstacle here
		if(collision.collider.CompareTag("Obstacle"))
		{
			currentSpeed -= stumbleSpeed; // we need to slow our speed
			isStumbling = true; // and flag as stumbling so we can recover
		}
		
	}
	
	void OnCollisionStay(Collision collision)
	{
		// we are colliding with a throwable object and happen to be throwing, tell the object to get thrown
		if(collision.collider.CompareTag("Throwable") && isThrowing)
		{
			collision.collider.gameObject.GetComponent<ThrowableObject>().Throw();
		}
	}
	
	// return values match animation clip names
	public string GetCurrentState()
	{
		if (isStumbling) return "Stumble";
		else if (inAir || isJumping) return "Jump";
		else if (isGrounded) return "Run";
		else return "Stand";
	}
}
