﻿using UnityEngine;
using System.Collections;

public class HeartMeterScript : MonoBehaviour {
	
	private int maxHearts = 3;
	private int currentHearts = 3;
	private tk2dSprite heart;
	private Vector3 defaultScale = new Vector3(1, 1, 1);
	private Vector3 heartScaleSpeed = new Vector3(-0.05f, -0.05f, 0);
	
	void Update()
	{
		// shrink hearts that were scaled up
		for(int i = 1; i <= maxHearts; i++)
		{
			heart = transform.FindChild("Heart"+i).GetComponent<tk2dSprite>();
			if(heart.transform.localScale.x > defaultScale.x || heart.transform.localScale.y > defaultScale.y)
			{
				heart.transform.localScale += heartScaleSpeed;
			}
			else
			{
				heart.transform.localScale = defaultScale;
			}
		}
	}
	
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
				heart.transform.localScale = new Vector3(2, 2, 1);
			}
		}
	}
	
	// change the last empty heart to full (health pickup)
	public void IncreaseHearts()
	{
		if (currentHearts < maxHearts)
		{
			currentHearts++;
			
			for(int i = 1; i <= currentHearts; i++)
			{
				heart = transform.FindChild("Heart"+i).GetComponent<tk2dSprite>();
				heart.SetSprite("HeartFull");
				if(i == currentHearts)
				{
					heart.transform.localScale = new Vector3(2, 2, 1);
				}
			}
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
