using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Events;
public class DreamLevelController : MonoBehaviour
{
	public static bool endResultRested = false;

	public DreamRunController dreamRunController;
	public ClockBlinker clockBlinker;
	public ParticleSystem maskParticleSystem;
	public AudioSource dreamMusic;
	public Transform groundMover;

	public ParticleSystem dreamGrabParticleSystem;

	private float dreamLevel = 1.0f;
	public Camera cam;
	public bool dreamingStarted = false;
	private ParticleSystem.EmissionModule maskParticleSystemEmission;
	public float fullEmissionRate = 60.0f;

	public float depthOfSleep = 1.0f;
	public float restedness = 0.0f;

	public float restednessPerBone = 0.1f;
	public float alarmDepthRate = -0.1f;
	public float depthDecay = -0.01f;

	public Slider restSlider;
	public Slider depthOfSleepSlider;

	public Color normalSliderColor = Color.white;
	public Color alarmSliderColor = Color.red;
	public Color yellowColor = Color.yellow;
	public Image depthOfSleepBar;
	public Image restBar;

	public TextMeshProUGUI depthOfSleepText;
	public TextMeshProUGUI restText;

	public DreamBone bonePrefab;
	public BoxCollider dreamBoundsCollider;
	private Bounds dreamBounds;
	private List<DreamBone> bones = new List<DreamBone>();
	public Vector2 boneInterval = new Vector2(1.0f, 1.5f);
	private float nextBoneTime;
	private float restSlideYellowUntilTime;

	public AudioSet bonePickupAudio;

	public UnityEvent endRestedEvent;
	public UnityEvent endUnrestedEvent;

	public void RefreshUI()
	{
		restSlider.value = restedness;

		depthOfSleepSlider.value = depthOfSleep;

		Color barColor = normalSliderColor;
		float scale = 1.0f;

		if (clockBlinker.ringing && dreamingStarted)
		{
			barColor = alarmSliderColor;
			scale = 1.2f;
		}

		depthOfSleepSlider.transform.localScale = Vector3.Lerp(depthOfSleepSlider.transform.localScale, Vector3.one * scale, Time.deltaTime * 5.0f);
		depthOfSleepBar.color = barColor;
		depthOfSleepText.color = barColor;

		barColor = normalSliderColor;
		scale = 1.0f;

		if (Time.time < restSlideYellowUntilTime)
		{
			barColor = yellowColor;
			scale = 1.2f;
		}

		restBar.color = barColor;
		restText.color = barColor;
		restSlider.transform.localScale = Vector3.Lerp(restSlider.transform.localScale, Vector3.one * scale, Time.deltaTime * 5.0f);
	}

	public void StartDreaming()
	{
		dreamingStarted = true;
		dreamBounds = dreamBoundsCollider.bounds;
	}

	private void Awake()
	{
		endResultRested = false;
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
		float decay = (ringing ? alarmDepthRate : depthDecay) * Time.deltaTime;
		depthOfSleep += decay;
		restedness += decay;

		restedness = Mathf.Clamp01(restedness);
		depthOfSleep = Mathf.Clamp01(depthOfSleep);

		RefreshUI();

		dreamLevel = Mathf.Lerp(dreamLevel, ringing ? 0.0f : 1.0f, 1.0f * Time.deltaTime);
		float dreamLevelSquared = dreamLevel * dreamLevel;

		//maskParticleSystem.transform.localScale = Vector3.one * dreamLevel;
		maskParticleSystemEmission.rateOverTime = fullEmissionRate * dreamLevelSquared;

		dreamMusic.volume = dreamLevel * 0.25f;
		dreamMusic.pitch = Mathf.Sqrt(dreamLevel);

		dreamRunController.controllable = !ringing;

		if (!ringing)
		{
			// add bones
			if (Time.time > nextBoneTime)
			{
				nextBoneTime = Time.time + Random.Range(boneInterval.x, boneInterval.y);
				AddBone();
			}

			// move bones
			for (int i = bones.Count - 1; i >= 0; i--)
			{
				DreamBone bone = bones[i];
				Vector3 pos = bone.transform.position;
				pos.z -= Time.deltaTime;
				bone.transform.position = pos;

				bone.transform.Rotate(Vector3.forward * Time.deltaTime * 60.0f, Space.Self);

				// remove if reached bounds
				if (pos.z < dreamBounds.min.z)
				{
					bones.Remove(bone);
					Destroy(bone.gameObject);
				}
			}
		}


		if (restedness == 1.0f)
		{
			Debug.Log("End level: rested");
			dreamingStarted = false;
			endResultRested = true;
			endRestedEvent.Invoke();
		}
		else if (depthOfSleep == 0.0f)
		{
			Debug.Log("End level: tired");
			dreamingStarted = false;
			endResultRested = false;
			endUnrestedEvent.Invoke();
		}

	}

	public void LoadTwoWolves()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("TwoWolves");
	}


	public void AddBone()
	{
		Vector3 pos = new Vector3(
			Random.Range(dreamBounds.min.x, dreamBounds.max.x),
			Random.Range(dreamBounds.min.y, dreamBounds.min.y + 1.5f),
			dreamBounds.max.z + 5.0f
			);

		DreamBone bone = Instantiate<DreamBone>(bonePrefab, pos, Quaternion.LookRotation(Random.onUnitSphere));
		bone.dreamLevelController = this;
		bones.Add(bone);
	}

	public void CatchBone(DreamBone bone)
	{
		bones.Remove(bone);


		AudioSource.PlayClipAtPoint(bonePickupAudio.randomClip, bone.transform.position, 0.5f);
		//bonePickupAudio.PlayRandom(bone.transform.position);

		dreamGrabParticleSystem.transform.position = bone.transform.position;
		dreamGrabParticleSystem.Emit(50);

		Destroy(bone.gameObject);
		restedness += restednessPerBone;

		restSlideYellowUntilTime = Time.time + 0.5f;

		RefreshUI();
	}
}
