using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnoozeButtonTrigger : MonoBehaviour
{
	public ClockBlinker clock;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerStay(Collider other)
	{
		clock.OnTriggerStay(other);
	}
}