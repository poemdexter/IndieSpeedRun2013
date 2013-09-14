using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour {
	
	public Vector2 throwVector = Vector2.zero;
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
		}
	}
	
	public bool hasBeenThrown()
	{
		return thrown;
	}
}
