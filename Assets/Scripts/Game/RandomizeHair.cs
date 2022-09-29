using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class RandomizeHair : MonoBehaviour {
	public Gradient colorGradient;

	SkinnedMeshRenderer skinnedMeshRenderer;

	void Awake() {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		Material mat = skinnedMeshRenderer.material; // this creates a new materia
		mat.color = colorGradient.Evaluate (Random.value);
		skinnedMeshRenderer.SetBlendShapeWeight (0, Random.Range (-20.0f, 120.0f));
		skinnedMeshRenderer.SetBlendShapeWeight (1, Random.Range (-20.0f, 120.0f));
		if (Random.value > 0.5f) {
			Vector3 localScale = transform.localScale;
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
