
using UnityEngine;
using System.Collections;


public abstract class Escena: MonoBehaviour
{
	public Elfo elfo;
	public GameObject imageTarget;
	public GameObject canvas;
	public VerificadorFinDeJuego verificador;
	public static Escena instance;

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

	public void DesactivarTodo()
	{
		if(HayDialogosActivos())
		{
			DesactivarDialogos();
			return;
		}
		else
		{
			DesactivarMovimiento();
			DesactivarCanvas();
			DesactivarImageTarget();
			DesactivarObjetosAR();
			DesactivarDialogos();
		}
	}

	public void ActivarTodo()
	{
		if(HayDialogosActivos())
		{
			ActivarDialogos();
		}
		else
		{
			ActivarSinCanvas();
			ActivarCanvas();
			ActivarImageTarget();
			ActivarObjetosAR();
			ActivarDialogos();
		}

	}

	void DesactivarImageTarget ()
	{
		if(imageTarget!= null)
			imageTarget.SetActive(false);
	}

	void ActivarImageTarget ()
	{
		if(imageTarget!= null)
			imageTarget.SetActive(true);
	}

	public void DesactivarCanvas()
	{
		canvas.SetActive(false);	
	}

	public void ActivarCanvas()
	{
		canvas.SetActive(true);
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

	protected void SetInstance(Escena escena)
	{
		instance = escena;
	}

	public void DesactivarObjetosAR()
	{
		ObjetoAR[] objetos = GameObject.FindObjectsOfType<ObjetoAR>();
		foreach (var objetoAR in objetos) 
		{
			objetoAR.Desactivar();
		}

	}

	public void ActivarObjetosAR()
	{
		ObjetoAR[] objetos = GameObject.FindObjectsOfType<ObjetoAR>();
		foreach (var objetoAR in objetos) 
		{
			objetoAR.Activar();
		}
		
	}

	public void DesactivarDialogos()
	{
		SDialogController [] dialogos = GameObject.FindObjectsOfType<SDialogController>();
		foreach (var sDialogController in dialogos) 
		{
			sDialogController.active = false;
		}

	}

	public void ActivarDialogos()
	{
		SDialogController [] dialogos = GameObject.FindObjectsOfType<SDialogController>();
		foreach (var sDialogController in dialogos) 
		{
			sDialogController.active = true;
		}
		
	}

	bool HayDialogosActivos ()
	{
		return GameObject.FindObjectOfType<SDialogController>() != null;
	}

	protected void ActivarPasaPagina()
	{
		GameObject pasaPagina = GameObject.Find("PasaLaPagina");
		if(pasaPagina != null)
		{
			pasaPagina.GetComponent<UnityEngine.UI.Text>().enabled = true;

		}
	}

	public abstract void TerminarEscena(bool gano);
}