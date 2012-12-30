class GUI_Toggle extends GUI_Base {
	public var content : GUIContent = GUIContent("");
	private var value = false;

	function OnGUI(){
		GUI.skin = GuiSkin;
		value = GUI.Toggle(getRect(), value, content);
	}
}