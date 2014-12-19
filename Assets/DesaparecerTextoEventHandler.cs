using UnityEngine;
using System.Collections;

public class DesaparecerTextoEventHandler : MonoBehaviour, ITrackableEventHandler {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	#region ITrackableEventHandler implementation

	public void OnTrackableStateChanged (TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
	{
		// si el trackeable aparece
			

		// desaparecer texto
		// si trackeable desaparece
		// Aparecer Texto
	}

	#endregion
}
