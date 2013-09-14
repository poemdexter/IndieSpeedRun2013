using UnityEngine;
using System.Collections;

public class TitlePageScript : MonoBehaviour {
	
	public GameObject credits;
	public bool creditsVisible = false;
	
	// load the game
	void StartGame()
	{
		Application.LoadLevel(1);
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
