using UnityEngine;
using System.Collections;

public class CargarEscenaTrackableEventHandler : MonoBehaviour, ITrackableEventHandler{

	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour mTrackableBehaviour;

	#endregion // PRIVATE_MEMBER_VARIABLES
	public UnityEngine.UI.Text textoCargando;
	
	
	
	
	void Start()
	{

		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ITrackableEventHandler implementation

	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		//Si el trackable aparece
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			Debug.Log("El Trackable " + mTrackableBehaviour.TrackableName + " Se ha encontrado");
			StartCoroutine(CargarEscena());
		}
	}


	IEnumerator CargarEscena ()
	{
		SDialogManager.skipAllCurrentDialogs();
		Debug.Log("Cargando escena " + this.name );
		if(textoCargando)
			textoCargando.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();

		//Para cargar a nueva escena se ve si se puede cargar la escena o si se debe cargar la escena del candado
		//tomamos el numero de la escena
		//El numero de la escena debe estar en el nombre del Image Tracker
		int numPagina;
		if(!int.TryParse(this.name[this.name.Length - 1].ToString(), out numPagina ))
		{
			Debug.Log("No se conoce el numero de la pagina para cargar el nivel del candado");
		}
		else
		{
			//Si la pagina esta activada cargar la pagina
			if(PaginasManager.Singleton.EstaActivada(numPagina))
				Application.LoadLevel(this.name);
			//Sino, cargar la pagina del candado
			else
			{
				EscenaPaginaLocked.paginaQueSeIntentoCargar = this.name;
				Application.LoadLevel("PaginaLocked");
			}
			yield return null;
		}
		//Si no se obtuvo el numero de la pagina simplemente no considera la pagina del candado
		Application.LoadLevel(this.name);
		yield return null;

	}


	#endregion
}
