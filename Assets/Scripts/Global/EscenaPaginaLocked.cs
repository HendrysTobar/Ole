using UnityEngine;
using System.Collections;

public class EscenaPaginaLocked : MonoBehaviour {
	public static string paginaQueSeIntentoCargar;
	public GameObject padlock;
	// Use this for initialization
	void Awake () {
		//Buscamos la pagina que se intento cargar
		GameObject go = GameObject.Find(paginaQueSeIntentoCargar);
		//Este image tracker ya no debe usarse para cargar la pagina porque sino se crearia un loop infinito
		Destroy (go.GetComponent<CargarEscenaTrackableEventHandler>());
		go.AddComponent<DeactivateChildrenTrackableEventHandler>();
		go.transform.parent = null;
		padlock.transform.parent = go.transform;
	


	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
