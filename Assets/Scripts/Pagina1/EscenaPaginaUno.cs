using UnityEngine;
using System.Collections;

public class EscenaPaginaUno : Escena {

	public SimpleDialogCall dialogoGanaste;
	public SimpleDialogCall dialogoIntenta;

	//TODO: Esto deberia aceptar cualquier cantidad de niños
	//Pero por el momento toca machete
	private int NIÑOS_POR_DORMIR = 2;
	private int niñosDormidos;

	// Use this for initialization
	void Start () {
		Camera.main.orthographic = true;
		InitializeBoys();
		SetInstance(this);
	}

	void InitializeBoys ()
	{
		Boy[] boys = FindObjectsOfType<Boy>();
		foreach (var boy in boys) 
		{
			//boy.onDormido += NiñoDormido;
		}

	}

	void PonerDialogoExplicativo ()
	{

	}
	
	public void NiñoDormido(string tagObjeto)
	{
		if(tagObjeto == "Ninyo")
		{
			niñosDormidos++;
			if(niñosDormidos >= NIÑOS_POR_DORMIR)
			{
				PonerDialogoExplicativo();
			}
		}
	}


	public override void TerminarEscena(bool gano)
	{
		if(gano)
		{
			//Activar dialogo ganar
			Desactivar();
			ActivarSiguienteEscena();
			dialogoGanaste.manualStart();
		}
		else
		{
			//Activar Dialogo intenta
			Desactivar();
			dialogoIntenta.manualStart();
		}
		ActivarPasaPagina();
	}







}





