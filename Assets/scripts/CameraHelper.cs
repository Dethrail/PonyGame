using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class CameraHelper:MonoBehaviour
{
	public int Height;
	public int Width;

	public int PixelPerUnits = 20;

	public bool EnableOnUpdate = false;

	//private void Start()
	//{
	//	Height = Screen.height;
	//	Width = Screen.width;
	//	Camera.main.orthographicSize = (float)Screen.height / 2 / PixelPerUnits;
	//}

	private void Update()
	{
		if(EnableOnUpdate) {
			Height = Screen.height;
			Width = Screen.width;
			Camera.main.orthographicSize = (float)Screen.height / 2 / PixelPerUnits;
		}
	}
}
