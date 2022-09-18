using UnityEngine;
using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;

[DefaultExecutionOrder(-1000)]
public class ProcAudioSource : MonoBehaviour
{
	public static ProcAudioSource instance;
	public AudioSource audioSourcePrefab;

	private Stack<AudioSource> pool = new Stack<AudioSource>();
	void OnEnable()
	{
		instance = this;
	}
	void OnDisable()
	{
		if (instance == this)
		{
			instance = null;
		}
	}
	public AudioSource AudioSourceAtPosition(AudioClip clip, Vector3 position, float volume = 1.0f, float pitch = 1.0f)
	{
		AudioSource audioSource = null;
		if (pool.Count > 0)
		{
			//			Debug.Log("Reusing AudioSource");
			audioSource = pool.Pop();
			audioSource.transform.position = position;
			audioSource.gameObject.SetActive(true);
			audioSource.transform.parent = transform;
		}
		else
		{
			//Debug.Log("Creating new AudioSource");
			audioSource = Instantiate(audioSourcePrefab, position, Quaternion.identity) as AudioSource;
			audioSource.transform.parent = transform;
		}

		audioSource.clip = clip;
		audioSource.volume = volume;
		audioSource.pitch = pitch;
		//		audioSource.spatialBlend = 1.0f;

		return audioSource;
	}

	public void RecycleAudioSource(AudioSource audioSource, float delay)
	{
		StartCoroutine(RecycleCoroutine(audioSource, delay));
		//		// if more time then pool it. For now, don't worry about it.
		//Destroy(audioSource.gameObject, delay);
	}

	public IEnumerator RecycleCoroutine(AudioSource audioSource, float delay)
	{
		yield return new WaitForSeconds(delay);
		if (audioSource != null)
		{
			audioSource.transform.parent = transform;
			audioSource.gameObject.SetActive(false);
			pool.Push(audioSource);
		}
	}
	public AudioSource PlayOneShot(AudioClip clip, Vector3 position, float volume = 1.0f, float pitch = 1.0f)
	{
		AudioSource source = AudioSourceAtPosition(clip, position, volume, pitch);
		source.Play();
		RecycleAudioSource(source, clip.length * (1.0f / pitch));
		return source;
	}
	public static void Play(AudioClip clip, Vector3 position, float volume = 1.0f, float pitch = 1.0f)
	{
		instance.PlayOneShot(clip, position, volume, pitch);
	}
}
