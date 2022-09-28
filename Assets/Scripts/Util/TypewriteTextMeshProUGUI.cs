using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Events;

// attach to UI Text component (with the full text already there)

[System.Serializable]
[RequireComponent(typeof(TextMeshProUGUI))]
public class TypewriteTextMeshProUGUI : MonoBehaviour
{

	TextMeshProUGUI txt;
	string story;
	public float keyDelay = 0.05f;
	private bool _done = false;

	public UnityEvent doneEvent;
	public AudioSet keySounds;

	public bool skippable = true;

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

	public void Update()
	{
		if (skippable && (Input.GetButtonDown("Jump") || Input.GetButtonDown("Fire1")))
		{
			_done = true;
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

		float startTime = Time.unscaledTime;
		foreach (char c in story)
		{
			if (_done) break;
			txt.text += c;
			if (c != ' ' && c != '\n')
			{
				if (keySounds != null)
				{
					keySounds.PlayRandom(camTran.position + Vector3.forward);
				}
			}
			yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			if (_done) break;
			if (c == '\n' || c == '.')
			{
				yield return new WaitForSeconds(keyDelay * Random.Range(0.8f, 1.2f));
			}
			if (_done) break;

		}

		_done = true;
		txt.text = story;
		doneEvent.Invoke();
		float endTime = Time.unscaledTime;

		float duration = endTime - startTime;
		//Debug.Log("Duration: " + duration);
	}

}