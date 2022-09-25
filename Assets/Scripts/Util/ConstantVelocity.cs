using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ConstantVelocity : MonoBehaviour
{
	public Vector3 velocity;
	Rigidbody rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		rb.velocity = velocity;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		rb.velocity = velocity;
	}
}
