using UnityEngine;
using System.Collections;

public class EscenaPortada : MonoBehaviour {
	public SimpleDialogCall dialogoFin;
	public MacetaTutorial maceta;
	// Use this for initialization
	void Start () 
	{
		maceta.onAccionado += MacetaAccionada;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void MacetaAccionada ()
	{
		dialogoFin.manualStart();
	}
}


