using UnityEngine;
using System.Collections;

// class for a dialog. It holds properties of a dialog as well as executes a dialog..

[System.Serializable]
public class ASDialog : System.ICloneable{
	[Header("Basic Properties :-")]
	public Transform parent;
	public GameObject avatar1;
	public DialogAnchor aAnchor = DialogAnchor.BottomLeft;
	public GameObject avatar2;
	public DialogAnchor bAnchor = DialogAnchor.BottomRight;
	public string header = "Simple Dialog Asset";
	[Multiline] public string text = "A dialog text. Please write some stuff here...";
	
	public DialogPropertyOptional optionalProperties;
	public DialogPropertyOptional o {get{return optionalProperties;}}

	[System.Serializable]
	public class DialogPropertyOptional{
		// optional properties
		[Header("Dialog Properties :-")]
		[Range(1,100)] public float widthPercent = 70f;
		[Range(1,100)] public float heightPercent = 40f;
		public DialogAnchor anchor = DialogAnchor.BottomCenter;
		public GameObject prefab;

		[Header("Avatar 1 Properties :-")]
		[Range(1,100)] public float startSizePercentA = 15f;
		[Range(1,100)] public float endSizePercentA = 15f;
		public Color startColor1 = Color.white;
		public Color endColor1 = Color.white;
		[Header("Avatar 2 Properties :-")]
		[Range(1,100)] public float startSizePercentB = 15f;
		[Range(1,100)] public float endSizePercentB = 15f;
		public Color startColor2 = Color.white;
		public Color endColor2 = Color.white;

		[Header("Font Properties :-")]
		public int fontSize = 0;
		[SerializeField] public float textSpeed = 0.03f;
		public Font font;
		public Color fontColor;

		[Header("Header Font Properties :-")]
		public int headerFontSize = 0;
		public Font headerFont;
		public Color headerFontColor;

		[Header("Invoke Other Scripts :- (Link script's gameObject)")]
		public GameObject onDialogShow;
		public string onShowMethodName;
		[Space(10f)]
		public GameObject onTextShown;
		public string onTextMethodName;
		[Space(10f)]
		public GameObject onDialogExit;
		public string onExitMethodName;
	}
	// MAIN CONSTRUCTOR
	public ASDialog(){} // required for getting default values set above...

	// OVERLOADED Constructors
	public ASDialog(string _text){ // only sets the text...
		text = _text;
	}
	// OVERLOADED Constructors
	public ASDialog(string _header, string _text){ // only sets the text & header...
		header = _header;
		text = _text;
	}

	// gives the ability to clone the dialog properties
	public object Clone(){
		return this.MemberwiseClone();
	}

	// calls the runDialog sequence
	public void execute(bool appearAnim){
		SDialogManager.runDialog(this,appearAnim);
	}
}