using UnityEngine;
using System.Collections;

/*
 * Allows an object to be thrown by the player and takes care of changing
 * constraints and gravity to make the throw use regular physics.
 */

public class ThrowableObject : MonoBehaviour {
	
	public Vector2 throwVector = new Vector2(-300, 400);
	public float throwRotation = 200;
	private bool thrown = false;
	
	void Start()
	{
		
	}
	
	void Update()
	{
		
	}
	
	// called by Player when this object is thrown
	public void Throw()
	{
		if(!thrown)
		{
			// throw yourself
			thrown = true;
			rigidbody.isKinematic = false;
			rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;	// necessary to make object rotate when thrown
			rigidbody.AddForce(throwVector);
			rigidbody.AddTorque(new Vector3(0,0,throwRotation));
			rigidbody.useGravity = true;									// necessary to make object throw more nicely
		}
	}
	
	public bool hasBeenThrown()
	{
		return thrown;
	}

}
