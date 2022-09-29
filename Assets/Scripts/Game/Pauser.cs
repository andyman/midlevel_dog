using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
public class Pauser : MonoBehaviour
{

	public string pauseButton = "Cancel";
	public bool paused = false;

	public UnityEvent pauseEvent;
	public UnityEvent resumeEvent;

	private List<AudioSource> pausedAudioSources = new List<AudioSource>();

	private float oldTimeScale = 1.0f;

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetButtonDown(pauseButton))
		{
			if (paused)
			{
				Resume();
			}
			else
			{
				Pause();
			}
		}
	}

	public void Pause()
	{
		pauseEvent.Invoke();
		paused = true;
		oldTimeScale = Time.timeScale;

		Time.timeScale = 0.0f;

		AudioSource[] audioSources = GameObject.FindObjectsOfType<AudioSource>();
		pausedAudioSources.Clear();
		for (int i = 0; i < audioSources.Length; i++)
		{
			AudioSource a = audioSources[i];
			if (a.isActiveAndEnabled && a.isPlaying)
			{
				a.Pause();
				pausedAudioSources.Add(a);
			}
		}
	}

	public void Resume()
	{
		resumeEvent.Invoke();
		Time.timeScale = oldTimeScale;

		for (int i = 0; i < pausedAudioSources.Count; i++)
		{
			pausedAudioSources[i].UnPause();
		}

		paused = false;
	}

	public void ShowCursor()
	{
		Cursor.visible = true;
	}

	public void HideCursor()
	{
		Cursor.visible = false;
	}

	public void QuitApplication()
	{
		Application.Quit();
	}
	public void LoadScene(int sceneIndex)
	{
		Time.timeScale = 1.0f;
		SceneManager.LoadSceneAsync(sceneIndex);
	}

	public void OpenWebLink(string url)
	{
		Application.OpenURL(url);
	}

	public void RestartScene()
	{
		Time.timeScale = 1.0f;
		LoadScene(SceneManager.GetActiveScene().buildIndex);
	}
}
