#pragma strict

var center : Vector3;
var radius : float = 10;
var speed : float = 1;

function Start () {
	center = transform.position;
}

function Update () {
	//Debug.Log(center);
	transform.position.x += speed*Time.deltaTime;
	if (transform.position.x > center.x+radius || transform.position.x < center.x-radius) {
		transform.position.x -= speed*Time.deltaTime;
		speed = -speed;
	}
}