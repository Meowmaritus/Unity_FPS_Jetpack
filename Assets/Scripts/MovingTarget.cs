using UnityEngine;
using System.Collections;

public class MovingTarget : MonoBehaviour {
	
	Vector3 center;
	public float radius = 20;
	public float speed = 7;
	
	void Start () {
		center = transform.position;
	}
	
	void Update () {
		// can't do this in C#   
		//transform.position.x += speed*Time.deltaTime;
		//have to do this instead!
		transform.position += new Vector3(speed*Time.deltaTime,0,0);
		if (transform.position.x > center.x+radius || transform.position.x < center.x-radius) {
			transform.position -= new Vector3(speed*Time.deltaTime,0,0);
			speed = -speed;
		}
	}
}
