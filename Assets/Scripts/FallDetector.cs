using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDetector : MonoBehaviour {

	private const float _xMin = -3.2f;
	private const float _zMin = -2.96f;
	private const float _xMax = 5.5f;
	private const float _zMax = 4.74f;
	private const float _spawnY =  4f;

	void Start () 
	{
		
	}
	
	void Update () 
	{
		
	}

	void OnTriggerEnter (Collider col)
	{
		if (col.tag == "MO") 
		{
			col.transform.position = new Vector3 (Random.Range (_xMin, _xMax),
												  _spawnY, 
												  Random.Range (_zMin, _zMax));
		}
	}
}
