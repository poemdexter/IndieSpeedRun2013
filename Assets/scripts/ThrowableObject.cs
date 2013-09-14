using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour {
	
	public Vector2 throwVector = Vector2.zero;
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
			thrown = true;
			// throw yourself
			rigidbody.AddForce(throwVector);
			rigidbody.AddTorque(new Vector3(0,0,throwRotation));
			rigidbody.useGravity = true;
		}
	}
	
	public bool hasBeenThrown()
	{
		return thrown;
	}
}
