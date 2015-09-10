using UnityEngine;
using System.Collections;

public class MacetaTutorial :Tocable {
	public GameObject mano;
	public GameObject canvasBotones;
	public GameObject manoGUI;
	public GameObject macetaConFlores;
	// Use this for initialization
	new void Start () {
		base.Start();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region implemented abstract members of Tocable
	public override void Accionar (int accion)
	{
		/*Eliminar la maceta esta
		 * Y poner la maceta con flores
		 * Y eliminar el inidicador de la mano de la GUI
		*/

		GameObject i = Instantiate(macetaConFlores, this.transform.position, this.transform.rotation) as GameObject;
		i.transform.parent = this.transform.parent;
		manoGUI.SetActive(false);
		Destroy (this.gameObject);

		if(onAccionado != null)
			onAccionado();
	}



	public override void Touched ()
	{
		Destroy(mano);
		canvasBotones.SetActive(true);

	}

	public override void UnTouched ()
	{

	}


	#endregion

	#region Eventos
	public System.Action onAccionado;
	#endregion


}
