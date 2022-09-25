using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScentCollector : MonoBehaviour
{
	public CommuteLevelController levelController;

	public CommutePlayerController player;

	public SphereCollider myCollider;
	public bool sniffing = false;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		sniffing = player.stickOut > 0.1f;
		GetComponent<Collider>().enabled = sniffing;
	}

	private void OnTriggerEnter(Collider other)
	{
		Scent scent = other.GetComponent<Scent>();
		if (scent == null) return;
		scent.Hook(this);

	}

	private void OnTriggerExit(Collider other)
	{
		Scent scent = other.GetComponent<Scent>();
		if (scent == null) return;

		scent.hooked = false;
	}

	public AudioSet goodScentCollectedSound;
	public AudioSet badScentCollectedSound;

	public void CollectScent(Scent scent)
	{
		if (scent.goodScent)
		{
			goodScentCollectedSound.PlayRandom(scent.transform.position);
			levelController.GoodScentCollected();
		}
		else
		{
			badScentCollectedSound.PlayRandom(scent.transform.position);
			levelController.BadScentCollected();
		}
	}
}
