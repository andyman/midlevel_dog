using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaverAttackTrigger : MonoBehaviour
{
	public float damagePerSecond = 1.0f;

	public float attackUntilTime;
	public Transform particlePoint;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerStay(Collider other)
	{
		if (Time.time > attackUntilTime) return;

		WolfInside wolf = other.GetComponent<WolfInside>();
		if (wolf == null) return;

		wolf.Damage(damagePerSecond * Time.deltaTime, particlePoint.position);
	}
}
