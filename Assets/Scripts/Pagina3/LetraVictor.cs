using UnityEngine;
using System.Collections;

public class LetraVictor : Tocable
{

	public GameObject globo;
	public GameObject _default;

	void TemblarYGritar ()
	{
		globo.SetActive(true);
		Animator anim = _default.GetComponent<Animator>();
		anim.enabled = true;
		anim.Play("Temblequeo");

	}

	void HacerEjercicio ()
	{
		globo.SetActive(false);
		Animator anim = _default.GetComponent<Animator>();
		anim.enabled = true;
		anim.Play("Ejercicio");
	}

	#region implemented abstract members of Tocable
	public override void Accionar (int accion)
	{
		if(accion == 1)
		{
			TemblarYGritar();
		}
		if(accion == 2)
		{
			HacerEjercicio();
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
