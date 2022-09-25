// INSPIRED BY Armooey's car controller
// https://github.com/armooey/Car-Controller/blob/master/Assets/Scripts/CarController.cs

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPhysics : MonoBehaviour
{
	public Rigidbody rb;
	public float torque = 60;
	public float maxSteeringAngle = 30.0f;

	public WheelCollider frontLeftWheel;
	public WheelCollider frontRightWheel;
	public WheelCollider rearLeftWheel;
	public WheelCollider rearRightWheel;

	public Transform frontRightTransform;
	public Transform frontLeftTransform;
	public Transform rearRightTransfrom;
	public Transform rearLeftTransform;

	private float steeringAngle;

	public AudioSource engineSound;
	public bool allowInputs = true;

	// Start is called before the first frame update
	void Start()
	{

	}

	private Vector2 inputs;
	private bool brakesHeld;
	private bool resetUp;

	// Update is called once per frame
	void Update()
	{
		if (allowInputs)
		{
			inputs.x = Input.GetAxis("Horizontal");
			inputs.y = Input.GetAxis("Vertical");
			brakesHeld = Input.GetButton("Jump");
			resetUp = Input.GetKey(KeyCode.R);

			float inputsSquared = inputs.y * inputs.y;
			engineSound.pitch = Mathf.Lerp(engineSound.pitch, Mathf.Lerp(0.5f, 1.0f, inputsSquared), Time.deltaTime * 2.0f);
			engineSound.volume = Mathf.Lerp(engineSound.volume, Mathf.Lerp(0.25f, 0.5f, inputsSquared), Time.deltaTime * 2.0f);
		}
		else
		{
			inputs.x = 0;
			inputs.y = 0;

		}
	}

	private void Accelerate()
	{
		frontLeftWheel.motorTorque = torque * inputs.y;
		frontRightWheel.motorTorque = torque * inputs.y;
		rearLeftWheel.motorTorque = torque * inputs.y;
		rearRightWheel.motorTorque = torque * inputs.y;
	}

	private void Steer()
	{
		steeringAngle = maxSteeringAngle * inputs.x;
		frontLeftWheel.steerAngle = steeringAngle;
		frontRightWheel.steerAngle = steeringAngle;
	}

	private void UpdateWheelPoses()
	{
		UpdateWheelPose(frontLeftWheel, frontLeftTransform);
		UpdateWheelPose(frontRightWheel, frontRightTransform);
		UpdateWheelPose(rearLeftWheel, rearLeftTransform);
		UpdateWheelPose(rearRightWheel, rearRightTransfrom);
	}

	private void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
	{
		Vector3 pos = wheelTransform.position;
		Quaternion rot = wheelTransform.rotation;
		wheelCollider.GetWorldPose(out pos, out rot);
		wheelTransform.position = pos;
		wheelTransform.rotation = rot;
	}

	private void FixedUpdate()
	{
		if (resetUp)
		{
			Vector3 forward = transform.forward;
			transform.rotation = Quaternion.LookRotation(forward, Vector3.up);
			resetUp = false;
		}
		Steer();
		Accelerate();
		UpdateWheelPoses();
	}
}
