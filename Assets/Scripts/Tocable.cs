using UnityEngine;
using System.Collections;

public abstract class Tocable : MonoBehaviour {
	private Transform flarePosition;

	// Use this for initialization
	protected void Start () 
	{
		flarePosition = transform.Find("Flare");
	}
	
	// Update is called once per frame
	void Update () {
	
	}



	void SwitchFlare (bool isBeingTouched)
	{
		flarePosition.gameObject.SetActive(isBeingTouched);
	}

	private bool isBeingTouched;
	public bool IsBeingTouched
	{
		set
		{
			isBeingTouched = true;
			SwitchFlare(isBeingTouched);
			if(isBeingTouched)
			{
				Touched();
			}
			else
			{
				UnTouched();
			}
		}
		get
		{
			return isBeingTouched;
		}
	}

	public abstract void Accionar();
	public abstract void Touched();
	public abstract void UnTouched();
}
