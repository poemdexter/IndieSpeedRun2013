using UnityEngine;
using System.Collections;

public class SceneControllerScript : MonoBehaviour {
	
	// automatically starts the scene
	void Start()
	{
		// activate the dragon and player
		GameObject.Find ("Player").GetComponent<PlayerMovement>().Activate();
		GameObject.Find ("Dragon").GetComponent<DragonBehavior>().Activate();
	}
	
	void Update()
	{
	
	}
	
	public void Restart()
	{
		Application.LoadLevel(1);	// reload the Game scene
	}
}
