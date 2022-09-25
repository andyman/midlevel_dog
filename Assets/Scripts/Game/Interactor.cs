using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
	public List<Interactable> interactables = new List<Interactable>();

	public Interactable closestInteractable;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// refresh which is the active interactable based on distance

		Interactable newClosestInteractable = null;
		float closestDistance = Mathf.Infinity;

		for (int i = 0; i < interactables.Count; i++)
		{
			Interactable interactable = interactables[i];
			if (interactable.enabled)
			{
				Vector3 diff = interactable.transform.position - transform.position;
				diff.y = 0.0f;
				float dist = diff.magnitude;
				if (dist < closestDistance)
				{
					closestDistance = dist;
					newClosestInteractable = interactable;
				}
			}
		}

		if (newClosestInteractable != closestInteractable)
		{
			if (closestInteractable != null)
			{
				closestInteractable.RemoveHighlight();
			}

			if (newClosestInteractable != null)
			{
				newClosestInteractable.Highlight();
			}

			closestInteractable = newClosestInteractable;
		}

		if (closestInteractable != null)
		{
			if (Input.GetButtonDown("Jump"))
			{
				closestInteractable.Interact();
			}
		}
	}

	void OnTriggerEnter(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable == null) return;

		if (!interactables.Contains(interactable))
		{
			interactables.Add(interactable);
		}
	}

	void OnTriggerExit(Collider other)
	{
		Interactable interactable = other.GetComponent<Interactable>();
		if (interactable == null) return;

		if (interactables.Contains(interactable))
		{
			interactables.Remove(interactable);
			if (interactable == closestInteractable)
			{
				if (closestInteractable != null)
				{
					closestInteractable.RemoveHighlight();
				}

				closestInteractable = null;
			}
		}
	}
}
