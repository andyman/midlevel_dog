using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanineResourcesWorker : MonoBehaviour
{
	public Animator anim;

	// Start is called before the first frame update
	IEnumerator Start()
	{
		anim.SetBool("grounded", true);
		anim.SetBool("sitting", false);
		anim.SetLayerWeight(4, 1.0f);

		while (true)
		{
			yield return new WaitForSeconds(Random.Range(1f, 2.0f));
			anim.SetTrigger("cleaverAttack");
		}
	}

}
