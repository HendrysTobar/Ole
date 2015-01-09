using UnityEngine;
using System.Collections;

public class VerificadorFinDeJuego : MonoBehaviour {

	/// <summary>
	/// El total del Items que este verificador cuenta.
	/// </summary>
	private static short totalItems;
	/// <summary>
	/// El total del Items que este verificador cuenta.
	/// </summary>
	public static  short TotalItems {
		get {
			return totalItems;
		}
	}
	/// <summary>
	/// Este verificador cuenta la cantidad de objetos que tengan este tag.
	/// Esa cantidad de objetos la asigna al totalItems.
	/// </summary>
	public  string tagCount;
	/// <summary>
	/// Este verificador cuenta la cantidad de objetos que tengan este tag.
	/// Esa cantidad de objetos la asigna al totalItems.
	/// </summary>
	public string TagCount {
		get {
			return tagCount;
		}
	}
	/// <summary>
	/// El total de Items que se han considerado correctos. 
	/// Esta variable debe irse actualizando usando el metodo IncrementarItemsCorrectos()
	/// </summary>
	private static short totalItemsCorrectos;
	/// <summary>
	/// El total de Items que se han considerado correctos. 
	/// Esta variable debe irse actualizando usando el metodo IncrementarItemsCorrectos()
	/// </summary>
	public static  short TotalItemsCorrectos {
		get {
			return totalItemsCorrectos;
		}
	}
	/// <summary>
	/// El total de Items que se han asignado sin considerar que esten correctos o no. 
	/// Esta variable debe irse actualizando usando el metodo IncrementarItemsAsignados()
	/// </summary>
	private static  short totalItemsAsignados;
	/// <summary>
	/// El total de Items que se han asignado sin considerar que esten correctos o no. 
	/// Esta variable debe irse actualizando usando el metodo IncrementarItemsAsignados()
	/// </summary>
	public static short TotalItemsAsignados {
		get {
			return totalItemsAsignados;
		}
	}


	// Use this for initialization
	void Start () 
	{
		Reset();
		CalcularTotalItems();
	}


	/// <summary>
	/// Calcula the total items.
	/// </summary>
	void CalcularTotalItems ()
	{
		if(string.IsNullOrEmpty(tagCount))
		{
			Debug.Log("El tag de conteo del verificador no ha sido asignado. El conteo de Items queda a 0");
			totalItems = 0;
			return;
		}
		totalItems = (short) GameObject.FindGameObjectsWithTag(tagCount).Length;

	}

	/// <summary>
	/// Incrementa el total de Items asignados en 1
	/// </summary>
	public static void IncrementarItemsAsignados()
	{
		totalItemsAsignados++;
		if(totalItemsAsignados == totalItems)
		{
			//Terminar el juego...
			if(totalItems == totalItemsCorrectos)
			{
				//Ganaste!!
				Debug.Log("Ganaste");
			}
			else
			{
				//Perdiste
				Debug.Log("Hay errores. Prueba de nuevo");
			}


		}
	}

	/// <summary>
	/// Decrementa el total de Items asignados en 1
	/// </summary>
	public static void DecrementarItemsAsignados()
	{
		totalItemsAsignados--;
	}

	/// <summary>
	/// Incrementa el total de Items Correctos en 1
	/// </summary>
	public static void IncrementarItemsCorrectos()
	{
		totalItemsCorrectos++;
	}
	
	/// <summary>
	/// Decrementa el total de Items Correctos en 1
	/// </summary>
	public static void DecrementarItemsCorrectos()
	{
		totalItemsCorrectos--;
	}

	private static void Reset()
	{
		totalItems = 0;
		totalItemsAsignados = 0;
		totalItemsCorrectos = 0;
	}

	public static void Log ()
	{
		Debug.Log("TotalItems: "+ totalItems + "\n" + "TotalItemsAignados: "+ totalItemsAsignados + "\n" + "TotalItemsCorrectos: " + totalItemsCorrectos);

	}
}