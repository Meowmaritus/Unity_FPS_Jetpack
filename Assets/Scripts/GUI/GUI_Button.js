class GUI_Button extends GUI_Base {
	public var content : GUIContent = GUIContent("");
	function OnGUI(){
		GUI.skin = GuiSkin;
		if(GUI.Button(getRect(), content)){
			print("pressed");
		}
	}
}
