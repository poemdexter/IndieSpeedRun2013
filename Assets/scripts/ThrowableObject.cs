using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
 * Allows an object to be thrown by the player and takes care of changing
 * constraints and gravity to make the throw use regular physics.
 */

public class ThrowableObject : MonoBehaviour {
	
	public AudioClip scream1, scream2, scream3, scream4, scream5, scream6, scream7, scream8, scream9;
	public Vector2 throwVector = new Vector2(-600, 600);
	public float throwRotation = 400;
	public bool thrown = false;
	
	private List<AudioClip> screams = new List<AudioClip>();
	private tk2dSprite pointer;
	private Vector3 pointerVector = new Vector3(0,0.1f,0);
	
	void Start()
	{
		// set pointer's renderlayer to the same as its parent
		pointer = transform.FindChild("Pointer").GetComponent<tk2dSprite>();
		pointer.RenderLayer = GetComponent<tk2dSprite>().RenderLayer;
		
		screams.Add(scream1);
		screams.Add(scream2);
		screams.Add(scream3);
		screams.Add(scream4);
		screams.Add(scream5);
		screams.Add(scream6);
		screams.Add(scream7);
		screams.Add(scream8);
		screams.Add(scream9);
	}
	
	void Update()
	{
		// animate pointer
		if(pointer)
		{
			if(pointer.transform.localPosition.y > 8)
			{
				pointerVector = new Vector3(0,-0.1f,0);
			}
			if(pointer.transform.localPosition.y < 6){
				pointerVector = new Vector3(0,0.1f,0);
			}
			pointer.transform.Translate(pointerVector);
		}
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
			DestroyObject(pointer);
			
			// change sprite
			tk2dSpriteAnimator anim = GetComponent<tk2dSpriteAnimator>();
			if (anim != null) anim.enabled = false; // turn of animation if we're a moving peasant
			GetComponent<tk2dSprite>().SetSprite("pesant throw");
			
			// play peasant scream sound
			AudioSource.PlayClipAtPoint(screams[Random.Range( 0, screams.Count )], transform.position);
		}
	}
	
	public bool hasBeenThrown()
	{
		return thrown;
	}

}
