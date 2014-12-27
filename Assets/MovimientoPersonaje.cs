using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CharacterController))]
public class MovimientoPersonaje : MonoBehaviour {

	CharacterController cc;
	public float speed;
	Plane groundPlane;
	Vector3 destination;
	// Use this for initialization
	void Start () 
	{
		cc = GetComponent<CharacterController>();
		groundPlane = new Plane(transform.parent.up, transform.parent.position);
		destination = transform.position;
	
	}
	
	// Update is called once per frame
	void Update () {

		Vector3 d = Capture3DClickPosition();
		if(d != Vector3.zero)
		{
			destination = d;
		}

		Vector3 vel = destination - transform.position;

		cc.SimpleMove(speed * vel.normalized * Time.deltaTime);

	
	}


	Vector3 Capture3DClickPosition ()
	{
		if (Input.GetMouseButtonDown(0)) 
		{
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			float rayDistance;
			if (groundPlane.Raycast(ray, out rayDistance))
			{
				return ray.GetPoint(rayDistance);
			}
			else
			{
				return Vector3.zero;
			}

		}
		else
		{
			return Vector3.zero;
		}

	}
}

