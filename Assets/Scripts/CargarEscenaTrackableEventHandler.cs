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
		textoCargando.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();
		Application.LoadLevel(this.name);

	}


	#endregion
}
