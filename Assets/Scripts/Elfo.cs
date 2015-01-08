using UnityEngine;
using System.Collections;

public class Elfo : MonoBehaviour {

	public ParticleSystem polvosMagicos;
	public AudioClip shimmer;
	// Use this for initialization
	void Start () {

	
	}
	
	// Update is called once per frame
	void Update () 
	{
			
	}

	public void LanzarPolvosMagicos()
	{
		//Instanciar Polvos Magicos
		polvosMagicos.Play();
		//Reproducir Sonido de Chispas
		audio.PlayOneShot(shimmer);

		if(isTouchingBoy)
		{
			//Poner a dormir al niño ;)

			Boy b = ninyo.GetComponent<Boy>();
			b.Dormirse();
		

		}
	}

	bool isTouchingBoy = false;
	private GameObject ninyo;
	void OnTriggerEnter(Collider c)
	{
		if(c.collider.CompareTag("Ninyo"))
		{
			isTouchingBoy = true;
			ninyo = c.transform.parent.gameObject;
		}
	}

	void OnTriggerExit(Collider c)
	{
		isTouchingBoy = false;
		ninyo = null;
	}


}
