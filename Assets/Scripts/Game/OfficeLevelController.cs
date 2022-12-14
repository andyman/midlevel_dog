using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using TMPro;
using System.Text;
using Cinemachine;

public class OfficeLevelController : MonoBehaviour
{
	public GameObject sittingMidlevelDog;
	public PlayerWalkController playerWalker;

	public TextMeshProUGUI emailText;
	public TextMeshProUGUI mainDirective;

	public int firings = 0;
	public int maxFirings = 3;
	public bool letterWritten;

	public UnityEvent walkingModeEvent;
	public UnityEvent exitWalkingModeEvent;

	StringBuilder buf = new StringBuilder();
	public ParticleSystem employeeBloodParticleSystem;

	public Interactable letterInteractable;
	public CinemachineVirtualCamera followCam;

	public bool isWalkingMode = false;
	private bool isTypingLetter = false;

	public UnityEvent letterWrittenEvent;

	public AudioSource backgroundSound;
	public AudioSource sadMusic;

	public void TypingLetter()
	{
		isTypingLetter = true;
	}

	public void EndTypingLetter()
	{
		isTypingLetter = false;
	}
	public Color blueLightColor;

	public void LetterSent()
	{
		letterWritten = true;
		letterWrittenEvent.Invoke();
		RenderSettings.fogColor = blueLightColor;
		RenderSettings.ambientEquatorColor = blueLightColor;
		RenderSettings.ambientGroundColor = blueLightColor;
		RenderSettings.ambientSkyColor = blueLightColor;
		Time.timeScale = 0.5f;
		backgroundSound.pitch = 0.7f;
	}

	public void RestoreTimeScale()
	{
		Time.timeScale = 1.0f;
		backgroundSound.pitch = 1.0f;
	}
	public void LoadForestLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Forest");
	}
	public void LoadTitle()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");
	}

	public void RefreshDirective()
	{
		buf.Clear();

		if (letterWritten)
		{
			buf.Append("<color=#ff0>Message sent.</color>\n\nStylishly make your way to the exit.\nSavor this moment in slow motion.");
		}
		else if (firings < maxFirings)
		{
			int remainingFirings = maxFirings - firings;

			buf.Append("Fire ");
			buf.Append(remainingFirings);
			buf.Append(" team member");
			if (remainingFirings != 1)
			{
				buf.Append("s");
			}

			buf.Append(" or\n");
			buf.Append("type up your resignation letter\n(at your computer).");
		}
		else
		{
			buf.Append("Tell Barky Boss that you\ncompleted the firings.");
		}
		mainDirective.text = buf.ToString();
		buf.Clear();
	}

	public UnityEvent standBehindEvent;

	public void EnterStandBehindMode()
	{
		isWalkingMode = false;
		standBehindEvent.Invoke();
		playerWalker.controllable = false;
	}
	public void ActivateWalkingMode()
	{
		isWalkingMode = true;
		walkingModeEvent.Invoke();
		playerWalker.controllable = true;
	}

	public void DeactivateWalkingMode()
	{
		isWalkingMode = false;
		exitWalkingModeEvent.Invoke();
		playerWalker.controllable = false;
	}

	// Start is called before the first frame update
	void Start()
	{
		var transposer = followCam.GetCinemachineComponent<CinemachineTransposer>();
		transposer.m_FollowOffset = new Vector3(0.0f, 2.5f, -1.5f);

		if (TwoWolvesLevelController.bowTieLived)
		{
			emailText.text += "P.S. Nice fancy bowtie! I must be paying you too much.";
		}
		else
		{
			emailText.text += "P.S. What an nice necktie! Are you chasing for the vice lapdog position?";
		}
	}

	public AudioSet keyClickSet;
	// Update is called once per frame
	void Update()
	{
		if (isWalkingMode)
		{
			RefreshDirective();
		}

		if (isTypingLetter)
		{
			if (Input.anyKeyDown)
			{
				keyClickSet.PlayRandom(sittingMidlevelDog.transform.position);
			}
		}

		// firing mode
		if (firingMode && lastDeskSet != null)
		{
			if (Time.time >= firingStartTime && Time.time < firingEndTime)
			{
				firingCounter.text = "FIRING IN " + Mathf.CeilToInt(firingEndTime - Time.time).ToString();
				if (Input.GetButtonDown("Jump"))
				{
					CancelFiring(lastDeskSet);
				}
			}
			else
			{

				lastDeskSet.Fire();
				EmployeeFired(lastDeskSet);

				firingMode = false;
				firingModal.SetActive(false);
				lastDeskSet = null;
			}

		}
	}

	public AudioSet firingAudio;
	public void CancelFiring(OtherDeskSet otherDeskSet)
	{
		firingMode = false;
		otherDeskSet.CancelFiring();
		firingModal.SetActive(false);
		ActivateWalkingMode();
	}
	public void EmployeeFired(OtherDeskSet otherDeskSet)
	{
		firings++;
		//employeeBloodParticleSystem.transform.position = otherDeskSet.otherWorker.transform.position + Vector3.up * 0.5f;
		//employeeBloodParticleSystem.Emit(200);

		firingAudio.PlayRandom(playerWalker.transform.position);
		ActivateWalkingMode();
		if (firings >= maxFirings)
		{
			letterInteractable.enabled = false;
			maxFiringsEvent.Invoke();

			// flip the camera
			StartCoroutine(SpinFollowCam180());
		}
	}

	private IEnumerator SpinFollowCam180()
	{
		var transposer = followCam.GetCinemachineComponent<CinemachineTransposer>();
		Vector3 initialOffset = transposer.m_FollowOffset;

		float startTime = Time.time;
		float endTime = Time.time + 3.0f;

		Quaternion startRot = Quaternion.identity;
		Quaternion endRot = Quaternion.Euler(0.0f, 150.0f, 0.0f);

		while (Time.time < endTime)
		{
			yield return 0;

			float lerpy = Mathf.InverseLerp(startTime, endTime, Time.time);
			lerpy = Mathf.SmoothStep(0.0f, 1.0f, lerpy);

			transposer.m_FollowOffset = Quaternion.Slerp(startRot, endRot, lerpy) * initialOffset;
		}



	}
	public UnityEvent maxFiringsEvent;

	public GameObject firingModal;
	public TextMeshProUGUI firingPleadText;
	public TextMeshProUGUI firingCounter;

	private float firingStartTime;
	private float firingEndTime;
	private bool firingMode = true;
	private OtherDeskSet lastDeskSet;

	public void StartFiring(OtherDeskSet otherDeskSet)
	{
		DeactivateWalkingMode();
		firingStartTime = Time.time;
		firingEndTime = firingStartTime + 5.0f;
		firingMode = true;

		firingPleadText.text = '"' + otherDeskSet.pleadMessage + '"';
		firingModal.SetActive(true);
		firingCounter.text = "";
		lastDeskSet = otherDeskSet;

		if (!sadMusic.isPlaying)
		{
			sadMusic.Play();
		}
	}

	public void ShowMoreGames()
	{
		Application.OpenURL("https://idumpling.com");

	}
}
