using UnityEngine;
using System.Collections;

public class EscenaPaginaDos : Escena {


	public GameObject worldVisible;
	public GameObject worldGrupo;
	public GameObject arbolCenador;
	private int numeroDeArboles = 0;
	public SimpleDialogCall dialogoCambio;
	public SimpleDialogCall dialogoExplicacionArbol;
	public SimpleDialogCall dialogoGanaste;
	public SimpleDialogCall dialogoIntenta;
	public GameObject canvas1;
	public GameObject canvas2;

	public static EscenaPaginaDos instancia;
	// Use this for initialization
	void Start () 
	{

		if(elfo == null)
		{
			Debug.LogWarning("No se encuentra el objeto elfo en la escena");
			return;
		}

		elfo.OnObjetoAccionado += ContarArboles;
		instancia = this;
		SetInstance(this);
	
	}
	



	void ContarArboles (string tagObjeto)
	{
		if(tagObjeto == "Maceta")
			numeroDeArboles++;
		if(numeroDeArboles == 3)
		{
			CambiarEscenario();
		}

	}

	Animator anim;
	private string ANIM_NAME = "FadeOut";
	void CambiarEscenario ()
	{
		//Desaparecer el mundo actual (reproducir la animacion, al final se destruye el objeto y al final se pone el cenador)
//		anim = worldVisible.GetComponent<Animator>();
//		anim.Play(ANIM_NAME);
		StartCoroutine("DesplegarDialogo");

		ArbolSinHojas [] arboles = FindObjectsOfType<ArbolSinHojas>();
		foreach(ArbolSinHojas a in arboles)
		{
			a.enabled = true;
		}

	}

	IEnumerator DesplegarDialogo()
	{
		yield return new WaitForSeconds(0.3f);

		dialogoCambio.manualStart();
		anim.speed = 0;
	}

	public void PonerArbol()
	{
		//worldGrupo.SetActive(false);
		//arbolCenador.SetActive(true);
		GameObject ac = Instantiate(arbolCenador, worldGrupo.transform.position, worldGrupo.transform.rotation) as GameObject;
		ac.transform.parent = worldGrupo.transform.parent;
		ac.transform.localScale =Vector3.one;

		Destroy(worldGrupo);


		canvas1.SetActive(false);
		canvas2.SetActive(true);
		dialogoExplicacionArbol.manualStart();
	}

	public void VerificarSlots()
	{
		VerificadorDeSlots vs = FindObjectOfType<VerificadorDeSlots>();
		if(vs != null)
			vs.Verificar();
	}



	#region implemented abstract members of Escena
	public override void TerminarEscena (bool gano)
	{

		if(gano)
		{
			dialogoGanaste.manualStart();
			ActivarSiguienteEscena();
			canvas2.SetActive(false);
		}
		else
		{
			dialogoIntenta.manualStart();
		}

	}
	#endregion


}



















































