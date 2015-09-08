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
		GameObject nuevoAdorno = new GameObject(adorno.ToString());
		SpriteRenderer sr =  nuevoAdorno.AddComponent<SpriteRenderer>();
		sr.sprite = sprite;
		nuevoAdorno.transform.parent = this.transform;
		nuevoAdorno.transform.position = nuevoAdorno.transform.parent.position;
		//El adorno queda mirando hacia la camara
		nuevoAdorno.transform.LookAt(EscenaOle.arCamera.transform);
		nuevoAdorno.transform.localScale = new Vector3(0.14f,0.14f,0.14f);

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

