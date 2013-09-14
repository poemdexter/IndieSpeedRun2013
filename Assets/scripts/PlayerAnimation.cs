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
		anim.Play(clipName);
	}
}
