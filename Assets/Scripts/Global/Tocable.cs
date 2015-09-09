using UnityEngine;
using System.Collections;


/// <summary>
/// Esta clase representa los objetos que el elfo puede tocar
/// </summary>
public abstract class Tocable : MonoBehaviour {
	private Transform flarePosition;
	public enum EstadoTocable{SinAsignar, Correcto, Incorrecto};
	public EstadoTocable estado = EstadoTocable.SinAsignar;

	// Use this for initialization
	protected void Start () 
	{

	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void SwitchFlare (bool isBeingTouched)
	{
		if(flarePosition == null)
			flarePosition = transform.Find("Flare");
		if(flarePosition != null)
			flarePosition.gameObject.SetActive(isBeingTouched);
	}

	private bool isBeingTouched;
	public bool IsBeingTouched
	{
		set
		{
			isBeingTouched = value;
			SwitchFlare(isBeingTouched);
			if(isBeingTouched)
			{
				Touched();
			}
			else
			{
				UnTouched();
			}
		}
		get
		{
			return isBeingTouched;
		}
	}


	public void Accionar()
	{
		Accionar(1);
	}
	public abstract void Accionar(int accion);
	public abstract void Touched();
	public abstract void UnTouched();
}
