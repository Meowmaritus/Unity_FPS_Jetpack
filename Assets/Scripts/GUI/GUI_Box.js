class GUI_Box extends GUI_Base {
	public var content : GUIContent = GUIContent("");
	function OnGUI(){
		GUI.skin = GuiSkin;
		GUI.Box(getRect(), content);
	}
}