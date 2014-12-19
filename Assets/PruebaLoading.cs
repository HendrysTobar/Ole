using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PruebaLoading : MonoBehaviour {

	public Text textoCargando;
	// Use this for initialization
	void Start () 
	{
		StartCoroutine(Load());
	
	}


	IEnumerator Load()
	{

		textoCargando.gameObject.SetActive(true);
		yield return new WaitForEndOfFrame();

		Application.LoadLevel(1);
	}
	// Update is called once per frame
	void Update () {
	
	}
}
