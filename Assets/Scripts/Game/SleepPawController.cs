using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepPawController : MonoBehaviour
{
	public Rigidbody paw;
	public BoxCollider pawBoundsRange;
	public Transform pawRestPosition;

	public float pawSpeed;
	public bool pawDown;
	public float pawDownHeight = -0.2f;
	public float pawDownSpeed = 1.0f;

	public ClockBlinker clock;

	private Camera mainCamera;
	private float horizontal;
	private float vertical;

	private float slapUntilTime;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown("Jump"))
		{
			slapUntilTime = Time.time + 0.1f;
			pawDown = true;
		}
		if (Time.time > slapUntilTime)
		{
			pawDown = false;
		}
		horizontal = Input.GetAxis("Horizontal");
		vertical = Input.GetAxis("Vertical");
	}


	private void FixedUpdate()
	{
		if (clock.ringing)
		{
			Bounds bounds = pawBoundsRange.bounds;

			if (mainCamera == null || !mainCamera.gameObject.activeInHierarchy)
			{
				mainCamera = Camera.main;
			}

			// flatten cam forward
			Vector3 camForward = mainCamera.transform.forward;
			camForward.y = 0.0f;
			camForward.Normalize();

			Quaternion camFlatRot = Quaternion.LookRotation(camForward);

			Vector3 newPosition = paw.position + camFlatRot * new Vector3(horizontal, 0.0f, vertical) * pawSpeed;
			Vector3 boundPosition = newPosition;
			boundPosition.y = bounds.center.y;

			newPosition.x = Mathf.Clamp(boundPosition.x, bounds.min.x, bounds.max.x);
			newPosition.z = Mathf.Clamp(boundPosition.z, bounds.min.z, bounds.max.z);

			float newHeight = pawDown ? bounds.center.y + pawDownHeight : bounds.center.y;

			newPosition.y = Mathf.MoveTowards(newPosition.y, newHeight, pawDownSpeed * Time.deltaTime);
			paw.MovePosition(newPosition);

		}
		else
		{
			paw.MovePosition(Vector3.MoveTowards(paw.position, pawRestPosition.position, 0.5f * Time.deltaTime));
		}
	}
}
