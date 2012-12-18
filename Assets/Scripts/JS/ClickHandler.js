#pragma strict

var myVoxel : GameObject;
var previewBox : GameObject;
var locs : Hashtable;
var symmetryOn : boolean;
var symLoc : float;
var symX : boolean;
var symY : boolean;
var symZ : boolean;

function Start () {
	locs = new Hashtable();
	symmetryOn = false;
	symLoc = 0;
	symX = symY = symZ = false;
}

function Update () {
	if (Input.GetKeyDown(KeyCode.Escape))
		Application.Quit();

	
	var middle = Vector3(Screen.width/2, Screen.height/2, 0);
	var ray : Ray = Camera.mainCamera.ScreenPointToRay(middle);//Input.mousePosition);
	var hit : RaycastHit;
	if (Physics.Raycast(ray, hit)) {
		var loc : Vector3 = hit.point;
		var locInt = Vector3(Mathf.RoundToInt(loc.x), Mathf.RoundToInt(loc.y), Mathf.RoundToInt(loc.z));
		
		if (locs.ContainsKey(locInt)) {
			if (Mathf.Approximately(Mathf.Abs(loc.x - locInt.x), 0.5))
				locInt.x += Mathf.RoundToInt(1.1*(loc.x - locInt.x));
			if (Mathf.Approximately(Mathf.Abs(loc.y - locInt.y), 0.5))
				locInt.y += Mathf.RoundToInt(1.1*(loc.y - locInt.y));
			if (Mathf.Approximately(Mathf.Abs(loc.z - locInt.z), 0.5))
				locInt.z += Mathf.RoundToInt(1.1*(loc.z - locInt.z));
		}
		if (Input.GetMouseButtonUp(0)) {
			var v : GameObject = Instantiate(myVoxel, locInt, transform.rotation);
			locs.Add(locInt, v);
		}
		else {
			previewBox.transform.position = locInt;
			previewBox.renderer.enabled = true;
		}
		/*if (symmetryOn) {
			if (symX) {
				locInt.x = -locInt.x;
				Instantiate(myVoxel, locInt, transform.rotation);
				locs.Add(locInt, 0);
			}
			if (symY) {
				locInt.y = -locInt.y;
				Instantiate(myVoxel, locInt, transform.rotation);
				locs.Add(locInt, 0);
			}
			if (symZ) {
				locInt.z = -locInt.z;
				Instantiate(myVoxel, locInt, transform.rotation);
				locs.Add(locInt, 0);
			}
		}*/
	}
	else {
		previewBox.renderer.enabled = false;
	}
	
	if (Input.GetKeyUp("x"))
		symX = true;
		//Debug.Log("X-AXIS");
		
	if (Input.GetKeyUp("y"))
		symY = true;
		//Debug.Log("Y-AXIS");
		
	if (Input.GetKeyUp("z"))
		symZ = true;
		//Debug.Log("Z-AXIS");

	if (Input.GetMouseButtonUp(1)) {
		//Debug.Log("hi dere");
		symmetryOn = !symmetryOn;
	}
}