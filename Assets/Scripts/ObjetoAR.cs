using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjetoAR : MonoBehaviour {

	public string nombre;
	public GameObject textComponent;
	private AudioSource[] audios;

	private GameObject camara;
	// Use this for initialization
	void Start () 
	{
		audios = GetComponents<AudioSource>();
		camara = GameObject.Find ("ARCamera");
	}

	// Update is called once per frame
	void Update () 
	{
#if UNITY_ANDROID
		if(Touched())

		{
			OnMouseDown();
		}
#endif
	}


	bool Touched ()
	{
		if(Input.touchCount == 0)
			return false;

		Touch t = Input.GetTouch(0);
		if(t.phase == TouchPhase.Began)
		{
			Ray ray = camara.camera.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hitInfo;
			//Si el rayo golpea algo
			if(Physics.Raycast(ray, out hitInfo))
			{
				Debug.Log("Algo golpeo");
				if(hitInfo.collider.gameObject == this.gameObject || hitInfo.collider.gameObject == this.transform.parent.gameObject)
				{

					return true;
				}
			}
			return false;

			

			
		}
		return false;
	}


	void OnMouseDown()
	{

		ToggleName();

	}


	public void ToggleName ()
	{

		textComponent.SetActive(!textComponent.activeSelf);

		PlaySound(textComponent.activeSelf);

	}
	

	void PlaySound (bool a)
	{

		audios[textComponent.activeSelf?1:0].Play();


	}
}
