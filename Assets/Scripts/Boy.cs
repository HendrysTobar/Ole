using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

	public GameObject cama;
	public GameObject camaConNinyo;
	public string comportamientoNiño;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Dormirse()
	{
		//cambiar la cama a cama-con-niño
		/// Destruir la cama vieja
		GameObject nuevaCama  = Instantiate(camaConNinyo,cama.transform.position,camaConNinyo.transform.rotation) as GameObject;
		UnityEngine.UI.Text textComportamientoNiño = nuevaCama.GetComponentsInChildren<UnityEngine.UI.Text>(true)[0];
		textComportamientoNiño.text = comportamientoNiño;
		Destroy (cama);
		/// Instanciar la cama-con-niño



		//Destruir el objeto de este script
		Destroy(this.gameObject);
	}
}
