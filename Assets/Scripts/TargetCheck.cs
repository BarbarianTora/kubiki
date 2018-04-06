using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : DefaultTrackableEventHandler {

	public GameObject[] blocks = null;

	public void OnTargetFound()
	{
		base.OnTrackingFound ();
		foreach (GameObject obj in blocks) 
		{
			obj.SetActive(true); 

		}

	}
	public void OnTargetLost()
	{
		foreach (GameObject obj in blocks) 
		{
			obj.SetActive(false); 
		}
	}
}


