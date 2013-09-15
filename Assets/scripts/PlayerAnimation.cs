using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{	
	tk2dSpriteAnimator anim;
	
	bool stumble = false;
	
	void Start ()
	{
		anim = GetComponent<tk2dSpriteAnimator>();
	}
	
	void Update ()
	{
		if(!stumble)
		{
			string clipName = GetComponent<PlayerMovement>().GetCurrentState();
			
			// after we shove, we need to stop shoving animation else we loop forever
			if (clipName.Equals("Shove"))
				anim.AnimationCompleted = ShoveCompleteDelegate;
			else
				anim.AnimationCompleted = null;
			
			anim.Play(clipName);
		}
	}
	
	public void PlayStumbleOnce()
	{
		anim.AnimationCompleted = StumbleCompleteDelegate;
		anim.Play ("Stumble");
		stumble = true;
	}
	
	void ShoveCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clilp)
	{
		GetComponent<PlayerMovement>().isShoving = false;
	}
	
	void StumbleCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clilp)
	{
		stumble = false;
	}
}
