using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIFader : MonoBehaviour {

	public Color targetColor;
	public Color currentColor;
	public float transitionTime;
	public bool useUnscaledTime = false;
	public bool destroyAfterUse = false;

	public Graphic ui = null;

	private float startTime;
	private float endTime;

	private Color startColor;

	void Awake()
	{
        if (ui == null)
        {
            ui = GetComponent<Graphic>();
        }
		ui.color = currentColor;
	}

	// Use this for initialization
	void Start () {
		ui.color = currentColor;		
		Restart();
	}

	public void Restart()
	{
		float time = useUnscaledTime ? Time.unscaledTime : Time.time;
		startTime = time;
		endTime = transitionTime;
		startColor = currentColor;
	}

	// Update is called once per frame
	void Update () {
		float time =  useUnscaledTime ? Time.unscaledTime : Time.time;
		if (time < endTime)
		{
			float lerp = Mathf.InverseLerp(startTime, endTime, time);
			currentColor = Color.Lerp(startColor, targetColor, lerp);
		}
		else
		{
			currentColor = targetColor;
		}
	
		if (ui.color != currentColor)
		{
			ui.color = currentColor;
		}

		if (destroyAfterUse && time > endTime)
		{
			Destroy(gameObject);
		}

	}

	public void SetTargetAlpha(float alpha)
	{
		targetColor.a = alpha;
		Restart();
	}

	public void SetCurrentAlpha(float alpha)
	{
		currentColor.a = alpha;
		Restart();
	}
}
