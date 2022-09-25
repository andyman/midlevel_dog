using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarLane : MonoBehaviour
{
	private Vector3 original;

	private float startTime, endTime = -1;
	private Vector3 startPos, endPos;


	// Start is called before the first frame update
	void Start()
	{
		original = transform.position;
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time >= endTime)
		{
			startTime = Time.time;
			endTime = startTime + Random.Range(1.0f, 6.0f);
			startPos = transform.position;
			endPos = original + Vector3.forward * Random.Range(-1.0f, 1.0f);
		}

		float t = Mathf.InverseLerp(startTime, endTime, Time.time);
		t = Mathf.SmoothStep(0.0f, 1.0f, t);
		transform.position = Vector3.Lerp(startPos, endPos, t);
	}
}
