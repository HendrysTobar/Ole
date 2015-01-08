using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

public enum DialogAnchor {
	TopLeft, TopCenter, TopRight, 
	CenterLeft, Center, CenterRight,
	BottomLeft, BottomCenter, BottomRight
}
public static class SDialogManager {

	static string dlgBG = "Background"; // the name of the gameObject that holds the background
	static string dlgBGContainer = "BGContainer"; // the name of the gameObject that holds the background
	static string dlgText = "Text"; // the name of the gameObject that holds the dialog text
	static string dlgAvatar1 = "Avatar1"; // the name of avatar 1 object (auto set)
	static string dlgAvatar2 = "Avatar2"; // the name of avatar 2 object (auto set)
	static string headerContainer = "HeaderContainer"; // the name of the gameObject that holds the background
	static string headerText = "HeaderText"; // the name of the gameObject that holds the dialog text
	static string headerBG = "HeaderBg"; // the name of the gameObject that holds the dialog title

	static List<ASDialog> dList = new List<ASDialog>(); // list of all dialogs to be shown
	static ASDialog currentDialog; // the current dialog as reference...

	// text border values
	static float widthPercent = 0.03f; // percentage of screen Width as border (range from 0.00 to 1)
	static float heightPercent = 0.02f; // percentage of screen Height as border (range from 0.00 to 1)

	// for auto-calculation values
	static float widthBorder {get{return Screen.width*widthPercent;}} // the value of a the width border based on desired percentage
	static float HeightBorder {get{return Screen.height*heightPercent;}} // the value of a the width border based on desired percentage
	static float widthBorderV {get{
			return Mathf.Abs( (Camera.main.ScreenToWorldPoint(Vector3.zero) - 
				Camera.main.ScreenToWorldPoint(new Vector3(widthBorder,0,0) )).x);
		}}
	static float heightBorderV {get{
			return Mathf.Abs( (Camera.main.ScreenToWorldPoint(Vector3.zero) - 
				Camera.main.ScreenToWorldPoint(new Vector3(0,HeightBorder,0) )).y);
		}}

	static float appearPercentageChange = 2f; // the rate of change for each appear routine cycle

	static GameObject go; // reference of the dialog gameObject
	static bool beingDestroyed = false; // bool to determine if this dialog is on the process of being destroyed...

	#region public methods

	// TODO please make your own displayDialog() call to suit your own requirements :)
	// TODO also make sure the ASDialog has the desired constructor to suit your own requirements :)
	
	// OVERLOADED METHOD
	public static void displayDialog(string text){
		displayDialog(new ASDialog(text));
	}
	// OVERLOADED METHOD
	public static void displayDialog(string header, string text){
		displayDialog(new ASDialog(header,text));
	}

	// MAIN PUBLIC CALLING METHOD
	public static void displayDialog(ASDialog aDialog){
		// add to the current dialog list to display...
		dList.Add( aDialog );
		callNextDialog(false); // calls the next dialog
	}

	// method to append a dialog...
	public static void displayDialogAppend(ASDialog d, string text){
		ASDialog clone = (ASDialog) d.Clone(); // clones the dialog...

		// bring forward the properties...
		clone.text = text; // new text
		clone.o.startColor1 = d.o.endColor1; // use back old color
		clone.o.startColor2 = d.o.endColor2; // use back old color
		clone.o.startSizePercentA = d.o.endSizePercentA; // use back old size
		clone.o.startSizePercentB = d.o.endSizePercentB; // use back old size

		dList.Insert(0,clone ); // appends the new modified cloned dialog...
	}

	// function to call the next dialog (if any)
	public static bool callNextDialog(bool destroyLast){
		bool instantShow = false; // state to determine if the dialog appears immediately w/o appear anim

		if(beingDestroyed){return false;} // a dialog is being destroyed... do not continue..
		if(destroyLast){
			if(go != null){
				// invoke methods (if any is assigned)
				if(currentDialog.o.onDialogExit){
					currentDialog.o.onDialogExit.SendMessage(currentDialog.o.onExitMethodName);
				}
				if(dList.Count == 0){ // no further dialogs...
					beingDestroyed = true;
					go.GetComponent<SDialogController>().StartCoroutine( fadeOutDialog() );
				} else{
					GameObject.Destroy(go); // destroys the go immediately
					go = null; // null the reference...
					instantShow = true;
				}
			}
		}
		if(go != null){return false;} // a dialog is currently active... do not continue...
		if(dList.Count > 0){
			if(instantShow){
				dList[0].execute(false); // there's a current dialog, do not perform appear anim
			} else {
				dList[0].execute(true); // no current dialog... appear anim
			}
			currentDialog = dList[0];
			dList.RemoveAt(0);
			return true;
		}
		return false; // no more dialog
	}

	// function to skip all current listed dialogs
	public static void skipAllCurrentDialogs(){
		dList.Clear();
	}

	// runDialog for processing dialogs and the dialogs to come...
	public static void runDialog(ASDialog d, bool isNew){
		//
		// NOTES :- d = the ASDialog reference.
		//          d.o = the optional properties in the ASDialog. 
		//
		// get the dialog prefab
		if(d.o.prefab == null){
			go = Object.Instantiate(Resources.Load("DialogPrefab")) as GameObject;
		} else {
			go = Object.Instantiate(d.o.prefab) as GameObject;
		}

		//ADDED: Esto es para que el objeto quede anidado dentro del parametro parent
		if(d.parent != null)
		{
			go.transform.parent = d.parent;
			go.transform.localRotation = Quaternion.identity;
			//Esto es para mover el objeto un poquito mas alla de su padre para que se pueda ver.
			go.transform.localPosition = new Vector3(go.transform.localPosition.x, go.transform.localPosition.y,2f);
		}

		// get the references object
		Renderer sr = findObject(dlgBG).GetComponent<Renderer>();
		TextMesh tm = findObject(dlgText).GetComponent<TextMesh>();
		Renderer hsr = findObject(headerBG).GetComponent<Renderer>();
		TextMesh htm = findObject(headerText).GetComponent<TextMesh>();
		Transform hContainer = findObject(headerContainer);
		Transform bgContainer = findObject(dlgBGContainer);

		// auto-scale the background...
		autoResize(findObject(dlgBG),d.o.widthPercent,d.o.heightPercent); // bg autoscale

		// set the header...
		if(htm != null ){ // header object exist
			htm.text = d.header; // sets the header text...
			htm.alignment = TextAlignment.Left; // sets the text alignment
			htm.anchor = TextAnchor.MiddleLeft; // sets the text anchor
			if(d.o.headerFont != null) htm.font = d.o.headerFont;
			if(d.o.headerFontColor != Color.clear) htm.color = d.o.headerFontColor;
			if(d.o.headerFontSize != 0) htm.fontSize = d.o.headerFontSize;
			
			if(sr != null){ // background exist...
				// text bounds within the background
				htm.transform.position = new Vector3( sr.transform.position.x - sr.bounds.extents.x + widthBorderV*2,
				                                     sr.transform.position.y + sr.bounds.extents.y,
				                                     sr.transform.position.z - 0.02f);
				htm.transform.localPosition = new Vector3(htm.transform.localPosition.x, htm.transform.localPosition.y, -0.1f);

			} else { Debug.Log("Error! Dialog Background not available..."); }
			
			if(hsr != null){ // header background exist...
				if(d.header.Equals("") ){ // if it's a blank msg... 
					// scale = 0 for no show...
					hsr.transform.localScale = Vector3.zero;
					htm.transform.localScale = Vector3.zero;

					// moving the position refreshes the renderer bounds... 
					// (required!) for some dumb unity reason
					hsr.transform.position = Vector3.zero;
					htm.transform.position = Vector3.zero;

					Object.Destroy(hsr.gameObject); // destroy the header background object...
					Object.Destroy(htm.gameObject); // destroy the header text object...
					htm = null;
					hsr = null;
				} else {
					// resize it appropriately
					hsr.transform.localScale = Vector3.one; // reset to one first...
					Vector3 v1 = htm.renderer.bounds.size;
					Vector3 v2 = hsr.bounds.size;
					hsr.transform.localScale = new Vector3(v1.x/v2.x + widthBorderV/2, v1.y/v2.y + heightBorderV, v1.z/v2.z); 
					
					
					// move the background to the header text...
					hsr.transform.position = htm.renderer.bounds.center + new Vector3(0,0,0.01f);

					if(hContainer != null){
						hsr.transform.parent = hContainer; // attach header bg to the container
						htm.transform.parent = hContainer; // attach header text to the container
						if(bgContainer != null) hContainer.parent = bgContainer; // attach header to bg
					}
				}
			}
		}

		// sets the text properties
		if(tm != null){
			tm.text = ""; // do not show any text...
			tm.alignment = TextAlignment.Left; // sets the text alignment
			tm.anchor = TextAnchor.UpperLeft; // sets the text anchor
			if(d.o.font != null) tm.font = d.o.font;
			if(d.o.fontColor != Color.clear) tm.color = d.o.fontColor;
			if(d.o.fontSize != 0) tm.fontSize = d.o.fontSize;

			if(hsr != null){
				tm.transform.position = new Vector3( sr.transform.position.x - sr.bounds.extents.x + widthBorderV,
				                                    hsr.transform.position.y - hsr.bounds.extents.y - heightBorderV,
				                                    sr.transform.position.z - 0.01f);
			} else if(sr != null){ // background exist...
				// text bounds within the background
				tm.transform.position = new Vector3( sr.transform.position.x - sr.bounds.extents.x + widthBorderV,
				                                    sr.transform.position.y + sr.bounds.extents.y - heightBorderV,
				                                    sr.transform.position.z - 0.01f);
			}
			tm.transform.localPosition = new Vector3(tm.transform.localPosition.x, tm.transform.localPosition.y, -0.1f);
		}

		// anchors the dialogBox
		setAnchor(go.transform,d.o.anchor);

		// sets the avatar 1 (if available)
		if(d.avatar1 != null){
			Transform tfav1 = (Object.Instantiate(d.avatar1) as GameObject).transform; // instantiate
			tfav1.transform.parent = go.transform; // reparents the avatar
			tfav1.name = dlgAvatar1; // force set the name
		}
		// sets the avatar 2 (if available)
		if(d.avatar2 != null){
			Transform tfav2 = (Object.Instantiate(d.avatar2) as GameObject).transform; // instantiate
			tfav2.transform.parent = go.transform; // reparents the avatar
			tfav2.name = dlgAvatar2; // force set the name
		}
		go.GetComponent<SDialogController>().StartCoroutine(showAvatar(d) );

		if(isNew){ // have the dialog box has an appear animation
			go.GetComponent<SDialogController>().StartCoroutine(dialogAppear(d) );
		} else { // just show the dialog box without appear animation
			go.GetComponent<SDialogController>().StartCoroutine(textWriteOut(d) );
		}
	}

	#endregion public methods

	#region private methods

	// function to help find a child object (becoz unity does not have a proper one)
	static Transform findObject(string name){
		foreach(Transform tf in go.transform){
			if(tf.name.Equals(name)){
				return tf;
			}
			Transform deepSearch = recursiveFindObject(name,tf);
			if(deepSearch != null){
				return deepSearch;
			}
		}
		return null;
	}

	static Transform recursiveFindObject(string name,Transform _tf){
		foreach(Transform tf in _tf){
			if(tf.name.Equals(name)){
				return tf;
			}
			Transform embedTf = recursiveFindObject(name,tf);
			if(embedTf != null){
				return embedTf;
			}
		}
		return null;
	}

	#endregion private methods

	#region Dialog Routines

	static IEnumerator showAvatar(ASDialog d){
		List<Renderer> rensA = new List<Renderer>();
		List<Renderer> rensB = new List<Renderer>();

		Transform av1 = findObject(dlgAvatar1);
		Transform av2 = findObject(dlgAvatar2);
		Color color; // container for a color
		
		if(av1 != null){
			rensA.AddRange(av1.GetComponentsInChildren<Renderer>());
		}
		if(av2 != null){
			rensB.AddRange(av2.GetComponentsInChildren<Renderer>());
		}

		float percent = 0;
		Color a; // reference for start color
		Color b; // reference for end color
		float s; // reference for start size
		float e; // reference for end size
		while(percent <= 1){
			// avatar 1 mode
			a = d.o.startColor1; // reference for start color
			b = d.o.endColor1; // reference for end color
			s = d.o.startSizePercentA;  // reference for start size
			e = d.o.endSizePercentA; // reference for end size

			foreach(Renderer ren in rensA){ // fades in the avatars
				color = new Color(a.r + (b.r - a.r) * percent, a.g + (b.g - a.g) * percent,
				                  a.b + (b.b - a.b) * percent, a.a + (b.a - a.a) * percent);
				ren.material.color = color;
			}
			if(av1 != null){
				autoResize(av1,s+((e-s)*percent),0); // 0 height = auto scale ratio
				setAnchor(av1,d.aAnchor); // sets the anchor
				av1.position+= new Vector3(0,0,-0.1f); // brings it to the front...
			}



			// avatar 2 mode
			a = d.o.startColor2; // reference for start color
			b = d.o.endColor2; // reference for end color
			s = d.o.startSizePercentB;  // reference for start size
			e = d.o.endSizePercentB; // reference for end size
			foreach(Renderer ren in rensB){ // fades in the avatars
				color = new Color(a.r + (b.r - a.r) * percent, a.g + (b.g - a.g) * percent,
				                  a.b + (b.b - a.b) * percent, a.a + (b.a - a.a) * percent);
				ren.material.color = color;
			}
			if(av2 != null){
				autoResize(av2,s+((e-s)*percent),0); // 0 height = auto scale ratio
				setAnchor(av2,d.bAnchor); // sets the anchor
				av2.position+= new Vector3(0,0,-0.1f); // brings it to the front...
			}

			percent+= appearPercentageChange/100f;
			if(go.GetComponent<SDialogController>().textSkip == false){
				yield return new WaitForSeconds(0.01f);
			}
			
		}
	}

	// function to scale the dialog on first show...
	static IEnumerator dialogAppear(ASDialog d){
		// invoke methods (if any is assigned)
		if(d.o.onDialogShow){
			d.o.onDialogShow.SendMessage(d.o.onShowMethodName);
		}

		Transform bg = findObject(dlgBGContainer);
		float percent = 0;
		while(percent <= 1){
			bg.localScale = new Vector3(1*percent,1*percent,1*percent); // scales the bg (text inclusive)
			percent+= appearPercentageChange/100f;
			if(go.GetComponent<SDialogController>().appearSkip == false){
				yield return new WaitForSeconds(0.01f);
			}

		}

		go.GetComponent<SDialogController>().StartCoroutine(textWriteOut(d) );
	}

	static IEnumerator textWriteOut(ASDialog d){
		go.GetComponent<SDialogController>().textShowing = true;
		TextMesh tm = findObject(dlgText).GetComponent<TextMesh>();
		Renderer sr = findObject(dlgBG).GetComponent<Renderer>();

		int count = 0;
		while(count <= d.text.Length){
			tm.text = d.text.Substring(0,count);
			textWrapToBG();
			count++;

			// If text space is bigger than the provided background... (bottom limit)
			if(tm.renderer.bounds.min.y < (sr.bounds.min.y + heightBorderV) ){
				int index = tm.text.LastIndexOf(" ");
				if(index >= 0){ // more than one word that can be split up...
					tm.text = tm.text.Remove(index) + "...";
					count = d.text.Length+1; // force complete...
					
					displayDialogAppend(d,d.text.Substring(index));
				}
			}

			if(count >= d.text.Length){
				go.GetComponent<SDialogController>().textShown = true;
				// invoke methods (if any is assigned)
				if(d.o.onTextShown){
					d.o.onTextShown.SendMessage(d.o.onTextMethodName);
				}
			}
			if(go.GetComponent<SDialogController>().textSkip == false){
				yield return new WaitForSeconds(d.o.textSpeed);
			}
		}
	}

	// routine to fade the dialog out...
	static IEnumerator fadeOutDialog(){

		// get all the renderers currently available
		Renderer[] rens = go.GetComponentsInChildren<Renderer>();
		
		float percentage = 100;
		Color color;
		
		while(percentage > 0){
			foreach(Renderer ren in rens){ // sets the alpha of all renderer to fade out
				color = ren.material.color;
				color.a = 1f*(percentage/100f);
				ren.material.color = color;
			}
			percentage-= 2;
			
			yield return new WaitForSeconds(0.01f);
		}
		GameObject.Destroy(go); // destroys the go immediately
		go = null; // null the reference...
		beingDestroyed = false; // SDialogManager no longer destroying a dialog...
		callNextDialog(false); // calls the next dialog (if any)
	}

	#endregion Dialog Routines

	#region auto resize function

	// function to auto calculate the size of the dialog prompt
	static void autoResize(Transform tf, float widthPercentage,float heightPercentage){
		// check if transform passed in exist...
		if(tf == null){
			Debug.Log("Finding gameOject by name not found. Please check your gameObject's name again.");
			return;
		}

		// check if the scale is 0
		if(widthPercentage == 0 && heightPercentage == 0){
			tf.localScale = Vector3.zero; // sets it to zero straight away...
			return; // no need to do any calculations...
		}

		tf.localScale = Vector3.one; // reset scale to 1 before any calculations...

		// background resize
		Renderer[] rens = tf.GetComponentsInChildren<Renderer>();

		if(rens.Length == 0){
			Debug.Log("There is nothing to render on " + tf.name + "... failed to auto resize. " +
			          "Please check the asset again.");
			return;
		}
		Bounds bounds = rens[0].bounds; // get the first bound to start with...
		foreach(Renderer ren in rens){
			bounds.Encapsulate(ren.bounds); // gets all bounds of all the rendered stuff
		}

		Vector3 minPos = Camera.main.WorldToScreenPoint(bounds.min);
		Vector3 maxPos = Camera.main.WorldToScreenPoint(bounds.max);
		float tfWitdh = Mathf.Abs(maxPos.x - minPos.x); // the object's rendered width on screen
		float tfHeight = Mathf.Abs(maxPos.y - minPos.y); // the object's rendered height on screen

		// scale it to desired screen percentage specified...
		if(heightPercentage == 0){ // auto scale height to maintain ratio...
			tf.localScale = new Vector3(
				((float) Screen.width / tfWitdh) * tf.localScale.x * (widthPercentage/100f),
				((float) Screen.width / tfWitdh) * tf.localScale.y * (widthPercentage/100f), 1);
		} else { // width & height determined...
			tf.localScale = new Vector3(
				((float) Screen.width / tfWitdh) * tf.localScale.x * (widthPercentage/100f),
				((float) Screen.height / tfHeight) * tf.localScale.y * (heightPercentage/100f), 1);
		}
	}

	#endregion auto resize function

	#region text word wrap helper

	// Wrap text by line height
	static void textWrapToBG(){
		Renderer sr = findObject(dlgBG).GetComponent<Renderer>();
		TextMesh tm = findObject(dlgText).GetComponent<TextMesh>();

		// Split string by char " "
		string[] words = tm.text.Split(" "[0]);
		// Prepare result
		string result = "";
		// Temp line string
		string line = "";
		// for each all words
		foreach(string s in words){
			// Append current word into line
			string temp = line + " " + s;
			tm.text = temp;
			// If text space is bigger than the provided background...
			if(tm.renderer.bounds.max.x > (sr.bounds.max.x - widthBorderV) ){
				// Append current line into result
				result += line + "\n";
				// Remain word append into new line
				line = s;
			}
			// Append current word into current line
			else {
				line = temp;
			}
		}
		// Append last line into result
		result += line;
		// Remove first " " char
		tm.text = result.Substring(1,result.Length-1);
	}

	#endregion text word wrap helper

	#region anchor system

	// function that sets the dialog object to an anchor as well as the nearest z-depth of the main camera
	static void setAnchor(Transform tf, DialogAnchor anchor){
		if(tf == null){
			return; // no transform here to anchor anything...
		}

		float posX, posY; // the variable to store the anchor point

		// gets the bounds of all the rendering of the game object
		Renderer[] rens = tf.GetComponentsInChildren<Renderer>();
		if(rens.Length == 0){
			Debug.Log("There is nothing to render on " + tf.name + "... failed to set anchor. " +
			          "Please check the asset again.");
			return;
		}

		Bounds bounds = rens[0].bounds; // get the first bound to start with...
		foreach(Renderer ren in rens){
			bounds.Encapsulate(ren.bounds); // gets all bounds of all the rendered stuff
		}

		Vector3 minPos = Camera.main.WorldToScreenPoint(bounds.min);
		Vector3 maxPos = Camera.main.WorldToScreenPoint(bounds.max);
		float tfWitdh = (maxPos.x - minPos.x) / 2f; // the object's rendered half-width on screen
		float tfHeight = (maxPos.y - minPos.y) / 2f; // the object's rendered half-height on screen

		// set the anchor based in the current screen but bounded within the edge of the screen with borders
		switch (anchor){
		case DialogAnchor.TopLeft:
			posX = 0 + tfWitdh + widthBorder;
			posY = Screen.height - tfHeight - HeightBorder;
			break;

		case DialogAnchor.TopCenter:
			posX = Screen.width/2;
			posY = Screen.height - tfHeight - HeightBorder;
			break;

		case DialogAnchor.TopRight:
			posX = Screen.width - tfWitdh - widthBorder;
			posY = Screen.height - tfHeight - HeightBorder;
			break;
			
		case DialogAnchor.CenterLeft:
			posX = 0 + tfWitdh + widthBorder;
			posY = Screen.height/2;
			break;

		case DialogAnchor.Center: default :
			posX = Screen.width/2;
			posY = Screen.height/2;
			break;

		case DialogAnchor.CenterRight:
			posX = Screen.width - tfWitdh - widthBorder;
			posY = Screen.height/2;
			break;

		case DialogAnchor.BottomLeft:
			posX = 0 + tfWitdh + widthBorder;
			posY = 0 + tfHeight + HeightBorder;
			break;
		
		case DialogAnchor.BottomCenter:
			posX = Screen.width/2;
			posY = 0 + tfHeight + HeightBorder;
			break;
			
		case DialogAnchor.BottomRight:
			posX = Screen.width - tfWitdh - widthBorder;
			posY = 0 + tfHeight + HeightBorder;
			break;
		}

		// position the object to the anchor points
		Vector3 targetPosition = Camera.main.ScreenToWorldPoint( new Vector3( posX, posY ,Camera.main.nearClipPlane + 5) );
		tf.position = tf.position - (bounds.center - targetPosition); // minus the offset of the bounds' center
	}

	#endregion anchor system
}



