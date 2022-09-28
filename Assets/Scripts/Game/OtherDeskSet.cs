using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherDeskSet : MonoBehaviour
{
	public KeyboardMouseHandsController kb;
	public GameObject virtualCam;
	public Interactable interactable;
	public OtherWorker otherWorker;
	public AudioSet yelp;

	[Multiline]
	public string pleadMessage = "Please don't fire me!";
	public OfficeLevelController officeLevelController;

	public bool fired = false;
	public bool firable = true;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (officeLevelController == null) return;

		if (officeLevelController.letterWritten || officeLevelController.firings >= officeLevelController.maxFirings)
		{
			interactable.enabled = false;
		}
	}

	public void StartFiring()
	{
		if (!firable || fired) return;

		officeLevelController.StartFiring(this);
		virtualCam.SetActive(true);
		interactable.enabled = false;
		interactable.gameObject.SetActive(false);

	}

	public void CancelFiring()
	{
		virtualCam.SetActive(false);
		interactable.enabled = true;
		interactable.gameObject.SetActive(true);
	}

	public void Fire()
	{
		kb.enabled = false;
		virtualCam.SetActive(false);
		interactable.enabled = false;
		interactable.gameObject.SetActive(false);
		yelp.PlayRandom(transform.position);
		Destroy(otherWorker.gameObject, 3.0f);
		otherWorker.gameObject.SetActive(false);
	}
}
