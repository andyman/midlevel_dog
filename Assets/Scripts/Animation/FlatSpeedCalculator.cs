using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlatSpeedCalculator : MonoBehaviour {

	public string speedParameter;
	public string forwardSpeedParameter;
	public string rightSpeedParameter;

	public float flatSpeed = 0.0f;
	public float dampTime = 0.1f;

	public Animator animator;
	public int speedHash = -1;
	public int forwardSpeedHash = -1;
	public int rightSpeedHash = -1;

	private Vector3 lastPosition;
	public bool useLocalPosition = false;

	// Use this for initialization
	void Start () {
		if (animator == null)
		{
			animator = GetComponent<Animator>();
		}
		speedHash = (speedParameter == "" || speedParameter == null) ? -1 : Animator.StringToHash(speedParameter);
		forwardSpeedHash = (forwardSpeedParameter == "" || forwardSpeedParameter == null) ? -1 : Animator.StringToHash(forwardSpeedParameter);
		rightSpeedHash = (rightSpeedParameter == "" || rightSpeedParameter == null) ? -1 : Animator.StringToHash(rightSpeedParameter);
		lastPosition = GetPosition();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		Vector3 newPosition = GetPosition();
		Vector3 diff = newPosition - lastPosition;
		diff.y = 0.0f;
		flatSpeed = diff.magnitude / Time.deltaTime;
		Vector3 dir = diff.normalized;
		float forwardSpeed = Vector3.Dot(transform.forward, dir) * flatSpeed;
		float rightSpeed = Vector3.Dot(transform.right, dir) * flatSpeed;

		lastPosition = newPosition;
		if (speedHash != -1)
		{
			animator.SetFloat(speedHash, flatSpeed, dampTime, Time.deltaTime);
		}

		if (forwardSpeedHash != -1)
		{
			animator.SetFloat(forwardSpeedHash, forwardSpeed, dampTime, Time.deltaTime);
		}

		if (rightSpeedHash != -1)
		{
			animator.SetFloat(rightSpeedHash, rightSpeed, dampTime, Time.deltaTime);
		}
	}
	private Vector3 GetPosition()
	{
		return (useLocalPosition ? transform.localPosition : transform.position);
	}
}
