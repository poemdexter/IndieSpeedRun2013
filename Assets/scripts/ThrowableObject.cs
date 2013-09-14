using UnityEngine;
using System.Collections;

public class ThrowableObject : MonoBehaviour {
	
	public float throwSpeed = 400;
	
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
			rigidbody.AddForce(new Vector2(0,throwSpeed));
		}
	}
	
	public bool hasBeenThrown()
	{
		return thrown;
	}
}
