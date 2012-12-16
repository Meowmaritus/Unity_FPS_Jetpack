//ENABLE DEBUG MODE:   //
#define DEBUG_MODE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugValueSetter
{	
	public object this[string text]
	{
		set { GameGUI.DebugOnlyDisplayValues[text] = value; }
		get { return (GameGUI.DebugOnlyDisplayValues[text]); }
	}	
}

public class GameGUI : MonoBehaviour {
	public static int wallCount;
	public static int targetCount;
	public static int bulletCount;
	
#if DEBUG_MODE
	public static bool DebugMode = true;
	public static bool DebugBox = true; 
#else
	public static bool DebugMode = false;
	public static bool DebugBox = false;
#endif

    public static Dictionary<string, object> DisplayValues = new Dictionary<string, object>();
	public static Dictionary<string, object> DebugOnlyDisplayValues = new Dictionary<string, object>();

	public GUIText displayText;
	public GUIText debugText;
	
	private System.Text.StringBuilder builder = new System.Text.StringBuilder();
	private System.Text.StringBuilder debugBuilder = new System.Text.StringBuilder();
	public static DebugValueSetter DebugValue = new DebugValueSetter();
    	
	void Start () {   
		displayText.text = "[ERROR:GUI_TEXT_NOT_LOADED]";
		
		Screen.lockCursor = true;
		Screen.showCursor = false;		
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
		
		//Screen.lockCursor = !CSI.IConsole
		//Screen.showCursor = CSharpInterpreter.showOutputAsEditorSelection;
		
		#if DEBUG_MODE
		debugText.enabled = DebugMode;	
		debugText.guiTexture.enabled = (DebugMode && DebugBox);	
		
		if (Input.GetKeyDown(KeyCode.F1))
		{
			DebugMode = !DebugMode;
		}
		
		if (Input.GetKeyDown(KeyCode.F2))
		{
			DebugBox = !DebugBox;
		}
#endif
		
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			
		}

        DisplayValues["Wall Hit Count"] = wallCount;
        DisplayValues["Target Hit Count"] = targetCount;
        DisplayValues["Bullets"] = bulletCount;
		
		

        BuildDebugString();

        displayText.text = builder.ToString();
		debugText.text = debugBuilder.ToString ();
	}

    void BuildDebugString()
    {
        builder.Remove(0, builder.Length);
		debugBuilder.Remove(0, debugBuilder.Length);

        foreach (KeyValuePair<string, object> kv in DisplayValues)
        {
        	builder.AppendLine(kv.Key + ": " + kv.Value.ToString());
        }
		
		
		debugBuilder.AppendLine("DebugMode = " + DebugMode + "; //Toggle with F1//");
		debugBuilder.AppendLine("DebugBox = " + DebugBox + "; //Toggle with F2//");
		
		debugBuilder.AppendLine(" ");
		
		foreach (KeyValuePair<string, object> kv in DebugOnlyDisplayValues)
        {
        	debugBuilder.AppendLine(kv.Key + ": " + kv.Value.ToString());
        }

    }
}
