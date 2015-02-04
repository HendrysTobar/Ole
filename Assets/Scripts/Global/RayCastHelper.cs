using UnityEngine;

public class RaycastHelper
{
	private readonly Camera _camera;
	
	// Constructeur
	public RaycastHelper(Camera camera)
	{
		_camera = camera;
	}
	
	public bool GetCameraRayCastInfos(Vector2 input, out RaycastHit hit)
	{
		var ray = _camera.ScreenPointToRay(input);
		hit = new RaycastHit();
		
		if(Physics.Raycast(ray, out hit, Mathf.Infinity))
			return true;
		
		return false;
	}

	public bool RayCastHitsTag(Vector2 input, string tag, out RaycastHit hit)
	{

		if(GetCameraRayCastInfos(input, out hit))
		{
			if(hit.collider.gameObject.tag == tag)
			{
				return true;
			}
		}
		return false;
	}
}