using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFunk : MonoBehaviour
{
	public void Hide()
	{
		Cursor.visible = false;
	}

	public void Show()
	{
		Cursor.visible = true;
	}
}
