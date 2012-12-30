class GUI_TextArea extends GUI_Base {
	public var content : String = "";

	function OnGUI(){
		GUI.skin = GuiSkin;
		content = GUI.TextArea(getRect(), content);
	}
}