public var item0 : GUIContent = GUIContent("Resume");
public var item1 : GUIContent = GUIContent("Options");
public var item2 : GUIContent = GUIContent("Quit To Title");
public var item3 : GUIContent = GUIContent("Quit To Desktop");
public var item4 : GUIContent = GUIContent("Paused");
public var guiSkin : GUISkin; 

function OnGUI(){ 
	GUI.skin = guiSkin;
	if(GUI.Button(Rect(Screen.width/2 + -128, Screen.height/2 + -72, 256, 24), item0)){
	//Implement Code Here
	}
	if(GUI.Button(Rect(Screen.width/2 + -128, Screen.height/2 + -42, 256, 24), item1)){
	//Implement Code Here
	}
	if(GUI.Button(Rect(Screen.width/2 + -128, Screen.height/2 + -12, 256, 24), item2)){
	//Implement Code Here
	}
	if(GUI.Button(Rect(Screen.width/2 + -128, Screen.height/2 + 18, 256, 24), item3)){
	//Implement Code Here
	}
	GUI.Box(Rect(Screen.width/2 + -150, Screen.height/2 + -135, 300, 50), item4);
}
function Awake() { 
}
function fillWindow(winID : int) { 
	switch(winID){
		default:
			Debug.Log("Default case reached");
	}
	GUI.DragWindow (Rect (0,0, 10000, 10000));
}