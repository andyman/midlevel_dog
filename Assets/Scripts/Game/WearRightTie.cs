using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearRightTie : MonoBehaviour
{
	public GameObject bowTie;
	public GameObject tie;

	// Start is called before the first frame update
	void Start()
	{
		bowTie.SetActive(TwoWolvesLevelController.bowTieLived);
		tie.SetActive(!TwoWolvesLevelController.bowTieLived);

	}

}
