using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System.Text;

public class OfficeSceneText : MonoBehaviour
{
	public TextMeshProUGUI mainText;

	// Start is called before the first frame update
	void Start()
	{
		StringBuilder buf = new StringBuilder();
		buf.Append("CHAPTER 4\nCORPORATE DOG EAT DOG OFFICE\n\n");

		bool rested = DreamLevelController.endResultRested;
		bool goodCommute = CommuteLevelController.goodCommute;

		buf.Append("The long workday has been grueling");

		if (rested && goodCommute)
		{
			buf.Append(",\n");
			buf.Append("but you were extra productive\n");
			buf.Append("because you were well rested\n");
			buf.Append("and had a good morning commute.");
		}
		else if (rested && !goodCommute)
		{
			buf.Append(".\n");

			buf.Append("Despite your restful sleep, you were\n");
			buf.Append("only moderately productive because\n");
			buf.Append("of the smog headache.");
		}
		else if (!rested && goodCommute)
		{
			buf.Append(".\n");

			buf.Append("Despite your good morning commute\n");
			buf.Append("you were only moderately productive\n");
			buf.Append("because of your poor sleep.");
		}
		else if (!rested && !goodCommute)
		{
			buf.Append(",\nand you were unproductive because of\n");
			buf.Append("your poor sleep and smog headache.");
		}

		buf.Append("\n\nYou receive an email from upper management...");
		mainText.text = buf.ToString();
		buf.Clear();

		mainText.gameObject.SetActive(true);
	}

}
