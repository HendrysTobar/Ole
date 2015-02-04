using UnityEngine;
using System.Collections;

public class Slot : MonoBehaviour {

	private TipoDeAdorno adornoAsignado = TipoDeAdorno.Ninguno;

	public TipoDeAdorno AdornoAsignado {
		get {
			return adornoAsignado;
		}
		set {
			adornoAsignado = value;
		}
	}

	public void RecibirAdorno(TipoDeAdorno adorno, Sprite sprite)
	{
		//Se destruyen todos los objetos que tenga el slot como hijos
		DestruirObjetosHijos();

		//se crea el adorno
		GameObject a = new GameObject(adorno.ToString());
		SpriteRenderer sr =  a.AddComponent<SpriteRenderer>();
		sr.sprite = sprite;
		a.transform.parent = this.transform;
		a.transform.position = a.transform.parent.position;
		a.transform.rotation = a.transform.parent.rotation;
		a.transform.localScale = new Vector3(0.14f,0.14f,0.14f);

		adornoAsignado = adorno;

		//TODO:Validar si se han puesto los objetos correctamente
		//Llamar al verificador del padre


	}

	void DestruirObjetosHijos ()
	{
		foreach(Transform t in transform)
		{
			Destroy(t.gameObject);
		}

	}
}

