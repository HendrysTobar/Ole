using UnityEngine;
using System.Collections;

/// <summary>
/// Esta clase representa los objetos que se pueden tocar con el touch
/// </summary>
public class Touchable : MonoBehaviour {


	// Update is called once per frame
	void Update () 
	{
		
	}
	
	
	protected bool Touched ()
	{
		return TouchHelper.ObjetoTocado == this.gameObject || TouchHelper.ObjetoTocado == this.transform.parent.gameObject;
	}
	
	
	void OnMouseDown()
	{
		//Machetico para saber si ya se ha tocado con el Touch
		if(Input.touchCount > 0)
		{
			//Se ha usado el Touch, entonces no haga nada
			return;
		}

		
	}
}
