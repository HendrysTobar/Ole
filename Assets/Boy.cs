using UnityEngine;
using System.Collections;

public class Boy : MonoBehaviour {

	public GameObject cama;
	public GameObject camaConNinyo;
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
		Instantiate(camaConNinyo,cama.transform.position,camaConNinyo.transform.rotation);
		Destroy (cama);
		/// Instanciar la cama-con-niño



		//Destruir el objeto de este script
		Destroy(this.gameObject);
	}
}
