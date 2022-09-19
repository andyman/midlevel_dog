using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

// attach to UI Text component (with the full text already there)

[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriteTextMeshProUGUI : MonoBehaviour
{

	TextMeshProUGUI txt;
	string story;
	public float keyDelay = 0.1f;
	private bool _done = false;

	public UnityEvent doneEvent;
	public AudioSet keySounds;

	void Awake()
	{
		txt = GetComponent<TextMeshProUGUI>();
	}

	public bool done
	{
		get
		{
			return _done;
		}
	}

	private void OnEnable()
	{
		story = txt.text;
		StartCoroutine(Run());
	}

	private void OnDisable()
	{
		StopAllCoroutines();
	}
	IEnumerator Run()
	{
		txt.text = "";
		Transform camTran = Camera.main.transform;

		float startTime = Time.time;
		foreach (char c in story)
		{
			txt.text += c;
			if (c != ' ' && c != '\n')
			{
				keySounds.PlayRandom(camTran.position + Vector3.forward);
			}
			yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			if (c == '\n' || c == '.')
			{
				yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			}
		}

		_done = true;
		doneEvent.Invoke();
		float endTime = Time.time;

		float duration = endTime - startTime;
		//Debug.Log("Duration: " + duration);
	}

}