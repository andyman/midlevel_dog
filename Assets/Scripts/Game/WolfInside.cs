using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WolfInside : MonoBehaviour
{
	public Rigidbody rb;
	public Animator anim;

	public ParticleSystem bloodParticleSystem;

	public float particlesPerLife = 1.0f;

	public float life = 100.0f;
	private float emissionPool = 0.0f;
	public bool dead = false;
	public int deathEmissionCount = 100;

	public bool hasDestination = false;
	private float nextStateTime = 0.0f;
	public Vector3 destinationPoint;

	public TwoWolvesLevelController levelController;

	public float fleeSpeed = 3.0f;
	public UnityEvent dieEvent;
	public AudioSet hurtSound;
	float nextHurtSoundTime;
	public AudioSet dieSound;

	public bool feedingTime = false;

	// Start is called before the first frame update
	void Start()
	{
		nextStateTime = Time.time + 10.0f;
	}

	// Update is called once per frame
	void Update()
	{
		if (dead) return;

		if (feedingTime)
		{
			destinationPoint = levelController.stewInBowl.transform.position;
			hasDestination = true;
		}
		else if (Time.time > nextStateTime)
		{
			//bool contains = false;
			//int tries = 0;
			//do
			//{
			//	Vector2 circle = Random.insideUnitCircle * 10.0f;

			//	destinationPoint = transform.position + new Vector3(circle.x, 0.0f, circle.y);
			//	contains = levelController.levelBounds.bounds.Contains(destinationPoint);
			//} while (!contains && tries < 8);


			//hasDestination = contains;
			//if (contains)
			//{
			//	nextStateTime = Time.time + Random.Range(5.0f, 10.0f);

			//}

			// get to another bone
			Vector3 dest = levelController.bones[Random.Range(0, levelController.bones.Count)].transform.position;
			dest.y = 0.0f;
			destinationPoint = dest;
			nextStateTime = Time.time + Random.Range(5.0f, 10.0f);
		}
	}
	private void FixedUpdate()
	{
		if (dead) return;

		if (hasDestination && !rb.isKinematic)
		{
			Vector3 diff = destinationPoint - transform.position;
			diff.y = 0.0f;
			if (diff.magnitude > 0.1f)
			{
				rb.velocity = diff.normalized * fleeSpeed;
			}
			else
			{
				hasDestination = false;
			}
		}



		Vector3 v = rb.velocity;
		v.y = 0.0f;
		if (v.magnitude > 0.01f)
		{
			v.Normalize();
			Quaternion targetRot = Quaternion.LookRotation(v);
			Quaternion newRot = Quaternion.Slerp(rb.rotation, targetRot, Time.deltaTime * 10.0f);
			transform.rotation = newRot;
			//rb.MoveRotation(newRot);
		}
	}

	public void Damage(float amount, Vector3 pos)
	{
		//Debug.Log("Damage");
		emissionPool += amount * particlesPerLife;

		if (emissionPool >= 1.0f)
		{
			int emitCount = Mathf.FloorToInt(emissionPool);
			emissionPool -= emitCount;
			bloodParticleSystem.transform.position = pos;
			bloodParticleSystem.Emit(emitCount);
		}

		if (!dead)
		{
			life -= amount;

			if (Time.time > nextHurtSoundTime)
			{
				hurtSound.PlayRandom(transform.position);
				nextHurtSoundTime = Time.time + Random.Range(1.0f, 2.0f);
			}

			if (life <= 0.0f)
			{
				Die();
			}
			else
			{
				// flee!!!
				nextStateTime = Time.time + 5.0f;
				hasDestination = true;

				if (levelController.bones.Count > 0)
				{
					// get to another bone
					Vector3 dest = levelController.bones[Random.Range(0, levelController.bones.Count)].transform.position;
					dest.y = 0.0f;
					destinationPoint = dest;
				}
			}
		}
	}

	public void Die()
	{
		dieSound.PlayRandom(transform.position);
		dead = true;
		bloodParticleSystem.Emit(deathEmissionCount);
		anim.SetBool("dead", true);
		rb.isKinematic = true;
		rb.GetComponent<Collider>().enabled = false;

		dieEvent.Invoke();
	}

	public void Feed()
	{
		feedingTime = true;
	}
}
