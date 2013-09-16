using UnityEngine;
using System.Collections;

/*
 * Sign waits to be collided by Dragon then slowly topples over
 */

public class SignScript : MonoBehaviour {
	
	public bool isToppling = false;
	private Vector3 throwVector = new Vector3(800, 400, 0);
	private float throwRotation = -120;
	
	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.CompareTag("Dragon") && !isToppling)
		{
			// tell game to ignore this collision
			GameObject.Find("SceneController").GetComponent<SceneControllerScript>().IgnoreSignCollision(gameObject.layer);
			
			isToppling = true;
			rigidbody.isKinematic = false;
			rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;	// necessary to make object rotate when thrown
			rigidbody.AddForce(throwVector);
			rigidbody.AddTorque(new Vector3(0,0,throwRotation));
			rigidbody.useGravity = true;									// necessary to make object throw more nicely
		}
	}
}
