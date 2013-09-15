using UnityEngine;
using System.Collections;

public class KillParticleEffect : MonoBehaviour
{
	void Update ()
	{
		if (!particleSystem.IsAlive())
			Destroy(this.gameObject);
	}
}
