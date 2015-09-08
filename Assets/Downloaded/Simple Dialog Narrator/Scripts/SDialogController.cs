using UnityEngine;
using System.Collections;

public class SDialogController : MonoBehaviour {

	public bool textShown = false;
	public bool textShowing = false;
	public bool textSkip = false;
	public bool appearSkip = false; // skips the appear anim (checked by SDialogManager)
	//Edited by Hendrys Tobar 07 Sept 2015
	/// <summary>
	/// The dialog controller is active (it receives input)
	/// </summary>

	public bool active = true;

	void OnMouseUpAsButton() {

		if(!active)
			return;
		if(textShown == true){ // already shown the text in full
			SDialogManager.callNextDialog(true);// calls the next dialog (if any) *Manager will destroy this controller
		} else if(textShowing == false){ // not yet showing text (likely still appearing)
			appearSkip = true; // skips the appear anim
		} else {
			textSkip = true; // skips the text being displayed one-char-at-a-time
		}
	}
}
