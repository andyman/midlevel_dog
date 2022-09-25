using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CopyFogColorToCamera : MonoBehaviour
{
	public Camera cam;

	// Update is called once per frame
	void Update()
	{
		if (cam != null)
		{
			cam.backgroundColor = RenderSettings.fogColor;
		}
	}
}
