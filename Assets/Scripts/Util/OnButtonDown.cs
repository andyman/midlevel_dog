using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class OnButtonDown : MonoBehaviour
{
	public string button = "Jump";
	public UnityEvent action;

	public bool singleUse = false;
	public bool used = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown(button))
		{
			if (singleUse && used) return;

			used = true;

			action.Invoke();
		}
	}
}
