using UnityEngine;
using System.Collections;

public class CamaConNinyo : MonoBehaviour {

	private bool esBueno;

	public bool EsBueno {
		get {
			return esBueno;
		}
		set {
			esBueno = value;
		}
	}

	public enum EstadoSombrilla{Ninguna,Blanca,Negra};
	public EstadoSombrilla sombrillaAsignada;
	public Transform gancho;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool EstaCorrectamenteAsignado
	{
		get
		{
			return ((sombrillaAsignada == EstadoSombrilla.Blanca && esBueno) || (sombrillaAsignada == EstadoSombrilla.Negra && !esBueno));
		}
	}
}
