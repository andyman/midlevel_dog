using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalBlendshape : MonoBehaviour
{
	public SkinnedMeshRenderer skinnedMeshRenderer;

	public int[] blendShapeIndex = new int[] { 0 };
	public Vector2[] range;
	public float period = 3.0f;

	public float timeMultiplier = 1.0f;

	private float time = 0.0f;
	public float timeOffset = 0.0f;

	private const float TWO_PI = 6.2831853f;

	// Start is called before the first frame update
	void Start()
	{
		time = timeOffset;
	}

	// Update is called once per frame
	void Update()
	{
		time += Time.deltaTime * timeMultiplier;

		float value = Mathf.Sin(time * TWO_PI / period) * 0.5f + 0.5f;

		for (int i = 0; i < blendShapeIndex.Length; i++)
		{
			float blend = Mathf.Lerp(range[i].x, range[i].y, value);

			skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex[i], blend);
		}

	}
}
