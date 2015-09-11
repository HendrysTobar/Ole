using UnityEngine;
using System.Collections;

public class EscenaPaginaCuatro : Escena {

	EscenaPaginaCuatro instancia;
	public GameObject canvasCuadro;
	public SimpleDialogCall dialogoFin;

	new void  Start()
	{
		instancia = this;
		SetInstance(this);
	}

	public void Exit()
	{
#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
#endif
		Application.Quit();
	}



	public override void TerminarEscena (bool gano)
	{
		dialogoFin.manualStart();
		canvas.SetActive(false);
		canvasCuadro.SetActive(true);
		imageTarget.SetActive(false);

		ActivarPasaPagina();
	}

}
