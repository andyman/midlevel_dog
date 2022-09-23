using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamLevelController : MonoBehaviour
{
	public DreamRunController dreamRunController;
	public ClockBlinker clockBlinker;
	public ParticleSystem maskParticleSystem;
	public AudioSource dreamMusic;
	public Transform groundMover;

	private float dreamLevel = 0.0f;
	public Camera cam;
	public bool dreamingStarted = false;
	private ParticleSystem.EmissionModule maskParticleSystemEmission;
	public float fullEmissionRate = 60.0f;

	public void StartDreaming()
	{
		dreamingStarted = true;

	}
	// Start is called before the first frame update
	void Start()
	{
		maskParticleSystemEmission = maskParticleSystem.emission;

	}

	// Update is called once per frame
	void Update()
	{
		if (!dreamingStarted) return;

		bool ringing = clockBlinker.ringing;

		dreamLevel = Mathf.Lerp(dreamLevel, ringing ? 0.0f : 1.0f, 1.0f * Time.deltaTime);
		float dreamLevelSquared = dreamLevel * dreamLevel;

		//maskParticleSystem.transform.localScale = Vector3.one * dreamLevel;
		maskParticleSystemEmission.rateOverTime = fullEmissionRate * dreamLevelSquared;

		dreamMusic.volume = dreamLevel * 0.25f;
		dreamMusic.pitch = dreamLevelSquared;

		dreamRunController.controllable = !ringing;

	}
}
