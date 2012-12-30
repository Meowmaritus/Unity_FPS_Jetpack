var GuiSkin : GUISkin;
enum SnapHorizon { Left, Center, Right };
enum SnapVertical { Top, Middle, Bottom };
enum DisplayType { Text, Image };
//~ var displayType : DisplayType = DisplayType.Text;
//~ var ContentImage : Texture;
//~ var ContentString : String;
var snapHorizon : SnapHorizon = SnapHorizon.Left;
var snapVertical : SnapVertical = SnapVertical.Top;
var ItemRect : Rect = Rect(0,0,100,50);
var visibility = true;

function show(){invisible=true;print("show");}
function hide(){invisible=false;}
//~ function dummy(){print(displayType);}
function getRect(): Rect{
	var itemRect = Rect();
	var h = Screen.height;
	var w = Screen.width;	
	itemRect.height = ItemRect.height;
	itemRect.width = ItemRect.width;	
	switch(snapHorizon){
		case SnapHorizon.Left:
			itemRect.x = ItemRect.x;
			break;
		case SnapHorizon.Right:
			itemRect.x = w + ItemRect.x - ItemRect.width;
			break;
		case SnapHorizon.Center:
			itemRect.x = w/2 - ItemRect.width /2 + ItemRect.x;
	}
	switch(snapVertical){
		case SnapVertical.Top:
			itemRect.y = ItemRect.y;
			break;
		case SnapVertical.Bottom:
			itemRect.y = h + ItemRect.y - ItemRect.height;
			break;
		case SnapVertical.Middle:
			itemRect.y = h/2 - ItemRect.height/2 + ItemRect.y;
	}
	return itemRect;
}
