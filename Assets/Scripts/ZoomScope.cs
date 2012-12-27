using UnityEngine;
using System.Collections;

public class ZoomScope : MonoBehaviour {

	// Use this for initialization
	void Start () {
		camera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(1))
			camera.enabled = true;
		else
			camera.enabled = false;
	}
}
