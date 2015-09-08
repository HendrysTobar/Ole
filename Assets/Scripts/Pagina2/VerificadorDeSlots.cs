using UnityEngine;
using System.Collections;

public class VerificadorDeSlots : MonoBehaviour {

	Slot [] slots;

	// Use this for initialization
	void Start () {

	}
	bool hayFruta = false;
	bool hayPastel = false;
	bool hayFlor = false;
	bool hayOtraCosa = false;
	/// <summary>
	/// Verificar si hay en el arbol al menos una fruta, un paste y una flor
	/// <returns>
	/// true en caso afirmativo, false de lo contrario o si hay presente otro tipo de adorno
	/// </returns>
	/// </summary>
	public bool EsCorrecto()
	{
		if(slots == null)
		{
			slots = GetComponentsInChildren<Slot>();
		}

		hayFruta = false;
		hayPastel = false;
		hayFlor = false;
		hayOtraCosa = false;

		foreach(Slot s in slots)
		{
			if(s.AdornoAsignado == TipoDeAdorno.Flor)
				hayFlor = true;
			else
				if(s.AdornoAsignado == TipoDeAdorno.Fruta)
					hayFruta = true;
				else
					if(s.AdornoAsignado == TipoDeAdorno.Pastel)
						hayPastel = true;
					else
						if(s.AdornoAsignado != TipoDeAdorno.Ninguno)
						{
							hayOtraCosa = true;
							return false;
						}

		}
		if(hayFruta && hayFlor && hayPastel)
		{

			return true;

		}
		return false;
	}

	public void Verificar()
	{
		EscenaPaginaDos escena = EscenaPaginaDos.instancia;
		if(EsCorrecto())
		{
			escena.TerminarEscena(true);
		}
		else
		{
			escena.TerminarEscena(false);
		}
	}
}
