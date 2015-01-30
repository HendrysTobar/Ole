using UnityEngine;
using System.Collections;

public class EscenaPaginaDos : MonoBehaviour {

	private Elfo elfo;
	public GameObject world;
	private int numeroDeArboles = 0;
	public SimpleDialogCall dialogoCambio;
	// Use this for initialization
	void Start () 
	{
		elfo = FindObjectOfType<Elfo>();
		if(elfo == null)
		{
			Debug.LogWarning("No se encuentra el objeto elfo en la escena");
			return;
		}

		elfo.OnObjetoAccionado += ContarArboles;
	
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
		anim = world.GetComponent<Animator>();
		anim.Play(ANIM_NAME);
		StartCoroutine("DesplegarDialogo");

	}

	IEnumerator DesplegarDialogo()
	{
		yield return new WaitForSeconds(0.3f);
		while(anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f)
		{
			yield return null;
		}
		dialogoCambio.manualStart();
		anim.speed = 0;
	}



}



















































