class GUI_Image extends GUI_Base {
	public var content : Texture;
	public var alpha : float = 0.0;

	function OnGUI(){
		GUI.skin = GuiSkin;
		GUI.DrawTexture(getRect(), content, ScaleMode.StretchToFill, true, alpha);
	}
}