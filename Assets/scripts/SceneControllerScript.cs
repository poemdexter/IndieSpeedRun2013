using UnityEngine;
using System.Collections;

public class SceneControllerScript : MonoBehaviour {
	
	public bool fading = true;
	public bool isRestarting = false;
	public Texture2D blackPixel;
	public float fadeSpeed = -0.2f;
	public float alpha = 1.0f;
	
	public AudioClip musicStart;
	public AudioClip musicLoop;
	
	void Alive()
	{
		Physics.IgnoreLayerCollision(8,9);
	}
	
	void OnGUI()
	{
		if (fading)
		{
			if(!isRestarting)
			{
				alpha += fadeSpeed * Time.deltaTime;
			}
			else
			{
				alpha -= fadeSpeed * Time.deltaTime;
			}
			
			// fade screen
			GUI.color = new Color(0,0,0,alpha);
			GUI.depth = 100;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), blackPixel);
			
			// load level if done transition
			if(!isRestarting)
			{
				if (alpha <= 0)
				{
					fading = false;
					StartGame();
				}
			}
			else
			{
				if (alpha >= 1)
				{
					fading = false;
					Application.LoadLevel(1);	// reload the Game scene
				}
			}
		}
	}
		
	void StartGame()
	{
		// start music
		AudioSource[] sources = GetComponents<AudioSource>();
		sources[0].clip = musicStart;
		sources[0].loop = false;
		sources[0].Play();
		
		float clipLength = musicStart.length;
		sources[1].clip = musicLoop;
		sources[1].loop = true;
		sources[1].PlayDelayed(clipLength);
		
		// activate the dragon and player
		GameObject.Find ("Player").GetComponent<PlayerMovement>().Activate();
		GameObject.Find ("Dragon").GetComponent<DragonBehavior>().Activate();
	}
	
	public void Restart()
	{
		// fade screen
		isRestarting = true;
		fading = true;
	}
}
