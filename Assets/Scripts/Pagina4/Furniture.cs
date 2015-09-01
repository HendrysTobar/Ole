using UnityEngine;
using System.Collections;

public class Furniture : Tocable {
	public GameObject zeta;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void Dormirse ()
	{
		//activar zetas al Gameobject

		zeta.SetActive (true);
	}

	void Animarse ()
	{
		Reemplazable r;
		r=GetComponent<Reemplazable> ();
		r.ReemplazarSinRotacion();
	}
	void ZetasOff()
	{
		zeta.SetActive (false);	
	}


	#region implemented abstract members of Tocable

	public override void Accionar (int accion)
	{
		if (accion == 1) 
		{
			//Dormirse
			Dormirse();
			Invoke("ZetasOff",3);
		}
		if (accion == 2) 
		{
			//Animarse
			Animarse();

		}


	}

	public override void Touched ()
	{

	}

	public override void UnTouched ()
	{

	}

	#endregion
}
