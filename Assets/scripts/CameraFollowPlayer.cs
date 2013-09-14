using UnityEngine;
using System.Collections;

public class CameraFollowPlayer : MonoBehaviour {
	
	public GameObject player;
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);
	}
}
