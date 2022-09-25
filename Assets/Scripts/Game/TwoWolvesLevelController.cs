using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TwoWolvesLevelController : MonoBehaviour
{
	static public bool bowTieLived = false;

	public PlayerWalkController player;
	public Animator playerAnimator;

	public GameObject equippedCleaver;
	public Interactable propCleaver;
	public AudioSet cleaverEquipSound;
	public AudioSet swordSwipeSound;

	private bool cleaverEquipped = false;
	private float nextAttackTime;
	private CleaverAttackTrigger cleaverAttackTrigger;

	public TwoWolvesBone bonePrefab;

	public List<TwoWolvesBone> bones = new List<TwoWolvesBone>();

	public BoxCollider levelBounds;
	public int boneStartCount = 10;

	public UnityEvent wolfDied;

	public UnityEvent heartEquipped;
	public GameObject equippedBlueHeart;
	public GameObject equippedRedHeart;

	public GameObject redHeartProp;
	public GameObject blueHeartProp;

	public WolfInside whiteWolf;
	public WolfInside blackWolf;

	// Start is called before the first frame update
	void Start()
	{
		bowTieLived = false;

		Bounds bounds = levelBounds.bounds;

		for (int i = 0; i < boneStartCount; i++)
		{
			Vector3 pos = new Vector3(
				Random.Range(bounds.min.x, bounds.max.x),
				0.5f,
				Random.Range(bounds.min.z, bounds.max.z)
				);

			TwoWolvesBone bone = Instantiate<TwoWolvesBone>(bonePrefab, pos, Quaternion.LookRotation(Random.onUnitSphere));
			bones.Add(bone);
		}

		cleaverAttackTrigger = equippedCleaver.GetComponent<CleaverAttackTrigger>();
	}

	// Update is called once per frame
	void Update()
	{
		if (cleaverEquipped)
		{
			if (Input.GetButtonDown("Jump") && Time.time > nextAttackTime)
			{
				playerAnimator.SetTrigger("cleaverAttack");
				swordSwipeSound.PlayRandom(equippedCleaver.transform.position);

				nextAttackTime = Time.time + 0.5f;
				cleaverAttackTrigger.attackUntilTime = nextAttackTime;
			}
		}
	}

	public void CleaverPickedUp()
	{
		playerAnimator.SetLayerWeight(4, 0.9f);
		equippedCleaver.SetActive(true);
		propCleaver.gameObject.SetActive(false);
		cleaverEquipSound.PlayRandom(propCleaver.transform.position);

		cleaverEquipped = true;
	}

	public void UnequipCleaver()
	{
		cleaverEquipped = false;
		equippedCleaver.gameObject.SetActive(false);
		playerAnimator.SetLayerWeight(4, 0.0f);

	}


	private void WolfDied()
	{
		Debug.Log("wolf died");
		UnequipCleaver();
		wolfDied.Invoke();
	}
	public void WhiteWolfDied()
	{
		Debug.Log("White Wolf Died");
		bowTieLived = true;
		WolfDied();
		blueHeartProp.transform.position = whiteWolf.transform.position + Vector3.up * 0.25f;
		blueHeartProp.gameObject.SetActive(true);
	}

	public void BlackWolfDied()
	{
		Debug.Log("Black wolf died");
		bowTieLived = false;
		WolfDied();
		redHeartProp.transform.position = blackWolf.transform.position + Vector3.up * 0.25f;
		redHeartProp.gameObject.SetActive(true);
	}

	public void BlueHeartGrabbed()
	{
		equippedBlueHeart.SetActive(true);
		playerAnimator.SetLayerWeight(4, 0.9f);
	}

	public void RedHeartGrabbed()
	{
		equippedRedHeart.SetActive(true);
		playerAnimator.SetLayerWeight(4, 0.9f);

	}

	public Material redStewMaterial;
	public Material blueStewMaterial;
	public MeshRenderer stewRenderer;
	public Transform stewParticlePosition;
	public MeshRenderer stewInBowl;

	public bool feedingTime;

	public void DropHeartInStew()
	{
		Material[] stewMaterials = stewRenderer.sharedMaterials;

		if (equippedRedHeart.activeSelf)
		{
			// turn stew red
			equippedRedHeart.SetActive(false);
			stewMaterials[2] = redStewMaterial;
			blackWolf.bloodParticleSystem.transform.position = stewParticlePosition.position;
			blackWolf.bloodParticleSystem.Emit(20);
			stewInBowl.sharedMaterial = redStewMaterial;
		}

		if (equippedBlueHeart.activeSelf)
		{
			// turn stew blue
			equippedBlueHeart.SetActive(false);
			stewMaterials[2] = blueStewMaterial;
			whiteWolf.bloodParticleSystem.transform.position = stewParticlePosition.position;
			whiteWolf.bloodParticleSystem.Emit(20);
			stewInBowl.sharedMaterial = blueStewMaterial;
		}
		stewRenderer.sharedMaterials = stewMaterials;

		playerAnimator.SetLayerWeight(4, 0.0f);
		afterDropHeartInStewEvent.Invoke();
	}
	public UnityEvent afterDropHeartInStewEvent;

	public void FillBowlFeedWolf()
	{
		feedingTime = true;
		blackWolf.Feed();
		whiteWolf.Feed();

		if (!blackWolf.dead)
		{
			blackWolf.GetComponent<Interactable>().enabled = true;
		}
		else if (!whiteWolf.dead)
		{
			whiteWolf.GetComponent<Interactable>().enabled = true;
		}

	}

	public UnityEvent afterFeedWolf;
	public void FeedWolf()
	{

		if (whiteWolf.dead)
		{
			bowTie.SetActive(true);
		}
		else if (blackWolf.dead)
		{
			neckTie.SetActive(true);
		}

		afterFeedWolf.Invoke();

	}

	public GameObject bowTie;
	public GameObject neckTie;

	public void LoadNextLevel()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Commute");
	}
}
