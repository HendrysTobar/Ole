using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

[RequireComponent(typeof(DragMe))]
public class AdornoGUIElement : MonoBehaviour,  IDragHandler, IEndDragHandler {

	RaycastHelper rh;
	private bool apuntando = false;
	private GameObject slotApuntado;
	DragMe dragMe;
	public TipoDeAdorno tipo;

	void Start()
	{
		rh = new RaycastHelper(EscenaOle.arCamera);
		dragMe = GetComponent<DragMe>();

	}

	#region IDragHandler implementation


	public void OnDrag (PointerEventData eventData)
	{

		RaycastHit hit;
		//Si un rayo desde la posicion del mouse golpea un slot, esto es un 
		//espacio donde se almacenan los adornos en la escena
		if(rh.RayCastHitsTag(Input.mousePosition, "Slot",out hit ))
		{
			if(!apuntando)
			{
				AgrandarIcono();
			}
			//Entonces, establecer el estado a "apuntando"
			apuntando = true;
			slotApuntado = hit.collider.gameObject;
		}
		//Sino
		else
		{
			//Entonces si ya se estaba apuntando resetear la escala del icono
			if(apuntando)
			{
				ResetearIcono();
				apuntando = false;
				slotApuntado = null;
			}
		}


	}

	void AgrandarIcono ()
	{
		dragMe.EnlargeIcon();
	}

	void ResetearIcono ()
	{
		dragMe.ResetIconScale();
	}

	#endregion

	#region IEndDragHandler implementation

	public void OnEndDrag (PointerEventData eventData)
	{
		if(apuntando)
		{
			Sprite s = this.GetComponent<UnityEngine.UI.Image>().sprite;
			slotApuntado.GetComponent<Slot>().RecibirAdorno(tipo, s);
		
		}
	}

	#endregion
}

public enum TipoDeAdorno {Ninguno, Flor, Fruta, Pastel, Botella, Estrella, Sombrilla };
