using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignPost : ActionItem {
	public string[] dialouge;
	public override void Interact()
	{
		DialogueSystem.Instance.AddNewDialogue(dialouge,"Sign");
	}


}
