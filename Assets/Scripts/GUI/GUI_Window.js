class GUI_Window extends GUI_Base {
	private var windowId = 0;
	public var content : GUIContent = GUIContent("");
	public var objects = 0;

	function Awake(){
		//purpose to get unique id
		var objs = GetComponents(GUI_Window);
		var cnt = 0;
		for(obj in objs)
			cnt = Mathf.Max(cnt, obj.windowId);
		windowId = cnt+1;
	}
	
	function OnGUI(){
		GUI.skin = GuiSkin;
		//~ ItemRect = GUI.Window(windowId, getRect(), fillWindow, content);
		GUI.Window(windowId, getRect(), fillWindow, content);
	}
	
	function fillWindow(winId : int){
		//~ GUI.DragWindow (Rect (0,0, 10000, 20));
	}
}