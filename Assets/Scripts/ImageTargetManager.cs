using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageTargetManager : DefaultTrackableEventHandler
{
	[SerializeField]
	private List<GameObject> _listOfManagmentObjects = new List<GameObject>();

	protected override void Start()
	{
		base.Start ();
		StateManagmentObjectsSwitcher (false);
	}
		
	protected override void OnTrackingFound()
	{
		StateManagmentObjectsSwitcher (true);
	}
		
	protected override void OnTrackingLost()
	{
		StateManagmentObjectsSwitcher (false);
	}

	private void StateManagmentObjectsSwitcher(bool state)
	{
		foreach (GameObject managmentObject in _listOfManagmentObjects)
			{
				if(managmentObject != null)
				managmentObject.SetActive (state);
			}
	}
		
//
//	protected override void OnDestroy()
//	{
//		base.OnDestroy ();
//	}
//
}