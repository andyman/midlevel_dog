using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ClockBlinker : MonoBehaviour
{
	public TextMeshProUGUI textUI;

	public float time;

	public float timeRate = 1.0f;
	public int secondsPerMinute = 60;

	private float nextUpdateTime = 0;

	public bool alarmSet = false;

	public float alarmTime = 21540 + 60;
	public UnityEvent alarmEvent;
	public UnityEvent snoozeEvent;

	public Material notGlowMaterial;
	public Material glowMaterial;
	public MeshRenderer clockRenderer;

	public bool ringing = false;

	public Rigidbody rb;

	public float shake = 1.0f;
	public float snoozeInterval = 30.0f;

	public SleepPawController paw;

	public SinusoidalBlendshape dogSnorer;
	public GameObject alarmSounder;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		time = time + Time.deltaTime * timeRate;

		if (alarmSet && time >= alarmTime)
		{
			Material[] materials = clockRenderer.sharedMaterials;
			materials[1] = glowMaterial;
			clockRenderer.sharedMaterials = materials;
			ringing = true;

			alarmSet = false;
			alarmEvent.Invoke();
			dogSnorer.timeMultiplier = 2.0f;
			alarmSounder.SetActive(true);
		}

		if (Time.time > nextUpdateTime)
		{
			int seconds = Mathf.FloorToInt(time);
			int hours = seconds / (secondsPerMinute * 60);
			seconds -= hours * (secondsPerMinute * 60);

			int minutes = seconds / secondsPerMinute;
			seconds = seconds - minutes * secondsPerMinute;

			string text = null;
			if (seconds % 2 == 0)
			{
				text = string.Format("{0}:{1:D2}", hours, minutes);
			}
			else
			{
				text = string.Format("{0}<color=#ff000000>:</color>{1:D2}", hours, minutes);
			}
			textUI.text = text;

			nextUpdateTime = Time.time + 0.1f;

			if (ringing)
			{
				// apply a random physics force
				Vector3 force = Random.onUnitSphere;
				//force.y = Mathf.Abs(force.y);

				force *= shake;
				rb.AddForce(force, ForceMode.Impulse);
			}
		}
	}

	public void SnoozePressed()
	{
		if (!ringing || !paw.pawDown) return;

		ringing = false;

		Material[] materials = clockRenderer.sharedMaterials;
		materials[1] = notGlowMaterial;
		clockRenderer.sharedMaterials = materials;

		alarmTime = time + snoozeInterval;
		alarmSet = true;

		dogSnorer.timeMultiplier = 1.0f;
		alarmSounder.SetActive(false);
		snoozeEvent.Invoke();
	}

	public void WakePressed()
	{
		if (!ringing) return;

	}

	public void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == 8)
		{
			Debug.Log("SnoozePressed");
			SnoozePressed();
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		OnTriggerStay(other);
	}
}
