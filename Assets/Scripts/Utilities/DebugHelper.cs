//#define DEBUG_WITHOUT_AR
using UnityEngine;
using System.Collections;

/// <summary>
/// Esta clase es solo de ayuda, sirve para no tener que estar activando la camara de AR
/// </summary>
public class DebugHelper : MonoBehaviour {




	public GameObject debugCamera;
	public GameObject world;
	// Use this for initialization
	void Awake ()
	{
#if DEBUG_WITHOUT_AR
		if(Application.isEditor)
		{
			debugCamera.SetActive(true);
			world.transform.parent = null;
			world.SetActive(true);

		}
#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
