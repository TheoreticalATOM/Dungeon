using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrisonDoor : ActionItem  
{
	public GameObject Pivot;

	public override void Interact()
	{
		Pivot.transform.Rotate(0, -90, 0);	//Lerp it
	}
}
