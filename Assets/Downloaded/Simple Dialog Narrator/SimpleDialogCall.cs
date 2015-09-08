using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleDialogCall : MonoBehaviour {

	public bool startManually = false;
	public float delayBeforeStart = 0f;
	public ASDialog[] dialogs = new ASDialog[1];

	// Use this for initialization
	void Start () {
		if(!startManually) StartCoroutine(runDialogs()); // do not call if it starts manually...
	}

	// manual start call..
	public void manualStart(){
		StartCoroutine(runDialogs());
	}

	IEnumerator runDialogs(){
		yield return new WaitForSeconds(delayBeforeStart);

		foreach(ASDialog d in dialogs){
			SDialogManager.displayDialog(d);
		}
	}
}
