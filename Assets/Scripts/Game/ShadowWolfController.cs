using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowWolfController : MonoBehaviour
{
	public Animator anim;

	// Start is called before the first frame update
	void Start()
	{
		anim.SetBool("grounded", true);
		anim.SetBool("sitting", false);
	}

	// Update is called once per frame
	void Update()
	{
	}
}
