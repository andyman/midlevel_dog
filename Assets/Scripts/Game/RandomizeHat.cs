using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class RandomizeHat : MonoBehaviour {

	SkinnedMeshRenderer skinnedMeshRenderer;

	void Awake() {
		skinnedMeshRenderer = GetComponent<SkinnedMeshRenderer> ();
		Material mat = skinnedMeshRenderer.material; // this creates a new materia
		mat.color = Random.ColorHSV(0.0f, 1.0f, 0.0f, 0.5f, 0.0f, 1.0f);

		skinnedMeshRenderer.SetBlendShapeWeight (0, Random.Range (-50.0f, 150.0f));
		skinnedMeshRenderer.SetBlendShapeWeight (1, Random.Range (-50.0f, 150.0f));
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
