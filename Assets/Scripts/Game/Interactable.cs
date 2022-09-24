using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
	public bool hovering = false;

	public UnityEvent interactEvent;

	public GameObject uiObject;

	public bool oneTimeUse = false;
	public bool used = false;


	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Highlight()
	{
		hovering = true;
		ShowUI();
	}

	public void RemoveHighlight()
	{
		hovering = false;
		HideUI();
	}

	virtual public void Interact()
	{
		if (oneTimeUse && used) return;

		Debug.Log("Interact with " + name);

		interactEvent.Invoke();

		if (oneTimeUse)
		{
			used = true;
			enabled = false;
		}
	}

	private void ShowUI()
	{
		uiObject.SetActive(true);
	}

	private void HideUI()
	{
		uiObject.SetActive(false);
	}
}
