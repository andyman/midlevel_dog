using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scent : MonoBehaviour
{
	public bool hooked = false;
	public Rigidbody rb;

	public ScentCollector sniffer = null;

	public bool goodScent = true;

	// Start is called before the first frame update
	void Start()
	{

	}

	public void Hook(ScentCollector collector)
	{
		hooked = true;
		sniffer = collector;
	}

	// Update is called once per frame
	void Update()
	{
		if (hooked && sniffer.sniffing)
		{
			Vector3 targetPos = sniffer.transform.position;
			Vector3 pos = transform.position;
			Vector3 diff = pos - targetPos;

			float dist = diff.magnitude;

			if (dist > 3.0f)
			{
				rb.velocity = new Vector3(0f, 0f, -5f);
			}
			else
			{
				Vector3 dir = diff.normalized;
				rb.velocity = Vector3.Lerp(rb.velocity, -dir * 10.0f, Time.deltaTime * 5.0f);

				if (diff.magnitude < 0.1f)
				{
					sniffer.CollectScent(this);
					Destroy(gameObject);
				}

			}
		}
		else
		{
			rb.velocity = new Vector3(0f, 0f, -5f);
		}

		if (transform.position.z < -40.0f)
		{
			Destroy(gameObject);
		}
	}
}
