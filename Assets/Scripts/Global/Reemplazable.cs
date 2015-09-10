using UnityEngine;
using System.Collections;

/// <summary>
/// Este script es para objetos que se reemplazan por otros.
/// Esto es, cuando se llama el metodo "Reemplazar" el objeto es cambiado por el prefab 
/// "reemplazo".
/// </summary>
public class Reemplazable : Tocable {

	public GameObject reemplazo;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Reemplazar()
	{
		GameObject i = Instantiate(reemplazo, this.transform.position, this.transform.rotation) as GameObject;
		i.transform.parent = this.transform.parent;
		i.SetActive (true);
		Destroy(this.gameObject);
	}

	public void ReemplazarSinRotacion()
	{
		GameObject i = Instantiate(reemplazo, this.transform.position, reemplazo.transform.rotation) as GameObject;
		i.transform.parent = this.transform.parent;
		i.SetActive (true);
		Destroy(this.gameObject);
	}

	#region implemented abstract members of Tocable

	public override void Accionar (int accion)
	{
		switch(accion)
		{
		case 1: Reemplazar(); break;
		case 2: ReemplazarSinRotacion(); break;
		default: Reemplazar();break;
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
