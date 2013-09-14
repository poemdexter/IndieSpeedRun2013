using UnityEngine;
using System.Collections;

public class DragonBehavior : MonoBehaviour {
	public float currentSpeed = 13.0f;	//current speed
	public float normalSpeed = 13.0f;	//normal speed
	public float minSpeed = 7.0f;		//absolute minimum speed
	public float speedDecrement = 0.5f; //rate at which speed is decremented.
	public float speedIncrement = 0.5f; //rate at which the speed increases after decrement
	
	public bool isHit = false;
	
	public Vector2 moveDirection = Vector2.zero;
	
	void FixedUpdate (){
		
		moveDirection = new Vector2(currentSpeed, 0);
		
		//Check if speed == normalSpeed, if not then slowly increase speed until speed = normal speed
		if (currentSpeed < normalSpeed)
			currentSpeed = currentSpeed + speedIncrement;
		else
			currentSpeed = normalSpeed;
		
		
		transform.Translate(moveDirection * Time.deltaTime);
	}
	
	void reduceSpeed() {
		//called when collision happens.
		if( currentSpeed > minSpeed)
			currentSpeed = currentSpeed - speedDecrement;
		else
			currentSpeed = minSpeed;
	}
	
}
