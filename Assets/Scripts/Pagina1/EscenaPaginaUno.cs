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
	}







}

public abstract class Escena: MonoBehaviour
{
	public Elfo elfo;
	public GameObject imageTarget;
	public GameObject canvas;
	public VerificadorFinDeJuego verificador;

	public void PonerCamaraPerspectiva(){
		Camera.main.orthographic = false;
		
	}
	
	void PonerCamaraOrtogonal ()
	{
		Camera.main.orthographic = true;
		imageTarget.SetActive(false);
		canvas.SetActive (false);
	}
	
	public void Desactivar()
	{
		PonerCamaraOrtogonal();
	}
	public void Activar()
	{
		ActivarSinCanvas();
		canvas.SetActive (true);

	}

	public void ActivarSinCanvas()
	{
		imageTarget.SetActive(true);
		if(elfo != null)
			elfo.GetComponent<MovimientoPersonaje>().ActivarMovimiento();
	}
	
	public void ActivarYRecalcular()
	{
		Activar();
		RecalcularVerificador();
		
	}
	
	private void RecalcularVerificador()
	{
		//Esto lo pongo para que el verificador recalcule los objetos de niño
		//porque como al principio estan inactivos no los encuentra.
		VerificadorFinDeJuego.Reset();
		verificador.CalcularTotalItems();
	}

	public void TerminarEscena()
	{
		TerminarEscena(true);
	}

	public void DesactivarMovimiento()
	{
		if(elfo != null)
		{
			elfo.GetComponent<MovimientoPersonaje>().DesactivarMovimiento();
		}
	}

	protected void ActivarSiguienteEscena()
	{
		PaginasManager.Singleton.ActivarSiguiente(Application.loadedLevelName);
	}


	public abstract void TerminarEscena(bool gano);
}



