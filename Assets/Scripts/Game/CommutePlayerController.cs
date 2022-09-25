using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommutePlayerController : MonoBehaviour
{
	public Animator anim;

	public float stickOut = 0.0f;

	// Start is called before the first frame update
	void Start()
	{
		anim.SetBool("sitting", true);
		anim.SetBool("grounded", true);
		anim.SetLayerWeight(5, 1.0f);
	}

	// Update is called once per frame
	void Update()
	{
		float pressed = Input.GetButton("Jump") ? 1.0f : 0.0f;
		stickOut = Mathf.Lerp(stickOut, pressed, Time.deltaTime * 5.0f);

		anim.SetFloat("stickout", stickOut);

	}
}
