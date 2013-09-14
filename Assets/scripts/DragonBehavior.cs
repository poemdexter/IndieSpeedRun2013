using UnityEngine;
using System.Collections;

public class DragonBehavior : MonoBehaviour {
	public float speed = 13.0f;
	public float normalSpeed = 13.0f;
	public float minSpeed = 7.0f;
	public float speedDecrement = 0.5f;
	
	public bool isHit = false;
	
	public Vector2 moveDirection = Vector2.zero;
	
	void FixedUpdate (){
		
		moveDirection = new Vector2(speed, 0);
		
		//Check if speed == normalSpeed, if not then slowly increase speed until speed = normal speed
		
		
		
		transform.Translate(moveDirection * Time.deltaTime);
	}
	
	void reduceSpeed() {
		//called when collision happens. 
		speed = speed - speedDecrement;
	}
	
}
