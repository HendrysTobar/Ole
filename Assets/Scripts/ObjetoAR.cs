using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjetoAR : MonoBehaviour {

	public string nombre;
	public GameObject textComponent;
	private AudioSource[] audios;


	// Use this for initialization
	void Start () 
	{
		audios = GetComponents<AudioSource>();

	}

	// Update is called once per frame
	void Update () 
	{

		if(Touched())

		{
			ToggleName();
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
}
