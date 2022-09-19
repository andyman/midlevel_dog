using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIAlphaFader : MonoBehaviour {

	public float targetAlpha;
	public float currentAlpha;
	public float transitionTime;
	public bool useUnscaledTime = false;
	public bool destroyAfterUse = false;

	public Graphic ui = null;

	private float startTime;
	private float endTime;

	private float startAlpha;

	void Awake()
	{
        if (ui == null)
        {
            ui = GetComponent<Graphic>();
        }
		Color c = ui.color;
		c.a = currentAlpha;
		ui.color = c;
	}

	// Use this for initialization
	void OnEnable () {
		if (ui == null)
		{
			ui = GetComponent<Graphic>();
		}
		Color c = ui.color;
		c.a = currentAlpha;
		ui.color = c;

		Restart();
	}

	public void Restart()
	{
		if (ui == null)
		{
			ui = GetComponent<Graphic>();
		}
		float time = useUnscaledTime ? Time.unscaledTime : Time.time;
		startTime = time;
		endTime = time + transitionTime;
		Color c = ui.color;
		startAlpha = c.a;
	}

	// Update is called once per frame
	void Update () {
		float time =  useUnscaledTime ? Time.unscaledTime : Time.time;
		if (time < endTime)
		{
			float lerp = Mathf.InverseLerp(startTime, endTime, time);
			currentAlpha = Mathf.Lerp(startAlpha, targetAlpha, lerp);
		}
		else
		{
			currentAlpha = targetAlpha;
		}
	
		Color c = ui.color;
		c.a = currentAlpha;
		ui.color = c;
		ui.enabled = currentAlpha >= 0.01f;

		if (destroyAfterUse && time > endTime)
		{
			Destroy(gameObject);
		}
	}
	public void Fade(float beginAlpha, float endAlpha, float duration)
	{
		SetAlpha(beginAlpha);
		startAlpha = beginAlpha;
		targetAlpha = endAlpha;
		transitionTime = duration;
		Restart();
	}
	public void SetAlpha(float alpha)
	{
		currentAlpha = alpha;
		targetAlpha = alpha;
		startAlpha = alpha;

		Color c = ui.color;
		c.a = currentAlpha;
		ui.color = c;

		endTime = 0.0f;
	}

	public void SetTargetAlpha(float alpha)
	{
		if (ui == null)
		{
			ui = GetComponent<Graphic>();
		}
		targetAlpha = alpha;
		Restart();
	}

	public void SetCurrentAlpha(float alpha)
	{
		if (ui == null)
		{
			ui = GetComponent<Graphic>();
		}
		currentAlpha = alpha;
		Color c = ui.color;
		c.a = currentAlpha;
		ui.color = c;

		Restart();
	}
}
