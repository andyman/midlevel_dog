using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DreamBone : MonoBehaviour
{
	public DreamLevelController dreamLevelController;
	public LineRenderer line;

	// Start is called before the first frame update
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{
		line.SetPosition(0, transform.position);
		line.SetPosition(1, transform.position + Vector3.down * 5.0f);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Rigidbody rb = collision.rigidbody;
		if (rb.GetComponent<DreamRunController>() != null)
		{
			dreamLevelController.CatchBone(this);
		}
	}
}
