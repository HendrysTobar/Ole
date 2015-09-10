using UnityEngine;
using System.Collections;
using System;

public class Elfo : MonoBehaviour {

	public ParticleSystem polvosMagicos;
	public ParticleSystem Gotas;
	public GameObject sombrillaNegra;
	public GameObject sombrillaBlanca;
	public GameObject purgante;
	public GameObject gym;
	public AudioClip shimmer;

	#region Singleton
	public static Elfo instancia;
	#endregion


	#region Eventos
	public delegate void TocableAccionadoHandler(string tagObjeto);
	public TocableAccionadoHandler OnObjetoAccionado;
	#endregion


	// Use this for initialization
	void Start () {
		instancia = this;
	
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}

	#region Metodos del Elfo

	void UsarVerificador (TipoVerificacion v)
	{
		if(objetoTocable == null)
			return;
		//Si esta verificacion es la misma que el objeto ya tiene...
		bool esElMismo = objetoTocable.estado == Tocable.EstadoTocable.Correcto && v== TipoVerificacion.correcto ||
			objetoTocable.estado == Tocable.EstadoTocable.Incorrecto && v == TipoVerificacion.incorrecto;
		if(!esElMismo)
		{

			if(v != TipoVerificacion.ignorar)
			{
				if(objetoTocable.estado != Tocable.EstadoTocable.SinAsignar)
				{
					VerificadorFinDeJuego.DecrementarItemsAsignados();
					if(objetoTocable.estado == Tocable.EstadoTocable.Correcto)
					{
						VerificadorFinDeJuego.DecrementarItemsCorrectos();
					}
				}
				if(v == TipoVerificacion.correcto)
				{
					objetoTocable.estado = Tocable.EstadoTocable.Correcto;
					VerificadorFinDeJuego.IncrementarItemsCorrectos();
				}
				objetoTocable.estado = Tocable.EstadoTocable.Incorrecto;
				VerificadorFinDeJuego.IncrementarItemsAsignados();
			}
		}
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
		if(isTouchingSomething)
		{
			//accionar Objeto touchable ;
			if(objetoTocable != null)
				objetoTocable.Accionar(1);						
		}
	}
	public void LanzarGotas()
	{
		//Instanciar Polvos Magicos
		Gotas.Play();
		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);
		

		if(isTouchingSomething)
		{
			//accionar Objeto touchable ;
			if(objetoTocable != null)
				objetoTocable.Accionar(2);	

			UsarVerificador(TipoVerificacion.correcto);
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

			//si es la misma sombrilla que ya esta puesta
			bool mismaSombrilla = camaConNinyoObjeto.sombrillaAsignada == CamaConNinyo.EstadoSombrilla.Negra && negra == true ||
				camaConNinyoObjeto.sombrillaAsignada == CamaConNinyo.EstadoSombrilla.Blanca && negra == false;
			if(mismaSombrilla)
				Destroy(sombrilla);
			if(!mismaSombrilla)
			{
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


		}
		#endregion



		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);
		VerificadorFinDeJuego.Log();


	}

	public void UsarManoMagica()
	{
		//Si esta tocando maceta
		if(maceta != null)
		{

			Reemplazable r = maceta.GetComponent<Reemplazable>();
			if(r!= null)
				r.Reemplazar();
			else
				Debug.LogWarning("La maceta no tiene script 'Reemplazable'");

			if(OnObjetoAccionado!= null)
				OnObjetoAccionado(maceta.tag);

			maceta = null;
			isTouchingMaceta = false;
			//Reproducir Sonido de Chispas
		}
		audio.PlayOneShot(shimmer);


	}

	void AccionarObjetoTocable()
	{
		//Esta validacion es necesaria porque si por alguna razon no hay objeto tocabl
		//no se puede continuar
		if(objetoTocable == null)
		{
			Debug.LogWarning("No hay objeto tocable al cual aplicar esta accion");
			return;
		}
		//.. De lo contrario
		else
		{
			objetoTocable.Accionar();
		}



	}

	public void UsarManoMagicaTutorial()
	{
		AccionarObjetoTocable();
		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);
	}

	public void UsarPurgante()
	{
		DesplegarYAccionar(purgante, "LetraVictor", TipoVerificacion.incorrecto,1);
	}


	private enum TipoVerificacion{ignorar, correcto, incorrecto};
	/// <summary>
	/// Este es un metodo generico. Lo que hace es poner una copia del objeto <paramref name="icono"/>
	/// Y acciona el objetoTocable con la <param name="accion">si lo hay y tiene el tag <paramref name="tagObjeto"/>
	/// La variable <paramref name="v"/> sirve para saber si se reporta al verificador un punto correcto, incorrecto o si se ignora
	/// </summary>
	/// <param name="icono">El Objeto a poner de Icono en las manos del elfo. Es un prefab </param>
	/// <param name="tagObjeto">El tag que debe tener el objeto a accionarse </param>
	/// <param name="v">El tipo de verificacion que se debe enviar al verificador de fin de juego</param>
	/// <param name="accion">La accion a invocar en elobjeto tocable. Opcional quiere decir que se llama la accion por defecto</param>
	private void DesplegarYAccionar(GameObject icono,string tagObjeto, TipoVerificacion v, int accion = 0)
	{
		//Poner el icono del purgante
		GameObject p = Instantiate(icono, polvosMagicos.transform.position, icono.transform.rotation) as GameObject;
		p.transform.parent = this.transform;
		p.transform.localScale = purgante.transform.localScale;
		p.transform.localRotation = icono.transform.localRotation;
		Destroy (p, 1.0f);
		//Si esta tocando algo	
		if(isTouchingSomething)
		{
			if(objetoTocable.CompareTag(tagObjeto))
			{
				if(accion == 0)
					objetoTocable.Accionar();
				else
					objetoTocable.Accionar(accion);
				UsarVerificador(v);

			}
			
		}
		
		audio.PlayOneShot(shimmer);
	}

	public void PonerAHacerEjercicio()
	{
		DesplegarYAccionar(this.gym, "LetraVictor", TipoVerificacion.correcto,2);
	}


	#endregion


	bool isTouchingBoy = false;
	bool isTouchingBed = false;
	bool isTouchingMaceta = false;
	bool isTouchingSomething = false;
	private GameObject ninyo;
	private GameObject camaConNinyo;
	private GameObject maceta;

	private GameObject something;
	Tocable objetoTocable;
	void OnTriggerEnter(Collider c)
	{

		//El objeto "something" es un puente entre la version anterior que no usaba la clase "Tocable" y la nueva version.
		//El objeto something representa el objeto tocado de ambas versiones.
		if(c.collider.CompareTag("Ninyo"))
		{
			isTouchingBoy = true;
			something = ninyo = c.transform.parent.gameObject;
		}

		if(c.gameObject.CompareTag("CamaConNinyo"))
		{
			isTouchingBed = true;
			something = camaConNinyo = c.transform.parent.gameObject;
		}
		if(c.gameObject.CompareTag("Maceta"))
		{
			isTouchingMaceta = true;
			something = maceta = c.transform.parent.gameObject;
		}
		///Genralizacion del proceso de identificar un objeto que se puede tocar
		if(c.gameObject.CompareTag("Tocable"))
		{
			isTouchingSomething = true;
			something = c.transform.parent.gameObject;
		}
		//Si algo se esta tocando, entonces marcarlo como Tocado para que aparezca el flare
		if(something != null)
		{
			objetoTocable = something.GetComponent<Tocable>();
			if(objetoTocable == null)
			{
				Debug.LogWarning("El obeto tocable no contiene un script Tocable");
			}
			else
			{
				objetoTocable.IsBeingTouched = true;
			}
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
			isTouchingMaceta = false;
			maceta = null;
		}

		if(c.gameObject.CompareTag("Maceta"))
		{
			isTouchingMaceta = false;
			maceta = null;
		}
		///Genralizacion del proceso de identificar un objeto que se puede tocar
		if(c.gameObject.CompareTag("Tocable") || something != null)
		{
			isTouchingSomething = false;
			something = null;
			objetoTocable.IsBeingTouched = false;
			objetoTocable = null;


		}		

	}
	/// <summary>
	/// Hace que el elfo vaya a la posicion indicada
	/// Esto no hace que se dirija inmediatamente sino que establece la posicion indicada como el destino actual del 
	/// script de MOvimientoPersonaje
	/// </summary>
	/// <param name="position">Position.</param>
	public void GoTo (Vector3 position)
	{
		GetComponent<MovimientoPersonaje>().GoTo(position);
	}

}
