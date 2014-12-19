﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ObjetoAR : MonoBehaviour {

	public string nombre;
	public Text textComponent;
	private AudioSource[] audios;
	// Use this for initialization
	void Start () 
	{
		audios = GetComponents<AudioSource>();
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
			Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
			RaycastHit hitInfo;
			//Si el rayo golpea algo
			if(Physics.Raycast(ray, out hitInfo))
			{
				if(hitInfo.collider.gameObject == this.gameObject)
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
		Debug.Log("Hola");
		ToggleName();

	}


	void ToggleName ()
	{

		Text t = textComponent;
		Color c = t.color;
		c.a = c.a < 255?255:0;
		t.color = c;

		PlaySound(t.color.a);

	}
	

	void PlaySound (float a)
	{
		if(audios.Length > 0)
		{
			if(a < 255)
				audios[0].Play();
			else
				audios[1].Play();




		}


	}
}
