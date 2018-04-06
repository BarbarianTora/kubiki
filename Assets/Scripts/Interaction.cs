using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour {

	private bool moGrabbed = false;

	Ray moRay;
	RaycastHit moHit;
	RaycastHit groundHit;
	RaycastHit mousePosHit;

	[SerializeField]
	private LayerMask WhatIsMO;
	[SerializeField]
	private LayerMask whatIsGround ;
	[SerializeField]
	private float _moYOffsetFromGround = 0f;
	[SerializeField]
	private float _mousePosYOffsetGround = 0f;
	[SerializeField]
	private Transform _mousePosMarker = null;

	private Transform _moTransform = null;
	private Transform _ground = null;
	private Vector3 _mousePosRelG = Vector3.zero;

	void Start () 
	{
		moHit = new RaycastHit ();
		groundHit = new RaycastHit();
		_mousePosMarker.gameObject.SetActive(false);

	}

	void Update () 
	{
		moRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		if (Input.GetMouseButtonDown (0))
			FindAndDragMO();

		if (Input.GetMouseButtonUp (0))
			DropMo ();
			
		
		moGrabbed = _moTransform != null;

		_mousePosMarker.gameObject.SetActive (moGrabbed);

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
			_moTransform = moHit.transform;
			_moTransform.GetComponent<Rigidbody> ().isKinematic = true;
			FindGround ();
		}

	}

	void FindGround ()
	{
		if (Physics.Raycast (_moTransform.position,
			    Vector3.down,
			    out groundHit,
			    Mathf.Infinity,
			    whatIsGround)) 
		{
			_ground = groundHit.transform;
		}
	}

	void TraceMousePosRelToGround ()
	{
		if (Physics.Raycast (moRay,
			    out mousePosHit,
			    Mathf.Infinity,
			    whatIsGround)) 
		{
			_mousePosRelG = mousePosHit.point;
			_moTransform.position = new Vector3 (_mousePosRelG.x, 
												_mousePosRelG.y + _moYOffsetFromGround,
												_mousePosRelG.z);
			_mousePosMarker.position = new Vector3 (_mousePosRelG.x, 
													_mousePosRelG.y + _mousePosYOffsetGround,
													_mousePosRelG.z);
		}
	}

	void DropMo()
	{
		if (_moTransform != null)
			_moTransform.GetComponent<Rigidbody> ().isKinematic = false;
		_moTransform = null;
		_ground = null;
	}


}
