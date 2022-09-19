using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
	public Collider lastCollider;
	public UnityEvent enterEvent;
	public UnityEvent stayEvent;
	public UnityEvent exitEvent;

	public void OnTriggerEnter(Collider other)
	{
		lastCollider = other;
		enterEvent.Invoke();
	}

	public void OnTriggerStay(Collider other)
	{
		lastCollider = other;
		stayEvent.Invoke();
	}

	public void OnTriggerExit(Collider other)
	{
		lastCollider = other;
		exitEvent.Invoke();
	}
}
