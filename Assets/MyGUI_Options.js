public var item0 : GUIContent = GUIContent("Motion Blur:");
public var item1 : GUIContent = GUIContent("Back");
public var item2 : GUIContent = GUIContent("Vignetting:");
public var item3 : GUIContent = GUIContent("Chromatic Abberation:");
public var item4 : GUIContent = GUIContent("Creasing:");
public var item5 : GUIContent = GUIContent("Ambient Occlusion:");
public var item6 : GUIContent = GUIContent("Post-Processed Anti-Aliasing:");
private var item7selectedIndex = 0;
public var item7 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
private var item8selectedIndex = 0;
public var item8 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
private var item9selectedIndex = 0;
public var item9 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
private var item10selectedIndex = 0;
public var item10 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
private var item11selectedIndex = 0;
public var item11 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
private var item12selectedIndex = 0;
public var item12 : GUIContent[] = [GUIContent("Off"),GUIContent("Low"),GUIContent("High")];
public var guiSkin : GUISkin; 

function OnGUI(){ 
	GUI.skin = guiSkin;
	GUI.Box(Rect(Screen.width/2 + -200, 10, 400, 50), item0);
	if(GUI.Button(Rect(Screen.width/2 + -50, Screen.height-24, 100, 24), item1)){
	//Implement Code Here
	}
	GUI.Box(Rect(Screen.width/2 + -200, 70, 400, 50), item2);
	GUI.Box(Rect(Screen.width/2 + -200, 130, 400, 50), item3);
	GUI.Box(Rect(Screen.width/2 + -200, 190, 400, 50), item4);
	GUI.Box(Rect(Screen.width/2 + -200, 250, 400, 50), item5);
	GUI.Box(Rect(Screen.width/2 + -200, 310, 400, 50), item6);
	item7selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 31, 390, 24), item7selectedIndex, item7);
	if(GUI.changed){
	//Implement Code Here
	}
	item8selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 91, 390, 24), item8selectedIndex, item8);
	if(GUI.changed){
	//Implement Code Here
	}
	item9selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 151, 390, 24), item9selectedIndex, item9);
	if(GUI.changed){
	//Implement Code Here
	}
	item10selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 211, 390, 24), item10selectedIndex, item10);
	if(GUI.changed){
	//Implement Code Here
	}
	item11selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 271, 390, 24), item11selectedIndex, item11);
	if(GUI.changed){
	//Implement Code Here
	}
	item12selectedIndex = GUI.Toolbar(Rect(Screen.width/2 + -195, 331, 390, 24), item12selectedIndex, item12);
	if(GUI.changed){
	//Implement Code Here
	}
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