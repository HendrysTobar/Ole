using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPersonaje : MonoBehaviour {

	/// <summary>
	/// Variables privadas 
	/// </summary>
	CharacterController cc;
	Plane groundPlane;
	Vector3 destination;


	/// <summary>
	/// Variables publicas editables desde el inspector 
	/// </summary>
	public float speed;
	public GameObject flare;

	// Use this for initialization
	void Start () 
	{
		cc = GetComponent<CharacterController>();
		groundPlane = new Plane(transform.parent.up, transform.parent.position);
		destination = transform.position;
	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		Vector3 d = Capture3DClickPosition();
		if(d != Vector3.zero)
		{
			destination = d;
			PlaceFlare(d);
			move = true;
		}

		Vector3 vel = destination - transform.position;

		if(move)
			cc.SimpleMove(speed * vel.normalized * Time.deltaTime);

	
	}


	Vector3 Capture3DClickPosition ()
	{
		Vector3 xy = Vector3.zero;
		if (Input.GetMouseButtonDown(0))
		{
			xy = Input.mousePosition;
		}
		else
		{
			if(Input.touchCount > 0)
			{
				xy = Input.GetTouch(0).position;
			}
			else
			{
				return Vector3.zero;
			}
		}


		Ray ray = Camera.main.ScreenPointToRay(xy);
		float rayDistance;
		if (groundPlane.Raycast(ray, out rayDistance))
		{
			return ray.GetPoint(rayDistance);
		}
		else
		{
			return Vector3.zero;
		}




	}
	/// <summary>
	/// Pone una pequeña bengala donde el usuario hizo clic para demostrar que es hacia ahi que debe ir el elfo
	/// La destruye en 3 segundos
	/// Ademas pone un sonidito
	/// </summary>
	/// <param name="d">D.</param>
	private Object oldFlare;
	void PlaceFlare (Vector3 pos)
	{
		if(oldFlare != null)
			Destroy(oldFlare);
		oldFlare = Instantiate(flare,pos,Quaternion.identity);
		audio.Play();
		Destroy(oldFlare, 3.0f);

	}
	/// <summary>
	/// Es necesario implementar este metodo para capturar si le dan clic a este objeto.
	/// En ese caso se delega el click al objeto hijo que es el que controla el texto
	/// </summary>
	void OnMouseDown()
	{
		ObjetoAR oar = GetComponentInChildren<ObjetoAR>();
		oar.ToggleName();
	}


	private bool move = false;
	void OnControllerColliderHit(ControllerColliderHit c)
	{
		if(!c.collider.CompareTag("World") && !c.collider.CompareTag("Player"))
			move = false;
	}
}

