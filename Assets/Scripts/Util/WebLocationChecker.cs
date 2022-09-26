using UnityEngine;
using System.Collections;
using System.Text;

public class WebLocationChecker : MonoBehaviour
{

	/** if it is a web build, then the domain must contain any
	 * one or more of these strings, or it will be redirected */
	public string[] domainMustContain;

	/** (optional) or fetch the domain list from this URL */
	public string domainListURL;

	/** this is where to redirect the webplayer/WebGL page if none of
	 * the strings in domainMustContain are found.
	 */
	public string redirectURL;

	/** (optional) game objects to deactivate while the domain checking is happening */
	public GameObject[] waitObjects;

	/** (optional) these are characters to split the domain list file, if it is being used */
	public char[] splitters;

	void Awake()
	{
#if UNITY_WEBPLAYER
		// deactivate all the wait objects first
		ActivateWaitObjects (false);
		StartCoroutine(CheckDomain());
#endif

#if UNITY_WEBGL
		// deactivate all the wait objects first
		ActivateWaitObjects(false);
		StartCoroutine(CheckDomain());
#endif

	}

	IEnumerator CheckDomain()
	{
		yield return new WaitForSeconds(0.1f);


		// fetch domain list
		if (domainListURL != null && domainListURL != "")
		{
			WWW www = new WWW(domainListURL + "?r=" + Random.value);
			yield return www;

			if (www.error == null || www.error == "")
			{
				string rawDomains = www.text;
				domainMustContain = rawDomains.Split(splitters);
			}
		}

		// build a checking js and run it
		if (domainMustContain.Length > 0)
		{
			StringBuilder buf = new StringBuilder();

			for (int i = 0; i < domainMustContain.Length; i++)
			{
				string domain = domainMustContain[i].Trim();

				if (i > 0)
				{
					buf.Append(" && ");
				}
				buf.Append("(document.location.host.indexOf('" + domain + "') == -1)");
			}
			string criteria = buf.ToString();
			Application.ExternalEval("if((document.location.protocol != 'file:') && (" + criteria + ")) { window.top.location='" + redirectURL + "'; }");
		}
		yield return new WaitForSeconds(0.1f);

		// reactivate all the wait objects
		ActivateWaitObjects(true);

	}


	void ActivateWaitObjects(bool activeValue)
	{
		int waitObjectsCount = waitObjects.Length;
		for (int i = 0; i < waitObjectsCount; i++)
		{
			waitObjects[i].SetActive(activeValue);
		}
	}
}
