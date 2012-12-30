class GUI_TextField extends GUI_Base {
	public var ItemMaxChar = 20;
	public var content : String = "";

	function OnGUI(){
		GUI.skin = GuiSkin;
		content = GUI.TextField(getRect(), content, ItemMaxChar);
	}
}