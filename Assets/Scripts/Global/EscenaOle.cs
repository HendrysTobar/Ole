using UnityEngine;
using System.Collections;

public class EscenaOle : MonoBehaviour {

	public static Camera arCamera;
	// Use this for initialization
	void Awake () {
		//Debug.LogWarning("La camara de la escena se configura por nombre, cambiar esto");
		//para cambiar, descomentar esta siguiente linea
				//arCamera = GameObject.FindObjectOfType<QCARBehaviour>().gameObject.camera;
		//Y comentar esta

		GameObject ao = GameObject.Find("ARCamera");

		if(ao == null)
		{
			Debug.LogWarning("No se encontro la camara ARCamera");
		}
		else
		{
			arCamera = ao.camera;
			if(arCamera == null)
			{
				Debug.LogWarning("Se encontro ARCamera pero no tiene componente Camera");
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
