using UnityEngine;
using System.Collections;

public class GravityCenter : MonoBehaviour {
	public bool gravityOn = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnCollisionEnter(Collision other) {
		gravityOn = true;
	}
}
