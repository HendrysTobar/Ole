using UnityEngine;
using System.Collections;

public class EscenaPaginaUno : Escena {

	public GameObject imageTarget;
	public GameObject canvas;
	public VerificadorFinDeJuego verificador;

	public SimpleDialogCall dialogoGanaste;
	public SimpleDialogCall dialogoIntenta;

	// Use this for initialization
	void Start () {
		Camera.main.orthographic = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void PonerCamaraPerspectiva(){
		Camera.main.orthographic = false;
		imageTarget.SetActive(true);
		canvas.SetActive (true);



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
		PonerCamaraPerspectiva();
	}

	public void ActivarYRecalcular()
	{
		PonerCamaraPerspectiva();
		RecalcularVerificador();

	}

	private void RecalcularVerificador()
	{
		//Esto lo pongo para que el verificador recalcule los objetos de niño
		//porque como al principio estan inactivos no los encuentra.
		VerificadorFinDeJuego.Reset();
		verificador.CalcularTotalItems();
	}

	public override void TerminarEscena(bool gano)
	{
		if(gano)
		{
			//Activar dialogo ganar
			Desactivar();
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
	public abstract void TerminarEscena(bool gano);
}



