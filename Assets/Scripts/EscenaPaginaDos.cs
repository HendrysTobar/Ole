using UnityEngine;
using System.Collections;

public class EscenaPaginaDos : MonoBehaviour {

	private Elfo elfo;
	public GameObject world;
	private int numeroDeArboles = 0;

	// Use this for initialization
	void Start () 
	{
		elfo = FindObjectOfType<Elfo>();
		if(elfo == null)
		{
			Debug.LogWarning("No se encuentra el objeto elfo en la escena");
			return;
		}

		elfo.OnObjetoAccionado += ContarArboles;
	
	}
	



	void ContarArboles (string tagObjeto)
	{
		if(tagObjeto == "Maceta")
			numeroDeArboles++;
		if(numeroDeArboles == 3)
		{
			CambiarEscenario();
		}

	}


	void CambiarEscenario ()
	{
		//Desaparecer el mundo actual (reproducir la animacion, al final se destruye el objeto y al final se pone el cenador)
		world.animation.Play();
	}
}



















































