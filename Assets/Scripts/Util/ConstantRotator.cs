using UnityEngine;
using System.Collections;

public class ConstantRotator : MonoBehaviour {
    public Vector3 angularVelocity;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        
        transform.rotation = transform.rotation * Quaternion.Euler(angularVelocity * Time.deltaTime);
	}


}
