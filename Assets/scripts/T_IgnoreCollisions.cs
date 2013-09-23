using UnityEngine;
using System.Collections;

public class T_IgnoreCollisions : MonoBehaviour {

	void Alive()
	{
		Physics.IgnoreLayerCollision(8,9); // particles on dragon
		Physics.IgnoreLayerCollision(14,16); // player on queen
		Physics.IgnoreLayerCollision(17,16); // ground on queen
	}
}
