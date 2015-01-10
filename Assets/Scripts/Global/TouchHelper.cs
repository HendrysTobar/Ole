using UnityEngine;
using System.Collections;


/// <summary>
/// Esta clase se utilizar para que se haga solo una validacion global de cual fue el objeto tocado
/// El objeto tocao se almacena en una propiedad que puede ser consultada por los otros objetos.
/// </summary>
public class TouchHelper : MonoBehaviour {

	/// <summary>
	/// El objeto tocado. Null si no se ha tocado nada
	/// </summary>
	private static GameObject objetoTocado;
	/// <summary>
	/// La camara que se usa para ver los objetos
	/// </summary>
	public GameObject camara;

	public static GameObject ObjetoTocado {
		get {
			return objetoTocado;
		}
	}

	void Start()
	{
		if(camara == null)
			Debug.LogError("No se ha especificado una camara para el Touch Helper");
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		CheckTouch();
	
	}
	/// <summary>
	/// Checks the touch.
	/// </summary>
	bool CheckTouch ()
	{
		if(Input.touchCount == 0)
		{
			objetoTocado = null;
			return false;
		}
		
		Touch t = Input.GetTouch(0);
		if(t.phase == TouchPhase.Ended)
		{
			Ray ray = camara.camera.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hitInfo;
			//Si el rayo golpea algo
			if(Physics.Raycast(ray, out hitInfo))
			{
				Debug.Log("Algo golpeo: " + hitInfo.collider.gameObject.name);
				objetoTocado = hitInfo.collider.gameObject;
				return true;
			}

			objetoTocado = null;
			return false;

		}
		objetoTocado = null;
		return false;
	}

	/// <summary>
	/// Gets a value indicating whether this <see cref="TouchHelper"/> ha tocado algo.
	/// </summary>
	/// <value><c>true</c> if ha tocado algo; otherwise, <c>false</c>.</value>
	public static bool HaTocadoAlgo
	{
		get
		{
			return objetoTocado != null;
		}
	}
}
