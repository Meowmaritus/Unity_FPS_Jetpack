
 class GUI_PasswordField extends GUI_Base {
	public var content : String = "";
	public var ItemMaxChar = 20;

	function OnGUI(){
		GUI.skin = GuiSkin;
		content = GUI.PasswordField (getRect(), content, "*"[0], ItemMaxChar);
		//~ GUI.SelectionGrid (getRect(), 0, content, 1);
	}
}