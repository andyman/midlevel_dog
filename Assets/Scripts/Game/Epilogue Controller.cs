using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueController : MonoBehaviour
{
	public void OpenIDumpling()
	{
		Application.OpenURL("https://idumpling.com");
	}
	public void RestartGame()
	{
		UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("Title");
	}
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}
}
