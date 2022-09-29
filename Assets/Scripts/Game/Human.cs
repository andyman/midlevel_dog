using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Human : MonoBehaviour
{
	public float speed = 4.0f;

	private float nextTime;
	private Vector3 target;

	public Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time >= nextTime)
		{
			if (transform.position.magnitude > 8.0f)
			{
				target = Vector3.zero;
			}
			else
			{
				Vector2 circle = Random.insideUnitCircle * 5.0f;
				target = transform.position +
					new Vector3(
						circle.x,
						0.0f,
						circle.y
						);
			}
			nextTime = Time.time + Random.Range(5.0f, 10.0f);
		}
	}

	private void FixedUpdate()
	{
		Vector3 diff = target - transform.position;
		diff.y = 0.0f;

		float dist = diff.magnitude;

		if (dist > 0.5f)
		{
			Vector3 dir = diff.normalized;

			rb.velocity = Vector3.Lerp(rb.velocity, dir * Random.value * speed, Time.deltaTime * 20.0f);
		}

		Vector3 v = rb.velocity;
		v.y = 0;
		if (v.magnitude > 0.1f)
		{

			v.Normalize();
			Quaternion rot = rb.rotation;
			Quaternion newRot = Quaternion.Slerp(rot, Quaternion.LookRotation(v), Time.deltaTime * 5.0f);
			rb.MoveRotation(newRot);
		}
	}
}
