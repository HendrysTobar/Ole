using UnityEngine;
using System.Collections;

public class EscenaOle : MonoBehaviour {

	public static Camera arCamera;
	// Use this for initialization
	void Awake () {
		Debug.LogWarning("La camara de la escena se configura por nombre, cambiar esto");
		//para cambiar, descomentar esta siguiente linea
				//arCamera = GameObject.FindObjectOfType<QCARBehaviour>().gameObject.camera;
		//Y comentar esta
		arCamera = GameObject.Find("ARCamera").camera;
		if(arCamera == null)
		{
			Debug.LogWarning("No se encontro la camara ARCamera");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
