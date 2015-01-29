using UnityEngine;
using System.Collections;

public class ManoIndicadora : MonoBehaviour {

	public MovimientoPersonaje movimientoElfo;
	public GameObject maceta;
	void Start()
	{
		movimientoElfo.OnDirectionChanged += Eliminar;
	}

	public void Eliminar()
	{
		movimientoElfo.OnDirectionChanged-= Eliminar;
		Destroy(gameObject);
		maceta.SetActive(true);
	}

}
