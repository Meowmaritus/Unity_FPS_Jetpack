//元件字串
private var guidata : String;
//元件內容物件
private var variables : String;
//元件內容的數量
private var variablecount = 0;
//OnGUI區塊的頭
private var header : String = "public var guiSkin : GUISkin; \n\nfunction OnGUI(){ \n\tGUI.skin = guiSkin;\n";
//OnGUI區塊的尾
private var tail : String = "}\n";
/* Window用  */
private var winitems = 0;
//Awake區塊
private var awakeheader : String = "function Awake() { \n";
private var awakedata : String = "";
//fillWindow區塊
private var fillwinheader : String = "function fillWindow(winID : int) { \n\tswitch(winID){\n";
private var fillwindata : String = "";
private var fillwintail : String = "\t\tdefault:\n\t\t\tDebug.Log(\"Default case reached\");\n\t}\n\tGUI.DragWindow (Rect (0,0, 10000, 10000));\n}";
function resetData(){
	guidata = "";
	variables = "";
	variablecount = 0;
	awakedata = "";
	fillwindata = "";
}

function Awake(){
	resetData();
	//~ var obj = GameObject.Find("UnityWatermark-small");
	//~ obj.guiTexture.color = Color.black;
	//~ print(obj.active);
	//~ for (var ob in obj.transform)
		//~ print(ob.name);
}
function Update () {
	var obj = GetComponents(GUI_Base);
	var rectStr : String;
	var blockStr : String = "{\n\t//Implement Code Here\n\t}\n";
	var blockStr2 : String = "{\n\t\t\t//Implement Code Here\n\t\t\t}\n";
	var cnt = 1;
	for(var o in obj){
		/*
		guidata += typeof(o) + ":\n";
		guidata += o.snapHorizon + "," + o.snapVertical + "\n";
		guidata += o.ItemRect.x + ", " + 
						o.ItemRect.y + ", " + 
						o.ItemRect.width + ", " + 
						o.ItemRect.height + "\n\n";
		*/
		rectStr = getRectString(o.snapHorizon,o.snapVertical,o.ItemRect);
		switch (typeof(o)){
			case GUI_Box:
				variables += "public var item" + variablecount + " : GUIContent = GUIContent(\"" + o.content.text + "\");\n";
				if (winitems == 0)
					guiStr = "\tGUI.Box(" + rectStr + ", item" + variablecount++ + ");\n";
				else {
					fillwindata += "\t\t\tGUI.Box(" + rectStr + ", item" + variablecount++ + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				break;
			case GUI_Button:
				//~ Debug.Log(o.snapHorizon);
				//~ Debug.Log(o.snapVertical);
				//~ Debug.Log(o.ItemRect);
				variables += "public var item" + variablecount + " : GUIContent = GUIContent(\"" + o.content.text + "\");\n";
				if (winitems == 0)
					guiStr = "\tif(GUI.Button(" + rectStr + ", item" + variablecount++ + "))" + blockStr;
				else {
					fillwindata += "\t\t\tif(GUI.Button(" + rectStr + ", item" + variablecount++ + "))" + blockStr2;
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ Debug.Log(o.content.tooltip);
				//~ Texture2D GUIContent
				//~ print(Utils.test(o.snapHorizon));
				break;
			case GUI_Image:
				variables += "public var item" + variablecount + " : Texture;\n";
				if (winitems == 0)
					guiStr = "\tGUI.DrawTexture(" + rectStr + ", item" + variablecount++ + ", ScaleMode.StretchToFill, true, " + o.alpha + ");\n";
				else {
					fillwindata += "\t\t\tGUI.DrawTexture(" + rectStr + ", item" + variablecount++ + ", ScaleMode.StretchToFill, true, " + o.alpha + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				break;
			case GUI_Label:
				variables += "public var item" + variablecount + " : GUIContent = GUIContent(\"" + o.content.text + "\");\n";
				if (winitems == 0)
					guiStr = "\tGUI.Label(" + rectStr + ", item" + variablecount++ + ");\n";
				//~ GUI.Label(getRect(), content);
				else {
					fillwindata += "\t\t\tGUI.Label(" + rectStr + ", item" + variablecount++ + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				break;
			case GUI_PasswordField:
				variables += "public var item" + variablecount + " : String = \"\";\n";
				if (winitems == 0)
					guiStr = "\titem" + variablecount + " = GUI.PasswordField(" + rectStr + ", item" + variablecount++ + ", \"*\"[0], " + o.ItemMaxChar + ");\n";
				else {
					fillwindata += "\t\t\titem" + variablecount + " = GUI.PasswordField(" + rectStr + ", item" + variablecount++ + ", \"*\"[0], " + o.ItemMaxChar + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ content = GUI.PasswordField (getRect(), content, "*"[0], ItemMaxChar);
				break;
			case GUI_SelectionGrid:
				variables += "private var item" + variablecount + "selectedIndex = 0;\n" +
				                  "public var item" + variablecount + " : GUIContent[] = [";
				for (c in o.content) {
					variables += "GUIContent(\"" + c.text + "\"),";
				}
				variables = variables.Substring(0,variables.length-1) + "];\n";
				if (winitems == 0)
					guiStr = "\titem" + variablecount + "selectedIndex = GUI.SelectionGrid("+ rectStr + ", item" + variablecount + "selectedIndex, item" + variablecount++ + ", " + o.elementsInRow + ");\n";
				else {
					fillwindata += "\t\t\titem" + variablecount + "selectedIndex = GUI.SelectionGrid("+ rectStr + ", item" + variablecount + "selectedIndex, item" + variablecount++ + ", " + o.elementsInRow + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ public var elementsInRow = 1; 
				//~ public var content : GUIContent[];
				//~ private var selectedIndex = 0;

				//~ function OnGUI(){
					//~ GUI.skin = GuiSkin;
					//~ selectedIndex = GUI.SelectionGrid (getRect(), selectedIndex, content, elementsInRow);
				break;
			case GUI_TextArea:
				variables += "public var item" + variablecount + " : String = \"\";\n";
				if (winitems == 0)
					guiStr = "\titem" + variablecount + " = GUI.TextArea(" + rectStr + ", item" + variablecount++ + ");\n";
				else {
					fillwindata += "\t\t\titem" + variablecount + " = GUI.TextArea(" + rectStr + ", item" + variablecount++ + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ content = GUI.TextArea(getRect(), content);
				break;
			case GUI_TextField:
				variables += "public var item" + variablecount + " : String = \"\";\n";
				if (winitems == 0)
					guiStr = "\titem" + variablecount + " = GUI.TextArea(" + rectStr + ", item" + variablecount++ + ", " + o.ItemMaxChar + ");\n";
				else {
					fillwindata += "\t\t\titem" + variablecount + " = GUI.TextArea(" + rectStr + ", item" + variablecount++ + ", " + o.ItemMaxChar + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ content = GUI.TextField(getRect(), content, ItemMaxChar);
				break;
			case GUI_Toggle:
				variables += "private var item" + variablecount + "value = " + o.value.ToString().ToLower() + ";\n" +
				                  "public var item" + variablecount + " : GUIContent = GUIContent(\"" + o.content.text + "\");\n";			
				if (winitems == 0)
					guiStr = "\titem" + variablecount + "value = GUI.Toggle(" + rectStr + ", item" + variablecount + "value, item" + variablecount++ + ");\n";
				else {
					fillwindata += "\t\t\titem" + variablecount + "value = GUI.Toggle(" + rectStr + ", item" + variablecount + "value, item" + variablecount++ + ");\n";
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ value = GUI.Toggle(getRect(), value, content);
				break;
			case GUI_ToolBar:
				variables += "private var item" + variablecount + "selectedIndex = 0;\n" +
				                  "public var item" + variablecount + " : GUIContent[] = [";
				for (c in o.content) {
					variables += "GUIContent(\"" + c.text + "\"),";
				}
				variables = variables.Substring(0,variables.length-1) + "];\n";
				if (winitems == 0)
					guiStr = "\titem" + variablecount + "selectedIndex = GUI.Toolbar(" + rectStr + ", item" + variablecount + "selectedIndex, item" + variablecount++ + ");\n" +
								"\tif(GUI.changed)" + blockStr;
				else {
					fillwindata += "\t\t\titem" + variablecount + "selectedIndex = GUI.Toolbar(" + rectStr + ", item" + variablecount + "selectedIndex, item" + variablecount++ + ");\n" +
										"\t\t\tif(GUI.changed)" + blockStr2;
					if(--winitems ==0)
						fillwindata += "\t\t\tbreak;\n";
				}
				//~ public var content : GUIContent[];
				//~ private var selectedToolBar = 0;
				//~ function OnGUI(){
					//~ GUI.skin = GuiSkin;
					//~ selectedToolBar = GUI.Toolbar(getRect(), selectedToolBar,  content);
					
					//~ if(GUI.changed){
						//~ print(selectedToolBar);
					//~ }			
				break;
			case GUI_Window:
				//~ Debug.Log("A Bit Diffcult XD.");
				if (winitems > 0)
					Debug.Log("Window contain window, terminate fill previous window");
				winitems = o.objects;
				var id = o.windowId;
				variables += "private var win" + id + "Rect : Rect;\n" + 
								  "public var win" + id + " : GUIContent = GUIContent(\"" + o.content.text + "\");\n";
				guiStr = "\twin" + id + "Rect = GUI.Window(" + id + ", win" + id + "Rect, fillWindow, win" + id + ");\n";
				awakedata += "\twin" + id + "Rect = " + rectStr + ";\n";
				fillwindata += "\t\tcase " + id + ":\n";
				//~ Debug.Log(awakeheader+awakedata+tail);
				//~ GUI.Window(windowId, getRect(), fillWindow, content);
				break;
			default:
				Debug.Log("Not implement yet.");
		}
		//~ Debug.Log(winitems);
		guidata += guiStr;
		guiStr = "";
		//~ Debug.Log(typeof(o) == GUI_TextField);
		//~ print( typeof(o));
		//~ print(o.ItemRect.x);
	}
	//~ if(Input.GetKeyUp(KeyCode.Return))
	
	transform.SendMessage("SetData", variables + header + guidata + tail + awakeheader + awakedata + tail + fillwinheader + fillwindata + fillwintail);
	resetData();
}

function getRectString(sh : SnapHorizon, sv : SnapVertical, itemRect : Rect) : String{
	var ret = "Rect(";
	var h = "Screen.height";
	var w = "Screen.width";	
	var ix = parseInt(itemRect.x);
	var iy = parseInt(itemRect.y);
	var ih = parseInt(itemRect.height);
	var iw = parseInt(itemRect.width);	
	switch(sh){
		case SnapHorizon.Left:
			ret += ix;
			break;
		case SnapHorizon.Right:
			ret += w + (ix - iw);
			break;
		case SnapHorizon.Center:
			ret += w+"/2 + " + (-iw /2 + ix);
	}
	ret += ", ";
	switch(sv){
		case SnapVertical.Top:
			ret += iy;
			break;
		case SnapVertical.Bottom:
			ret += h + (iy - ih);
			break;
		case SnapVertical.Middle:
			ret += h + "/2 + " + (-ih/2 + iy);
	}
	ret += ", " + iw + ", " + ih + ")";
	return ret;
}