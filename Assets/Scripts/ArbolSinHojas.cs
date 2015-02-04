using UnityEngine;
using System.Collections;

public class ArbolSinHojas : Touchable 
{
	new void Update()
	{
		base.Update();
	}

	new void OnMouseDown()
	{
		if(this.enabled == true)
			base.OnMouseDown();
	}


	#region implemented abstract members of Touchable
	protected override void ManejarToque ()
	{
		EscenaPaginaDos.instancia.PonerArbol();
	}
	#endregion
}
