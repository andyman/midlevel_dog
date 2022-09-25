using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherCar : MonoBehaviour
{
	public Material[] otherCarMaterials;
	public Material[] otherDogMaterials;

	public MeshRenderer altDogRenderer;

	public MeshRenderer carRenderer;
	public MeshRenderer dogRenderer;

	// Start is called before the first frame update
	void Start()
	{
		if (Random.value > 0.5f)
		{
			altDogRenderer.gameObject.SetActive(true);
			dogRenderer.gameObject.SetActive(false);
			dogRenderer = altDogRenderer;
		}

		Material[] mats = carRenderer.sharedMaterials;
		mats[0] = otherCarMaterials[Random.Range(0, otherCarMaterials.Length)];
		carRenderer.sharedMaterials = mats;

		dogRenderer.sharedMaterial = otherDogMaterials[Random.Range(0, otherDogMaterials.Length)];

	}

}
