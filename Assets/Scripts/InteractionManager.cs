using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InteractionManager : MonoBehaviour 
{
	public delegate void OnInteractionChangeDelegate (bool State);
	public static event  OnInteractionChangeDelegate OnInteractionStateChange;		

	[SerializeField]
	private DragAndDrop _interaction = null;

	[SerializeField]
	private Button _btn = null;
	[SerializeField]
	private Text _btnText = null;

	public bool onButton = false;

	void Start () 
	{
		_btn.onClick.AddListener(OnBtn);	
	}

	void Update ()
	{
		if (onButton)
			_btnText.text = "Click here to move objects";
		else
			_btnText.text = "Click here to rotate objects";
	}

	void OnBtn ()
	{

		onButton = !onButton;

		_interaction.enabled = !onButton;

		if (OnInteractionStateChange != null)        
			OnInteractionStateChange (onButton);
	}
}