using UnityEngine;
using System.Collections;

/*
 * Allows an object to move similarly to the way the player does.
 * Makes objects look like they're "fleeing" as well.
 * Takes care of necessary constraints/gravity so the object moves normally.
 */

public class MovingObject : MonoBehaviour {
	
	public float currentSpeed = 4.0f;
	public float runSpeed = 4.0f;
	public float jumpSpeed = 5.0f;
	public float gravity = -0.1f;
	public float currentGravity = 0;
	
	public bool isGrounded = false;
	public bool isJumping = false;
	public bool inAir = false;
	
	public bool triggered = false;
	
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 jumpDirection = Vector2.zero;
	
	void Start()
	{
		//Necessary to keep object "flat" and keep gravity working properly, otherwise object's "down" changes
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
	}

	void Update()
	{
		if(triggered && !rigidbody.useGravity)	//Necessary to make throwable objects look nicer
		{
			CalculateMoveDirection();
		}
	}
	
	// this is an object that is moving (possibly jumping?) like the player does
	
	void CalculateMoveDirection()
	{
		moveDirection = new Vector2(currentSpeed, 0); // gotta go fast (to the right)
		
		// move us for real
		transform.Translate(moveDirection * Time.deltaTime);
	}
	
	void TriggerMe()
	{
		triggered = true;
	}
}
