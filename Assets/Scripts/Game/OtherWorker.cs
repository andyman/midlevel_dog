using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherWorker : MonoBehaviour
{
	public Animator anim;

	public GameObject bowTie;
	public GameObject tie;

	public Material[] otherDogMaterials;

	public SkinnedMeshRenderer altDogRenderer;

	public SkinnedMeshRenderer dogRenderer;

	// Start is called before the first frame update
	void Start()
	{
		if (Random.value > 0.5f)
		{
			altDogRenderer.gameObject.SetActive(true);
			dogRenderer.gameObject.SetActive(false);
			dogRenderer = altDogRenderer;
		}

		dogRenderer.sharedMaterial = otherDogMaterials[Random.Range(0, otherDogMaterials.Length)];

	}


	//// Update is called once per frame
	//void Update()
	//{

	//}
}
