using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusoidalBlendshape : MonoBehaviour
{
	public SkinnedMeshRenderer skinnedMeshRenderer;

	public int blendShapeIndex = 0;
	public Vector2 range = new Vector2(0.0f, 100.0f);
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
		value = Mathf.Lerp(range.x, range.y, value);
		skinnedMeshRenderer.SetBlendShapeWeight(blendShapeIndex, value);
	}
}
