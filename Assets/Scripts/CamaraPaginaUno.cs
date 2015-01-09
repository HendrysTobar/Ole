using UnityEngine;
using System.Collections;

public class CamaraPaginaUno : MonoBehaviour {

	public GameObject imageTarget;
	public GameObject canvas;
	// Use this for initialization
	void Start () {
		Camera.main.orthographic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PonerCamaraPerspectiva(){
		Camera.main.orthographic = false;
		imageTarget.SetActive(true);
		canvas.SetActive (true);
	}
}
