class GUI_Slider extends GUI_Base {
	public var style : GUIStyle;
	public var content : GUIStyle;
	public var value : float = 1.0f;
	public var size : float = 1.0f;
	public var start : float = 1.0f;
	public var end : float = 1.0f;
	public var id : int = 0;
	public var horizontal : boolean = true;

	function OnGUI(){
		GUI.skin = GuiSkin;
		value = GUI.Slider(getRect(), value, size, start, end, style, style, horizontal, id);
	}
}