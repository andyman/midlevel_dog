using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class AudioSet : ScriptableObject
{
	public AudioClip[] clips;
	public Vector2 volumeRange = Vector2.one;
	public Vector2 pitchRange = Vector2.one;

	public AudioClip randomClip { get { return clips[Random.Range(0, clips.Length)]; } }

	public AudioSource PlayRandom(Vector3 pos, float minVolume, float maxVolume, float minPitch, float maxPitch)
	{
		return ProcAudioSource.instance.PlayOneShot(randomClip, pos, Random.Range(minVolume, maxVolume), Random.Range(minPitch, maxPitch));
	}

	public AudioSource PlayRandom(Vector3 pos)
	{
		return PlayRandom(pos, volumeRange.x, volumeRange.y, pitchRange.x, pitchRange.y);
	}
}
