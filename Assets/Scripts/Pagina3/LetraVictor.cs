using UnityEngine;
using System.Collections;

public class LetraVictor : Tocable
{

	public GameObject globo;
	public GameObject _default;

	private string currentAnim = null;

	void TemblarYGritar ()
	{
		globo.SetActive(true);
		Animator anim = _default.GetComponent<Animator>();
		anim.enabled = true;
		currentAnim = "Temblequeo";
		anim.Play(currentAnim);

	}

	void HacerEjercicio ()
	{
		globo.SetActive(false);
		Animator anim = _default.GetComponent<Animator>();
		anim.enabled = true;
		currentAnim = "Ejercicio";
		anim.Play(currentAnim);
	}

	void OnEnable()
	{
		Animator anim = _default.GetComponent<Animator>();
		if(!string.IsNullOrEmpty(currentAnim))
		{

			anim.enabled = true;
			anim.Play(currentAnim);

		}
		else
		{
			anim.enabled = false;
		}
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
