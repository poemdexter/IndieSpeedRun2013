using UnityEngine;
using System.Collections;

public class PlayerAnimation : MonoBehaviour
{	
	tk2dSpriteAnimator anim;

	void Start ()
	{
		anim = GetComponent<tk2dSpriteAnimator>();
	}
	
	void Update ()
	{
		string clipName = GetComponent<PlayerMovement>().GetCurrentState();
		
		// after we shove, we need to stop shoving animation else we loop forever
		if (clipName.Equals("Shove"))
			anim.AnimationCompleted = ShoveCompleteDelegate;
		else 
			anim.AnimationCompleted = null;
		
		anim.Play(clipName);
	}
	
	void ShoveCompleteDelegate(tk2dSpriteAnimator sprite, tk2dSpriteAnimationClip clilp)
	{
		GetComponent<PlayerMovement>().isShoving = false;
	}
}
