#pragma strict

var speed : float;
var rotSpeed : float;
var playerCam : Camera;
var boost : boolean;

function Start () {
	boost = false;
	Screen.showCursor = false;
	Screen.lockCursor = true;
}

function Update () {
	var t : Vector3 = new Vector3(Input.GetAxis("Horizontal"),
									Input.GetAxis("UpDown"),
									Input.GetAxis("Vertical"));
	Input.mousePosition.x = Input.mousePosition.y = 0;
	if (Input.GetButtonDown("SpeedBoost"))
		boost = true;
	else if (Input.GetButtonUp("SpeedBoost"))
		boost = false;
	if (boost)
		t *= 2;
	
	//Debug.Log(t);
	transform.Translate(speed*Time.deltaTime*t, Space.Self);
	transform.Rotate(0,rotSpeed*Time.deltaTime*Input.GetAxis("Mouse X"),0);
	//transform.Rotate(0, Input.GetAxis("Mouse X")*rotSpeed*Time.deltaTime, 0, Space.World);
	playerCam.transform.Rotate(-Input.GetAxis("Mouse Y")*rotSpeed*Time.deltaTime, 0, 0, Space.Self);
}