using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public GameObject heartMeter;
	
	public float currentSpeed = 20.0f;
	public float runSpeed = 20.0f;
	public float jumpSpeed = 28.0f;
	public float stumbleSpeed = 15.0f;
	public float recoverSpeed = 0.2f;
	public float dragonBoostSpeed = 50.0f;
	public float dragonRecoverySpeed = 0.8f;
	public float gravity = -1f;
	public float currentGravity = 0;
	public float rayOffset = -0.1f;
	
	public bool isActivated = false;	// player doesn't start running until this is true
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	public bool isHit = false;
	public bool isStumbling = false;
	public bool isDragonBoosted = false;
	public bool isThrowing = false;
	public bool isShoving = false;
	
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 jumpDirection = Vector2.zero;
	
	public void Activate()
	{
		isActivated = true;
	}
	
	void Update()
	{
		if(isActivated)
		{
			if(isGrounded && Input.GetKeyDown(KeyCode.X))	// is throwing if key pressed while on ground
			{
				isThrowing = true;
			}
			else
			{
				isThrowing = false;
			}
			
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
		
		if (isDragonBoosted)
		{	
			if (currentSpeed > runSpeed) currentSpeed -= dragonRecoverySpeed;
			else
			{
				currentSpeed = runSpeed;
				isDragonBoosted = false;
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
		if (Physics.Raycast(ray, out hit, collider.bounds.extents.y + rayOffset + .1f))
		{
			if (hit.collider.gameObject.CompareTag("Ground"))
			{
				transform.position = new Vector3(transform.position.x, hit.point.y + collider.bounds.extents.y + rayOffset, 0);
				return true;
			}
		}
		return false;
	}
	
	void OnTriggerStay(Collider collider)
	{
		// we are colliding with a throwable object and happen to be throwing, tell the object to get thrown
		if(collider.CompareTag("Throwable") && isThrowing)
		{
			collider.gameObject.GetComponent<ThrowableObject>().Throw();
			isShoving = true;
		}
	}
	
	void OnTriggerEnter(Collider collider)
	{
		// we hit an obstacle here and need to slow down
		if(collider.gameObject.CompareTag("Obstacle"))
		{
			currentSpeed -= stumbleSpeed; // we need to slow our speed
			isStumbling = true; // and flag as stumbling so we can recover
		}
		
		if(collider.gameObject.CompareTag("Finish"))
		{
			Debug.Log("winner");
		}
		
		// trigger the section so that objects within can start moving
		if (collider.gameObject.CompareTag("SectionTrigger"))
		{
			collider.gameObject.BroadcastMessage("TriggerMe");
		}
		
		if (collider.gameObject.CompareTag("Dragon"))
		{			
			// play king pain sound
			
			// deplete heart meter
			heartMeter.GetComponent<HeartMeterScript>().DepleteHearts();
			
			if (isStumbling)
			{
				isStumbling = false;
				currentSpeed = runSpeed;
			}
			
			// TODO: Move this to triggered event
			currentSpeed += dragonBoostSpeed;	// speed up a bit to get away from the dragon
			isDragonBoosted = true;	// flag as boosted by dragon so we can slow down
		}
	}
	
	// return values match animation clip names
	public string GetCurrentState()
	{
		if (isStumbling) return "Stumble";
		if (isShoving) return "Shove";
		else if (inAir || isJumping) return "Jump";
		else if (isGrounded) return "Run";
		else if (isDragonBoosted) return "Boosted";
		else return "Stand";
	}
}
