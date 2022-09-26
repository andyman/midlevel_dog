using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSnooze : MonoBehaviour
{
	int clicks = 0;
	public void Load()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(1);


	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

		if (Input.GetButtonDown("Fire1"))
		{

			Load();
		}
	}
}
