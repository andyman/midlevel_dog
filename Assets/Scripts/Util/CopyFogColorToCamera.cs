using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyFogColorToCamera : MonoBehaviour
{
	public Camera cam;

	// Update is called once per frame
	void Update()
	{
		cam.backgroundColor = RenderSettings.fogColor;
	}
}
