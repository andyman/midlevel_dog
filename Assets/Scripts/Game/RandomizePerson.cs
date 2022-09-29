using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TextureOption
{
	public Texture2D diffuseAlpha;
}

public class RandomizePerson : MonoBehaviour
{

	public SkinnedMeshRenderer skinnedMeshRenderer;
	public GameObject[] maleHair;
	public GameObject[] femaleHair;

	public Gradient skinColorGradient;

	public TextureOption[] pants;
	public TextureOption[] tops;

	public TextureOption[] tattoos;

	private SkinnedMeshRenderer censor;
	public SkinnedMeshRenderer maleCensor;
	public SkinnedMeshRenderer femaleCensor;

	public GameObject hair = null;
	private float gender = 0.0f;
	private float obesity = 0.0f;

	private Material material;
	public bool nude = false;

	public Material GetMaterial()
	{
		return material;
	}
	void Awake()
	{
		transform.localScale = Vector3.one * Random.Range(0.5f, 1.0f);
		gender = Random.value > 0.5 ? Random.Range(0.5f, 0.75f) : Random.Range(0.0f, 0.25f);
		obesity = Random.Range(0.5f, 1.0f);

		float genderBlendShapeWeight = gender * 100.0f;
		float obesityBlendShapeWeight = (obesity * 2.0f - 1.0f) * 100.0f;
		float skinnyBlendShapeWeight = (-obesity * 2.0f + 1.0f) * 100.0f;

		censor = gender < 0.5f ? maleCensor : femaleCensor;
		skinnedMeshRenderer.SetBlendShapeWeight(1, genderBlendShapeWeight);
		skinnedMeshRenderer.SetBlendShapeWeight(0, obesityBlendShapeWeight);
		skinnedMeshRenderer.SetBlendShapeWeight(2, skinnyBlendShapeWeight);

		censor.SetBlendShapeWeight(1, genderBlendShapeWeight);
		censor.SetBlendShapeWeight(0, obesityBlendShapeWeight);
		censor.SetBlendShapeWeight(2, skinnyBlendShapeWeight);

		material = skinnedMeshRenderer.material;
		material.color = skinColorGradient.Evaluate(Random.value);
		transform.localScale = transform.localScale * Random.Range(1.0f, 1.2f);

		// shirt and pants
		TextureOption pant = pants[Random.Range(0, pants.Length)];
		TextureOption shirt = tops[Random.Range(0, tops.Length)];

		float minSaturation = 0.0f;
		float maxSaturation = 0.75f;
		float minBrightness = 0.0f;
		float maxBrightness = 1.0f;

		material.SetTexture("_Tex2", pant.diffuseAlpha);
		//		material.SetTexture ("_Bump2", pant.normal);
		material.SetColor("_Tint2r", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));
		material.SetColor("_Tint2g", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));
		material.SetColor("_Tint2b", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));

		material.SetTexture("_Tex3", shirt.diffuseAlpha);
		//		material.SetTexture ("_Bump3", shirt.normal);
		material.SetColor("_Tint3r", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));
		material.SetColor("_Tint3g", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));
		material.SetColor("_Tint3b", Random.ColorHSV(0.0f, 1.0f, minSaturation, maxSaturation, minBrightness, maxBrightness));

		nude = false;
	}

	[ContextMenu("Make Nude")]
	public void MakeNude()
	{
		material.SetTexture("_Tex2", null);
		material.SetTexture("_Bump2", null);
		material.SetColor("_Tint2r", Random.ColorHSV());
		material.SetColor("_Tint2g", Random.ColorHSV());
		material.SetColor("_Tint2b", Random.ColorHSV());

		material.SetTexture("_Tex3", null);
		material.SetTexture("_Bump3", null);
		material.SetColor("_Tint3r", Random.ColorHSV());
		material.SetColor("_Tint3g", Random.ColorHSV());
		material.SetColor("_Tint3b", Random.ColorHSV());

		// censoring, set to false to remove
		if (true)
		{
			censor.gameObject.SetActive(true);

			// if it's a hat, remove it and give a different hair
			if (hair.GetComponent<RandomizeHat>() != null)
			{
				hair.SetActive(false);
				if (gender < 0.5f)
				{
					hair = maleHair[Random.Range(0, maleHair.Length - 2)];
				}
				else
				{
					hair = femaleHair[Random.Range(0, femaleHair.Length - 2)];
				}

				hair.SetActive(true);
			}
		}
		nude = true;

		// tattoos
		tattooCount = Random.Range(-3, 4);
		if (tattooCount > 0)
		{
			int actualTattooCount = 0;

			for (int i = 0; i < tattooCount; i++)
			{
				string suffix = (i + 2).ToString();
				int tattooIndex = Random.Range(0, tattoos.Length);
				TextureOption tattoo = tattoos[tattooIndex];

				if (tattoo == null)
				{
					//nada
				}
				else
				{
					material.SetTexture("_Tex" + suffix, tattoo.diffuseAlpha);
					material.SetTexture("_Bump" + suffix, null);
					material.SetColor("_Tint" + suffix + "r", Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 0.0f, 0.5f));
					material.SetColor("_Tint" + suffix + "g", Random.ColorHSV());
					material.SetColor("_Tint" + suffix + "b", Random.ColorHSV());
					tattoos[tattooIndex] = null;
					actualTattooCount++;
				}
			}

			tattooCount = actualTattooCount;
		}
	}
	public int tattooCount = 0;

	// Use this for initialization
	void Start()
	{

		PickHair();
	}

	void PickHair()
	{
		// male
		if (gender < 0.5f)
		{
			hair = maleHair[Random.Range(0, maleHair.Length)];
		}
		// female
		else
		{
			hair = femaleHair[Random.Range(0, femaleHair.Length)];
		}
		hair.SetActive(true);
	}

	// Update is called once per frame
	void Update()
	{

	}
}
