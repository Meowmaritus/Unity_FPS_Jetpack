 class GUI_SelectionGrid extends GUI_Base {
	public var elementsInRow = 1; 
	public var content : GUIContent[] = [GUIContent("")];
	private var selectedIndex = 0;

	function OnGUI(){
		GUI.skin = GuiSkin;
		selectedIndex = GUI.SelectionGrid (getRect(), selectedIndex, content, elementsInRow);
	}
}