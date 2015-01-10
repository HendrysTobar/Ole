using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPersonaje : MonoBehaviour 
{

	/// <summary>
	/// Variables privadas 
	/// </summary>
	CharacterController cc;
	Plane groundPlane;
	Vector3 destination;
	GameObject camara;
	public Collider triggerWorldBounds;

	EventSystem eventSystem;

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

		eventSystem = GameObject.Find ("EventSystem").GetComponent<EventSystem>();
		camara = GameObject.Find ("ARCamera");

	
	}
	
	// Update is called once per frame
	void LateUpdate () {

		//Si no se esta dando click en un objeto de la GUI
		//y
		//El touch Helper no ha tocado algo
		if(eventSystem.currentSelectedGameObject == null && !TouchHelper.HaTocadoAlgo)
		{
			//Capturamos la posicion deseada por el usuario
			Vector3 d = Capture3DClickPosition();
			//Si esa posicion es distinta de cero (es decir, que se ha detectado una entrada del usuario)
			//y
			//Esa posicio esta dentro del mundo
			if(d != Vector3.zero && triggerWorldBounds.bounds.Contains(d) )
			{
				destination = d;
				PlaceFlare(d);
				move = true;
			}
		}

		Vector3 vel = destination - transform.position;



		if(move)
			cc.SimpleMove(speed * vel.normalized * Time.deltaTime);

	
	}


	Vector3 Capture3DClickPosition ()
	{

		Vector3 xy = Vector3.zero;
		if(Input.touchCount > 0)
		{
			if(Input.GetTouch(0).phase == TouchPhase.Ended)
			{
				Debug.Log("Se ha soltado el touch");
				xy = Input.GetTouch(0).position;
			}
			else
			{
				return Vector3.zero;
			}
		}
		else
		{
			if (Input.GetMouseButtonDown(0))
			{
				Debug.Log("Se ha presionado el Mouse");
				xy = Input.mousePosition;
			}
			else
			{
				return Vector3.zero;
			}
		}

		Debug.Log(xy.ToString());
		Ray ray = camara.camera.ScreenPointToRay(xy);
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

