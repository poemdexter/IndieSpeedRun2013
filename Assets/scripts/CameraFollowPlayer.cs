using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	
	public GameObject player;
	public bool winCondition = false;
	
	// as long as we aren't winning keep up with player
	void Update () {
		if (!winCondition) transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
	
	public void WinAndStopCamera()
	{
		winCondition = true;
	}
}
