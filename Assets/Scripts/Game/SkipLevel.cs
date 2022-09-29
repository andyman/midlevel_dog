using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipLevel : MonoBehaviour
{
	public int nextScene = 0;
	private bool pressed = false;

	// Update is called once per frame
	void Update()
	{
		if (!pressed && Input.GetKeyDown(KeyCode.Slash))
		{
			pressed = true;
			Time.timeScale = 1.0f;
			SceneManager.LoadScene(nextScene);
		}
	}
}
