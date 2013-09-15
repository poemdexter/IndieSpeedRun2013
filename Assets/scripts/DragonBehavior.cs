using UnityEngine;
using System.Collections;

public class DragonBehavior : MonoBehaviour {
	public float currentSpeed = 23.0f;	//current speed
	public float normalSpeed = 23.0f;	//normal speed
	public float minSpeed = 8.0f;		//absolute minimum speed
	public float speedDecrement = 15.0f; //rate at which speed is decremented.
	public float speedIncrement = 0.2f; //rate at which the speed increases after decrement
	
	public bool isHit = false;
	public bool eatKing = false;
	
	public Vector2 moveDirection = Vector2.zero;
	
	void Update ()
	{
		calculateMoveRate();
	}
	
	void calculateMoveRate() 
	{
		moveDirection = new Vector2(currentSpeed, 0);
		
		//Check if speed == normalSpeed, if not then slowly increase speed until speed = normal speed
		if (isHit)
		{
			currentSpeed = (currentSpeed - speedDecrement < minSpeed) ? minSpeed : currentSpeed - speedDecrement;
			isHit = false;
		}
		
		if(!isHit && currentSpeed < normalSpeed)
		{
			currentSpeed = currentSpeed + speedIncrement;
			// Debug.Log("speed up sucka");
		}
		
		//move dragon
		transform.Translate(moveDirection * Time.deltaTime);
	}
	
	
	void reduceSpeed() 
	{
		//called when collision happens.
		if( currentSpeed > minSpeed)
			currentSpeed = currentSpeed - speedDecrement;
		else
			currentSpeed = minSpeed;
	}
	
	void OnTriggerEnter(Collider collider)
	{
		// we hit an obstacle here and need to slow down
		
		if(collider.gameObject.CompareTag("Throwable") && collider.gameObject.GetComponent<ThrowableObject>().hasBeenThrown())
		{
//			Debug.Log("obj hit dragon");
			isHit = true;
			Destroy(collider.gameObject);
		}
		
		if(collider.gameObject.CompareTag("Player")) 
		{
			eatKing = true;
		}
	}
	
	public bool canEatKing() 
	{
		return eatKing;
	}
}
