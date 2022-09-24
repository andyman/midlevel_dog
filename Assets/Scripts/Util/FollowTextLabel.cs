using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class FollowTextLabel : MonoBehaviour
{

	public Transform target;
	public Vector3 worldOffset;
	public Vector3 pixelOffset;

	private Transform cameraTransform;
	private Camera cam;


	// Use this for initialization
	void OnEnable()
	{
		cam = Camera.main;

		cameraTransform = cam.transform;

		RefreshTransform();
	}

	void RefreshTransform()
	{
		Vector3 screenPos = cam.WorldToScreenPoint(target.position + worldOffset) + pixelOffset;

		transform.position = screenPos;

		//transform.position = target.position + offset;
		//transform.rotation = Quaternion.LookRotation((transform.position - cameraTransform.position).normalized);
	}

	// Update is called once per frame
	void Update()
	{
		if (target == null) return;

		RefreshTransform();
	}
}
