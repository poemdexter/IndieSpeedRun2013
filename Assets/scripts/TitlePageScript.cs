using UnityEngine;
using System.Collections;

public class TitlePageScript : MonoBehaviour {
	
	public GameObject credits;
	public bool creditsVisible = false;
	public bool fading = false;
	
	public Texture2D blackPixel;
	public float fadeSpeed = 0.2f;
	
	public float alpha = 0.0f;
	
	void StartGame()
	{
		// fade to black
		fading = true;
	}
	
	void OnGUI()
	{
		if (fading)
		{
			alpha += fadeSpeed * Time.deltaTime;
			
			// fade screen
			GUI.color = new Color(0,0,0,alpha);
			GUI.depth = 100;
			GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), blackPixel);
			
			// fade music
			audio.volume = 1 - alpha;
			
			// load level if done transition
			if (alpha >= 1) 
				Application.LoadLevel(1);
		}
	}
	
	// show team credits
	void ShowCredits()
	{
		Renderer[] renderers = credits.GetComponentsInChildren<Renderer>();
	    foreach (Renderer r in renderers)
		{
	        r.enabled = !r.enabled;
	    }
	}
}
