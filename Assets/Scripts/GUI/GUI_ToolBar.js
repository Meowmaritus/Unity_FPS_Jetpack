class GUI_ToolBar extends GUI_Base{
	//~ public var ItemLabel : String[] = ["a","b"];
	public var content : GUIContent[] = [GUIContent("")];
	private var selectedToolBar = 0;
	function OnGUI(){
		GUI.skin = GuiSkin;
		selectedToolBar = GUI.Toolbar(getRect(), selectedToolBar,  content);
		
		if(GUI.changed){
			print(selectedToolBar);
		}
	}
}