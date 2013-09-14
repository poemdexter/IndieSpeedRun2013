﻿using UnityEngine;
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
	
	public Vector2 moveDirection = Vector2.zero;
	public Vector2 jumpDirection = Vector2.zero;
	
	void Start()
	{
		//Necessary to keep object "flat" and keep gravity working properly, otherwise object's "down" changes
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
	}
	
	void Update()
	{
	
	}
	
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
			currentGravity = gravity;
		}
		
		if(!rigidbody.useGravity)	//Necessary to make throwable objects look nicer
		{
			CalculateMoveDirection();
		}
	}
	
	// this is an object that is moving (possibly jumping?) like the player does
	
	void CalculateMoveDirection()
	{
		moveDirection = new Vector2(currentSpeed, 0); // gotta go fast (to the right)
		
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
}
