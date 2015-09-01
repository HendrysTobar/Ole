using UnityEngine;
using System.Collections;

public class DesaparecerTextoEventHandler : MonoBehaviour, ITrackableEventHandler {

	public UnityEngine.UI.Text texto;
	#region PRIVATE_MEMBER_VARIABLES
	
	private TrackableBehaviour mTrackableBehaviour;
	
	#endregion // PRIVATE_MEMBER_VARIABLES
	
	
	

	
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
			DesaparecerTexto();
		}
		else
		{
			AparecerTexto();
		}


	}


	void DesaparecerTexto ()
	{
		texto.enabled = false;

	}

	void AparecerTexto ()
	{
		texto.enabled = true;
	}
	#endregion
}
