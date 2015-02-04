using UnityEngine;
using System.Collections;

/// <summary>
/// Esta clase representa los objetos que se pueden tocar con el touch
/// </summary>
public abstract class Touchable : MonoBehaviour {


	// Update is called once per frame
	protected void Update () 
	{
		if(Touched())
		{
			ManejarToque();
		}
		
	}
	
	
	protected bool Touched ()
	{
		return TouchHelper.ObjetoTocado == this.gameObject || TouchHelper.ObjetoTocado == this.transform.parent.gameObject;
	}
	
	
	protected void OnMouseDown()
	{
		//Machetico para saber si ya se ha tocado con el Touch
		if(Input.touchCount > 0)
		{
			//Se ha usado el Touch, entonces no haga nada
			return;
		}
		else
		{
			ManejarToque();
		}

		
	}

	protected abstract void ManejarToque();
}
