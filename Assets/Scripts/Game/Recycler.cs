using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recycler : MonoBehaviour
{
	public BoxCollider respawnBlock;

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
		Bounds b = respawnBlock.bounds;
		other.transform.position = new Vector3(
			Random.Range(b.min.x, b.max.x),
			Random.Range(b.min.y, b.max.y),
			Random.Range(b.min.z, b.max.z)
			);
	}
}
