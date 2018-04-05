using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	public bool moGrabbed = false;

	Ray moRay;
	public Transform moTransform;
	public LayerMask WhatIsMO;
	RaycastHit moHit;

	public LayerMask whatIsGround;
	public Transform ground;
	RaycastHit groundHit;

	public Transform mousePosMarker;
	RaycastHit mousePosHit;
	public float moYOffsetFromGround = 0f;
	public float mousePosYOffsetGround = 0f;
	public Vector3 mousePosRelG;

	void Start () 
	{
		moHit = new RaycastHit ();
		groundHit = new RaycastHit();
		mousePosMarker.gameObject.SetActive(false);
	}

	void Update () 
	{
		moRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
			FindAndDragMO();

		if (Input.GetMouseButtonUp (0))
			DropMo ();
			
		
		moGrabbed = moTransform != null;

		mousePosMarker.gameObject.SetActive (moGrabbed);

		if (moGrabbed)
			TraceMousePosRelToGround ();
	}

	void FindAndDragMO ()
	{
		if (Physics.Raycast (moRay,
							 out moHit,
							 Mathf.Infinity,
							 WhatIsMO)) 
		{
			moTransform = moHit.transform;
			moTransform.GetComponent<Rigidbody> ().isKinematic = true;
			FindGround ();
		}

	}

	void FindGround ()
	{
		if (Physics.Raycast (moTransform.position,
			    Vector3.down,
			    out groundHit,
			    Mathf.Infinity,
			    whatIsGround)) 
		{
			ground = groundHit.transform;
		}
	}

	void TraceMousePosRelToGround ()
	{
		if (Physics.Raycast (moRay,
			    out mousePosHit,
			    Mathf.Infinity,
			    whatIsGround)) 
		{
			mousePosRelG = mousePosHit.point;
			moTransform.position = new Vector3 (mousePosRelG.x, 
											mousePosRelG.y + moYOffsetFromGround,
											mousePosRelG.z);
			mousePosMarker.position = new Vector3 (mousePosRelG.x, 
													mousePosRelG.y + mousePosYOffsetGround,
													mousePosRelG.z);
		}
	}

	void DropMo()
	{
		if (moTransform != null)
			moTransform.GetComponent<Rigidbody> ().isKinematic = false;
		moTransform = null;
		ground = null;
	}


}
