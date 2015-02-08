using UnityEngine;
using System.Collections;

public class EscenaPaginaCuatro : Escena {

	EscenaPaginaCuatro instancia;
	public GameObject canvasCuadro;
	public SimpleDialogCall dialogoFin;

	new void  Start()
	{
		instancia = this;
	}



	public override void TerminarEscena (bool gano)
	{
		dialogoFin.manualStart();
		canvas.SetActive(false);
		canvasCuadro.SetActive(true);
		imageTarget.SetActive(false);
	}

}
