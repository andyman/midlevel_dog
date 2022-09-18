using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// moves the dog's hands over the keyboard and mouse
public class KeyboardMouseHandsController : MonoBehaviour
{
	public Animator animator;

	public Transform ikHandL;
	public Transform ikHandR;

	public Transform mouse;

	public Collider leftHandZone;
	public Collider rightHandZone;
	public Collider mouseZone;

	public IKTracker ikTracker;

	private float startTimeL, endTimeL;
	private Vector3 startPosL, endPosL;

	private float startTimeR, endTimeR;
	private Vector3 startPosR, endPosR;

	public float handRaiseHeight = 0.2f;
	public Vector2 pressTimeRange = new Vector2(0.25f, 0.5f);

	private bool mouseMode = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	private void OnEnable()
	{
		ikTracker.leftHandPositionWeight = 1.0f;
		ikTracker.rightHandPositionWeight = 1.0f;
		ikTracker.leftHandTracker = ikHandL;
		ikTracker.rightHandTracker = ikHandR;
	}

	private void OnDisable()
	{
		ikTracker.leftHandPositionWeight = 0.0f;
		ikTracker.rightHandPositionWeight = 0.0f;
		ikTracker.leftHandTracker = null;
		ikTracker.rightHandTracker = null;
	}

	// Update is called once per frame
	void Update()
	{
		float time = Time.time;

		if (time > endTimeL)
		{
			// make a new left hand position
			startPosL = ikHandL.position;
			Bounds bounds = leftHandZone.bounds;

			endPosL = new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				Random.Range(bounds.min.y, bounds.max.y),
				Random.Range(bounds.min.z, bounds.max.z)
				);

			startTimeL = time;
			endTimeL = time + Random.Range(pressTimeRange.x, pressTimeRange.y);
		}

		// move left hand
		float lerpTime = Mathf.InverseLerp(startTimeL, endTimeL, time);
		ikHandL.position = Vector3.Lerp(startPosL, endPosL, lerpTime) + Vector3.up * Mathf.Sin(lerpTime * Mathf.PI) * handRaiseHeight;

		// right hand
		if (time > endTimeR)
		{
			// keyboard
			if (Random.value < 0.66f)
			{
				// make a new left hand position
				startPosR = ikHandR.position;
				Bounds bounds = rightHandZone.bounds;

				endPosR = new Vector3(
					Random.Range(bounds.min.x, bounds.max.x),
					Random.Range(bounds.min.y, bounds.max.y),
					Random.Range(bounds.min.z, bounds.max.z)
					);

				startTimeR = time;
				endTimeR = time + Random.Range(pressTimeRange.x, pressTimeRange.y);
				mouseMode = false;
			}
			else
			{
				mouseMode = true;


			}
		}

		// move right hand
		lerpTime = Mathf.InverseLerp(startTimeR, endTimeR, time);
		ikHandR.position = Vector3.Lerp(startPosR, endPosR, lerpTime) + Vector3.up * Mathf.Sin(lerpTime * Mathf.PI) * handRaiseHeight;




	}
}
