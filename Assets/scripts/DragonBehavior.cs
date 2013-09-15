using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DragonBehavior : MonoBehaviour {
	public AudioClip fireSound1, fireSound2, footStep1, footStep2, footStep3;
	public float currentSpeed = 23.0f;	//current speed
	public float normalSpeed = 23.0f;	//normal speed
	public float minSpeed = 8.0f;		//absolute minimum speed
	public float speedDecrement = 15.0f; //rate at which speed is decremented.
	public float speedIncrement = 0.2f; //rate at which the speed increases after decrement
	
	public bool isActivated = false;	//dragon doesn't start running until this is true
	public bool isHit = false;
	
	public Vector2 moveDirection = Vector2.zero;
	
	private List<AudioClip> fireSounds = new List<AudioClip>();
	private List<AudioClip> footSteps = new List<AudioClip>();
	
	private GameObject fireHitObject;
	public ParticleSystem charcoalParticleEffect;
	
	private tk2dSpriteAnimator anim;
	
	void Start()
	{
		anim = GetComponent<tk2dSpriteAnimator>();
		
		fireSounds.Add(fireSound1);
		fireSounds.Add(fireSound2);
		
		footSteps.Add (footStep1);
		footSteps.Add (footStep2);
		footSteps.Add (footStep3);
	}
	
	public void Activate()
	{
		isActivated = true;
		anim.Play();
		anim.AnimationEventTriggered = FlameOnDelegate;
		anim.AnimationCompleted = FireCompleteDelegate;
	}
	
	public void Deactivate()
	{
		isActivated = false;
		anim.Stop();
	}
	
	void Update ()
	{
		if(isActivated)
		{
			calculateMoveRate();
		}
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
	
	// done breathing fire, just walk again
	void FireCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clip)
	{
		if(clip.name.Equals("Fire"))
		{
			anim.Play("Walk");
		}
	}
	
	void FlameOnDelegate(tk2dSpriteAnimator animator, tk2dSpriteAnimationClip clip, int frameNumber)
	{
		if(clip.GetFrame(frameNumber).eventInfo.Equals("FlameOn"))
		{
			string fireTag = fireHitObject.tag;
			
			if (fireTag.Equals("Throwable"))
			{
				Vector3 position = fireHitObject.transform.position;
				position.z = charcoalParticleEffect.transform.position.z;
				
				// KILL THE PEASANTS
				Destroy(fireHitObject);
				
				// BURNINATE THE PEASANTS (particle effects)
				ParticleSystem localCharcoal = GameObject.Instantiate(charcoalParticleEffect, position, charcoalParticleEffect.transform.rotation) as ParticleSystem;
				localCharcoal.Play();
			}
			else if (fireTag.Equals("Player"))
			{
				// TODO: tell player to get bumped
			}
		}
		
		if(clip.GetFrame(frameNumber).eventInfo.Equals("DragonFootStep"))
		{
			AudioSource.PlayClipAtPoint(footSteps[Random.Range( 0, footSteps.Count )], transform.position);
		}
	}
	
	
	void OnTriggerEnter(Collider collider)
	{
		// we hit an obstacle here and need to slow down
		
		if(collider.gameObject.CompareTag("Throwable") && collider.gameObject.GetComponent<ThrowableObject>().hasBeenThrown())
		{
			isHit = true;
			anim.Play("Fire");
			fireHitObject = collider.gameObject;
			
			// play fire breath sound
			AudioSource.PlayClipAtPoint(fireSounds[Random.Range( 0, fireSounds.Count )], transform.position);
		}
		else if(collider.gameObject.CompareTag("Player")) 
		{
			anim.Play("Fire");
			fireHitObject = collider.gameObject;
			
			// play fire breath sound
			AudioSource.PlayClipAtPoint(fireSounds[Random.Range( 0, fireSounds.Count )], transform.position);
		}
	}
}
