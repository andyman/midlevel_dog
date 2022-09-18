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
	public float heightAboveMouse = 0.1f;

	private bool mouseMode = false;
	private bool movingMouse = false;

	public Vector2 moveMouseTimeRange = new Vector2(0.5f, 1.0f);

	// lower the more productive
	public float sluggishness = 1.0f;
	public AudioSet typeAudioSet;

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

		Bounds bounds = leftHandZone.bounds;

		ikHandL.position = new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
			);

		bounds = rightHandZone.bounds;
		ikHandR.position = new Vector3(
			Random.Range(bounds.min.x, bounds.max.x),
			Random.Range(bounds.min.y, bounds.max.y),
			Random.Range(bounds.min.z, bounds.max.z)
			);

		startTimeL = startTimeR = Time.time;
		endTimeL = endTimeR = Time.time + 0.5f;
		startPosL = endPosL = ikHandL.position;
		startPosR = endPosR = ikHandR.position;
		mouseMode = movingMouse = false;
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

			// play audio
			if (!mouseMode)
			{
				typeAudioSet.PlayRandom(ikHandL.position);
			}

			Bounds bounds = leftHandZone.bounds;

			endPosL = new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				Random.Range(bounds.min.y, bounds.max.y),
				Random.Range(bounds.min.z, bounds.max.z)
				);

			startTimeL = time;
			endTimeL = time + Random.Range(pressTimeRange.x, pressTimeRange.y) * sluggishness;
		}

		// move left hand
		float lerpTime = Mathf.InverseLerp(startTimeL, endTimeL, time);
		ikHandL.position = Vector3.Lerp(startPosL, endPosL, lerpTime)
			+ (mouseMode ? Vector3.zero : (Vector3.up * Mathf.Sin(lerpTime * Mathf.PI) * handRaiseHeight));

		// right hand
		if (time > endTimeR)
		{
			// play audio
			if (!mouseMode)
			{
				typeAudioSet.PlayRandom(ikHandR.position);
			}

			if (mouseMode && !movingMouse
				|| mouseMode && Random.value > 0.2f
				|| !mouseMode && Random.value > 0.8f
				)
			{
				mouseMode = true;

				Vector3 mousePos = mouse.position;
				Vector3 handPos = ikHandR.position;
				mousePos.y = handPos.y;

				float dist = Vector3.Distance(mousePos, handPos);

				startPosR = ikHandR.position;

				// too far, then destination is mouse
				if (dist > 0.001f)
				{
					endPosR = mouse.position;
					endPosR.y = Random.Range(mouseZone.bounds.min.y, mouseZone.bounds.max.y);
					movingMouse = false;
				}
				else
				{
					Bounds bounds = mouseZone.bounds;

					endPosR = new Vector3(
						Random.Range(bounds.min.x, bounds.max.x),
						Random.Range(bounds.min.y, bounds.max.y),
						Random.Range(bounds.min.z, bounds.max.z)
						);
					movingMouse = true;
				}
				endTimeR = time + Random.Range(moveMouseTimeRange.x, moveMouseTimeRange.y) * sluggishness;

				// freeze the left hand while we move the mouse
				startTimeL = time;
				endTimeL = endTimeR;
				startPosL = endPosL = ikHandL.position;
				endPosL.y = Random.Range(leftHandZone.bounds.min.y, leftHandZone.bounds.max.y);

			}
			// keyboard
			else
			{
				// make a new left hand position
				startPosR = ikHandR.position;
				Bounds bounds = rightHandZone.bounds;

				endPosR = new Vector3(
					Random.Range(bounds.min.x, bounds.max.x),
					Random.Range(bounds.min.y, bounds.max.y),
					Random.Range(bounds.min.z, bounds.max.z)
					);

				mouseMode = false;
				movingMouse = false;
				endTimeR = time + Random.Range(pressTimeRange.x, pressTimeRange.y) * sluggishness;
			}

			startTimeR = time;

		}

		// move right hand
		lerpTime = Mathf.InverseLerp(startTimeR, endTimeR, time);
		ikHandR.position =
			Vector3.Lerp(startPosR, endPosR, lerpTime)
			+ (mouseMode ? Vector3.zero : Vector3.up * Mathf.Sin(lerpTime * Mathf.PI) * handRaiseHeight);

		if (mouseMode && movingMouse)
		{
			Vector3 mousePos = ikHandR.position;
			mousePos.y = mouse.position.y;

			mouse.position = mousePos;
		}
	}
}
