using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class CommuteLevelController : MonoBehaviour
{
	public CommutePlayerController player;

	public bool spawning = false;

	public float health = 100.0f;
	public float scents = 0; // 0 to 20
	public float maxScents = 50.0f;
	public Slider healthSlider;
	public Slider scentsSlider;

	private float healthSliderBulgeUntilTime;
	private float scentsSliderBulgeUntilTime;

	private bool switchedView = false;

	public UnityEvent minScentsObtainedEvent;

	public BoxCollider scentSpawnZone;

	public float chanceOfGoodScent = 1.0f;
	private float nextSpawnTime = 0.0f;
	public Vector2 spawnInterval = new Vector2(1.0f, 3.0f);

	public Scent goodScentPrefab;
	public Scent badScentPrefab;

	private bool levelDone = false;

	public void RefreshUI()
	{
		healthSlider.value = health / 100.0f;
		scentsSlider.value = scents / maxScents;

		healthSlider.transform.localScale = (Time.time > healthSliderBulgeUntilTime ? 1.0f : 1.2f) * Vector3.one;
		scentsSlider.transform.localScale = (Time.time > scentsSliderBulgeUntilTime ? 1.0f : 1.2f) * Vector3.one;
	}
	// Start is called before the first frame update
	void Start()
	{
		RefreshUI();
	}

	// Update is called once per frame
	void Update()
	{
		if (levelDone) return;

		if (!switchedView && scents >= 3.0f)
		{
			switchedView = true;
			minScentsObtainedEvent.Invoke();
			chanceOfGoodScent = 0.5f;
		}

		RefreshUI();

		if (spawning && Time.time > nextSpawnTime)
		{
			nextSpawnTime = Time.time + Random.Range(spawnInterval.x, spawnInterval.y);
			Bounds b = scentSpawnZone.bounds;
			Scent prefab = Random.value < chanceOfGoodScent ? goodScentPrefab : badScentPrefab;
			Instantiate<Scent>(prefab,
				new Vector3(
					Random.Range(b.min.x, b.max.x),
					Random.Range(b.min.y, b.max.y),
					Random.Range(b.min.z, b.max.z)
				),
				Quaternion.identity);
		}
	}

	public UnityEvent winEvent;

	public void GoodScentCollected()
	{
		if (levelDone) return;
		scents += 1.0f;

		if (scents >= 50)
		{
			levelDone = true;

			// win
			winEvent.Invoke();
		}
		scentsSliderBulgeUntilTime = Time.time + 0.2f;
	}

	public UnityEvent loseEvent;

	public void BadScentCollected()
	{
		if (levelDone) return;
		health -= 5.0f;

		if (health <= 0.0f)
		{
			levelDone = true;

			// die
			loseEvent.Invoke();
		}
		healthSliderBulgeUntilTime = Time.time + 0.2f;
	}
	public void StartSpawning()
	{
		spawning = true;
	}

	public void LoadNextLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Office");
	}
}
