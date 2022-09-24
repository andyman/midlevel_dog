using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Light))]
public class Torch : MonoBehaviour
{
	private Light light;

	public Gradient colorRange;
	public Vector2 brightnessRange;

	private float startTime;
	private float endtime;
	private Color startColor, endColor;
	private float startBrightness, endBrightness;

	// Start is called before the first frame update
	void Start()
	{
		light = GetComponent<Light>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Time.time > endtime)
		{
			startTime = Time.time;
			endtime = startTime + Random.Range(0.01f, 0.2f);

			startColor = light.color;
			endColor = colorRange.Evaluate(Random.value);

			startBrightness = light.intensity;
			endBrightness = Random.Range(brightnessRange.x, brightnessRange.y);
		}

		float lerpTime = Mathf.InverseLerp(startTime, endtime, Time.time);

		light.color = Color.Lerp(startColor, endColor, lerpTime);
		light.intensity = Mathf.Lerp(startBrightness, endBrightness, lerpTime);
	}
}
