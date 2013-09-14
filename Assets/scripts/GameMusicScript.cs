using UnityEngine;
using System.Collections;

public class GameMusicScript : MonoBehaviour {
	
	public AudioClip musicStart;
	public AudioClip musicLoop;
	
	void Start()
	{
		AudioSource[] sources = GetComponents<AudioSource>();
		sources[0].clip = musicStart;
		sources[0].loop = false;
		sources[0].Play();
		
		float clipLength = musicStart.length;
		sources[1].clip = musicLoop;
		sources[1].loop = true;
		sources[1].PlayDelayed(clipLength);
	}
	
	void Update()
	{
		
	}
}
