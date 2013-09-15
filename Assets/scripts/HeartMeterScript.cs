using UnityEngine;
using System.Collections;

public class HeartMeterScript : MonoBehaviour {
	
	private int maxHearts = 3;
	private int currentHearts = 3;
	private tk2dSprite heart;
	
	// change the last full heart to empty
	public void DepleteHearts()
	{
		if (currentHearts > 0)
		{
			currentHearts--;
			
			for(int i = maxHearts; i > (currentHearts); i--)
			{
				heart = transform.FindChild("Heart"+i).GetComponent<tk2dSprite>();
				heart.SetSprite("HeartEmpty");
			}
		}
		else
		{
			// reset scene or w/e
		}
	}
	
	public bool IsHeartMeterEmpty()
	{
		if(currentHearts > 0)
		{
			return false;
		}
		else
		{
			return true;
		}
	}
}
