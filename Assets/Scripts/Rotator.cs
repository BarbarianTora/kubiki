﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

	[SerializeField]
	private float _rSpeed = 1f;
	private float _rY = 0f;
	private Rigidbody _rb;
	private RigidbodyConstraints _rbCon;
	private bool _rotation = false;
	private GameObject _gameObject;

	void Start ()
	{
		_rb = transform.GetComponent<Rigidbody>();
		_rbCon = _rb.constraints;

		_gameObject = gameObject;
	}

	void OnEnable()
	{
		InteractionManager.OnInteractionStateChange += Rotate;
	}

	void Disable()
	{
		InteractionManager.OnInteractionStateChange -= Rotate;
	}

	void Rotate(bool rotate)
	{
		_rotation = rotate;
		Debug.Log ("Event rotate catch");
		if (_rotation)
			StartCoroutine (Rotation ());
	}

	private IEnumerator Rotation()
	{

		_rbCon = RigidbodyConstraints.FreezePosition;
		_rb.isKinematic = true;
		while (_rotation) 
		{
			yield return new WaitForEndOfFrame ();

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;

			if (Physics.Raycast (ray, out hit)) 
			{
				if (hit.transform.gameObject == _gameObject) 
				{
					_rY = Input.GetAxis ("Mouse Y") * _rSpeed * Mathf.Deg2Rad;
					transform.RotateAround (Vector3.up, -_rY);
				}
			}
		}
	}

}