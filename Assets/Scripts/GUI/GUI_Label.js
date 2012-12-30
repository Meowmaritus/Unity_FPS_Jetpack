class GUI_Label extends GUI_Base {
	public var content : GUIContent = GUIContent("");
	function OnGUI(){
		GUI.skin = GuiSkin;
		if(visibility){
			GUI.Label(getRect(), content);
		}
	}
}