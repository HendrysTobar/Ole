using UnityEngine;
using System.Collections;

public class Elfo : MonoBehaviour {

	public ParticleSystem polvosMagicos;
	public GameObject sombrillaNegra;
	public GameObject sombrillaBlanca;
	public AudioClip shimmer;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}

	public void LanzarPolvosMagicos()
	{
		//Instanciar Polvos Magicos
		polvosMagicos.Play();
		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);

		if(isTouchingBoy)
		{
			//Poner a dormir al niño ;)

			Boy b = ninyo.GetComponent<Boy>();
			b.Dormirse();
			isTouchingBoy = false;
		

		}
	}

	public void LanzarSombrilla(bool negra)
	{
		GameObject sombrilla;

		#region Codigo Eduardo
		//Si es negra
		if (negra == true) 
		{
			//Entonces poner sombrilla negra
			sombrilla = Instantiate(sombrillaNegra,polvosMagicos.transform.position, sombrillaNegra.transform.rotation) as GameObject;
		}
		//Sino
		else
		{
			///Enonces poner Sombrilla blanca
			sombrilla = Instantiate(sombrillaBlanca,polvosMagicos.transform.position, sombrillaBlanca.transform.rotation) as GameObject; 
		}
		#endregion
		#region Codigo Hendrys
		//Si no esta tocando la cama
		if(!isTouchingBed)
			//La sombrilla se destruira en un segundo
			Destroy(sombrilla, 1.0f);
		//De otro modo...
		else
		{

			//Poner la sombrilla en el gancho de la cama
			CamaConNinyo camaConNinyoObjeto  =  camaConNinyo.GetComponent<CamaConNinyo>();
			Transform gancho = camaConNinyoObjeto.gancho;
			//Si el gancho ya tiene hijos..
			if(gancho.childCount > 0)
			{
				//Destruir su hijo
				Destroy(gancho.GetChild(0).gameObject);
				VerificadorFinDeJuego.DecrementarItemsAsignados();
				if(camaConNinyoObjeto.EstaCorrectamenteAsignado)
					VerificadorFinDeJuego.DecrementarItemsCorrectos();
			}

			if(negra)
				camaConNinyoObjeto.sombrillaAsignada = CamaConNinyo.EstadoSombrilla.Negra;
			else
				camaConNinyoObjeto.sombrillaAsignada = CamaConNinyo.EstadoSombrilla.Blanca;

			sombrilla.transform.position = gancho.transform.position;

			sombrilla.transform.parent = gancho;

			//Llamar al verificador pues se ha colocado una somrbilla
			//Una vez se ha puesto la sombrilla, se verifica si se ha asignado correctamente
			if(camaConNinyoObjeto.sombrillaAsignada == CamaConNinyo.EstadoSombrilla.Negra && !camaConNinyoObjeto.EsBueno
			   ||
			   camaConNinyoObjeto.sombrillaAsignada == CamaConNinyo.EstadoSombrilla.Blanca && camaConNinyoObjeto.EsBueno)
			{
				VerificadorFinDeJuego.IncrementarItemsCorrectos();
			}
			VerificadorFinDeJuego.IncrementarItemsAsignados();







		}
		#endregion



		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);
		//VerificadorFinDeJuego.Log();


	}
	bool isTouchingBoy = false;
	bool isTouchingBed = false;
	private GameObject ninyo;
	private GameObject camaConNinyo;
	void OnTriggerEnter(Collider c)
	{
		if(c.collider.CompareTag("Ninyo"))
		{
			isTouchingBoy = true;
			ninyo = c.transform.parent.gameObject;
		}

		if(c.gameObject.CompareTag("CamaConNinyo"))
		{
			isTouchingBed = true;
			camaConNinyo = c.transform.parent.gameObject;
		}
	}

	void OnTriggerExit(Collider c)
	{
		if(c.collider.CompareTag("Ninyo"))
		{
			isTouchingBoy = false;
			ninyo = null;
		}
		if(c.gameObject.CompareTag("CamaConNinyo"))
		{
			isTouchingBed = false;
			camaConNinyo = null;
		}
		

	}


}
