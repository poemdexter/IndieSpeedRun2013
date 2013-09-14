using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {
	
	public float speed = 10.0f;
	public float jumpPower = 25.0f;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		ConstantForce constantForce = new ConstantForce();
		constantForce.force = new Vector3(speed,0,0);
		
		rigidbody.constantForce = constantForce;
//		
//		Vector3 move = Vector3.zero;
//		
//		if (Input.GetKeyDown(KeyCode.Space))
//		{
//			move += new Vector3(0, jumpPower, 0);
//		}
//
//		move += new Vector3(speed, 0, 0);
//		
//		transform.Translate(move * Time.deltaTime);
	}
}
