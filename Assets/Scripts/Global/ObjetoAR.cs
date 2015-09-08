using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjetoAR : MonoBehaviour {

	public string nombre;
	public GameObject textComponent;
	private AudioSource[] audios;
	/// <summary>
	/// Dice si el objeto esta activo, esto es, si recibe clicks. 
	/// Se usa para pausar la deteccion de input de parte del jugador.
	/// </summary>
	bool activo = true;


	// Use this for initialization
	void Start () 
	{
		audios = GetComponents<AudioSource>();

	}

	// Update is called once per frame
	void Update () 
	{
		//Si tocan este objeto
		if(Touched() && activo)
		{
			//Activar o desactivar la etiqueta del nombre
			ToggleName();
			//Hacer que el elfo vaya hacia la posicion de este objeto
			if(Elfo.instancia!=null)
			{
				Elfo.instancia.GoTo(this.transform.position);
			}

		}

	}


	bool Touched ()
	{
		/*
		if(Input.touchCount == 0)
			return false;

		Touch t = Input.GetTouch(0);
		if(t.phase == TouchPhase.Ended)
		{
			Ray ray = camara.camera.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hitInfo;
			//Si el rayo golpea algo
			if(Physics.Raycast(ray, out hitInfo))
			{
				Debug.Log("Algo golpeo");
				if(hitInfo.collider.gameObject == this.gameObject || hitInfo.collider.gameObject == this.transform.parent.gameObject)
				{

					return true;
				}
			}
			return false;

			

			
		}
		return false;
		*/
		return TouchHelper.ObjetoTocado == this.gameObject || TouchHelper.ObjetoTocado == this.transform.parent.gameObject;
	}


	void OnMouseDown()
	{
		//Machetico para saber si ya se ha tocado con el Touch
		if(Input.touchCount > 0)
		{
			//Se ha usado el Touch, entonces no haga nada
			return;
		}
		if(activo)
			ToggleName();

	}


	public void ToggleName ()
	{
		Debug.Log("Toggling Name");
		textComponent.SetActive(!textComponent.activeSelf);

		PlaySound(textComponent.activeSelf);



	}
	

	void PlaySound (bool a)
	{

		audios[a?1:0].Play();


	}

	public void Desactivar ()
	{
		activo = false;
	}

	public void Activar ()
	{
		activo = true;
	}
}
