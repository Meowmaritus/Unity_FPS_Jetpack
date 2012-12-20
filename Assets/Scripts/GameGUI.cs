//ENABLE DEBUG MODE:   //
#define DEBUG_MODE

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("_Main/HUD/Game GUI")]
public class DebugValueSetter
{	
	public object this[string text]
	{
		set { GameGUI.DebugOnlyDisplayValues[text] = value; }
		get { return (GameGUI.DebugOnlyDisplayValues[text]); }
	}	
}

public class GameGUI : MonoBehaviour {
	public PlayerHandler Player;
	
	public HealthBar BarHealth;
	public float BarHealthLerp = 0.75f;
	public float BarHealthTempLerp = 0.75f;
	private float oldBarHealthValue = 0.0f;
	private float oldBarHealthTempValue = 0.0f;
	private float tempBarHealthTempValue = 0.0f;
	
	public HealthBar BarStamina;
	public float BarStaminaLerp = 0.75f;
	public float BarStaminaTempLerp = 0.75f;
	private float oldBarStaminaValue = 0.0f;
	private float oldBarStaminaTempValue = 0.0f;
	private float tempBarStaminaTempValue = 0.0f;
	
	public HealthBar BarJetpackFuel;
	public float BarJetpackFuelLerp = 0.75f;
	public float BarJetpackFuelTempLerp = 0.75f;
	private float oldBarJetpackFuelValue = 0.0f;
	private float oldBarJetpackFuelTempValue = 0.0f;
	private float tempBarJetpackFuelTempValue = 0.0f;
	
	
	public static int wallCount;
	public static int targetCount;
	public static int bulletCount;
	
#if DEBUG_MODE
	public static bool DebugMode = true;
#else
	public static bool DebugMode = false;
	public static bool DebugBox = false;
#endif

    public static Dictionary<string, object> DisplayValues = new Dictionary<string, object>();
	public static Dictionary<string, object> DebugOnlyDisplayValues = new Dictionary<string, object>();

	public GUIText displayText;
	public Color DisplayTextColor;
	public GUIText debugText;
	public Color DebugTextColor;
	
	private System.Text.StringBuilder builder = new System.Text.StringBuilder();
	private System.Text.StringBuilder debugBuilder = new System.Text.StringBuilder();
	public static DebugValueSetter DebugValue = new DebugValueSetter();
	
	public GUIStyle DefaultHealthBarGUIStyle = new GUIStyle();
    	
	void OnGUI()
	{						
		Screen.lockCursor = true;
		Screen.showCursor = false;		
	}
	
	void Start () {   
		displayText.text = "[ERROR:GUI_TEXT_NOT_LOADED]";
		
		//int width = Screen.GetResolution[0].width;
		//int height = Screen.GetResolution[0].height;
		//Screen.SetResolution(width, height, true);
		//Screen.lockCursor = true;
		//Screen.showCursor = false;
	}
	
	void Update () {		
		
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("Title Screen");	
		}	

		displayText.material.color = DisplayTextColor;
		debugText.material.color = DebugTextColor;
		
		BarHealth.Value = Mathf.Lerp(Player.SmoothHealth, oldBarHealthValue, BarHealthLerp);
		BarStamina.Value = Mathf.Lerp(Player.SmoothStamina, oldBarStaminaValue, BarStaminaLerp);
		BarJetpackFuel.Value = Mathf.Lerp(Player.SmoothJetpackFuel, oldBarJetpackFuelValue, BarJetpackFuelLerp);
		
		if (BarHealth.Value <= 0)
		{
			Player.Die();
		}
		
		if (Player.IsUpdatingTempHealth)      { tempBarHealthTempValue = Player.SmoothHealth; }
		if (Player.IsUpdatingTempStamina)     { tempBarStaminaTempValue = Player.SmoothStamina; }
		if (Player.IsUpdatingTempJetpackFuel) { tempBarJetpackFuelTempValue = Player.SmoothJetpackFuel; }
		
		BarHealth.TempValue = Mathf.Lerp(tempBarHealthTempValue, oldBarHealthTempValue, BarHealthTempLerp);                 
		BarStamina.TempValue = Mathf.Lerp(tempBarStaminaTempValue, oldBarStaminaTempValue, BarStaminaTempLerp);             
		BarJetpackFuel.TempValue = Mathf.Lerp(tempBarJetpackFuelTempValue, oldBarJetpackFuelTempValue, BarJetpackFuelTempLerp);
		
		//Screen.showCursor = CSharpInterpreter.showOutputAsEditorSelection;
		
#if DEBUG_MODE
		debugText.enabled = DebugMode;		
#endif
		
		if (Input.GetKeyDown(KeyCode.BackQuote))
		{
			
		}
		
		DisplayValues["NOTE"] = "Press H to hurt yourself!";
        DisplayValues["Wall Hit Count"] = wallCount;
        DisplayValues["Target Hit Count"] = targetCount;
        DisplayValues["Bullets"] = bulletCount;		
		
        BuildDebugString();

        displayText.text = builder.ToString();
		debugText.text = debugBuilder.ToString ();
				
		
		oldBarHealthValue = BarHealth.Value;
		oldBarStaminaValue = BarStamina.Value;
		oldBarJetpackFuelValue = BarJetpackFuel.Value;
		oldBarHealthTempValue = BarHealth.TempValue;
		oldBarStaminaTempValue = BarStamina.TempValue;
		oldBarJetpackFuelTempValue = BarJetpackFuel.TempValue;
	}

    void BuildDebugString()
    {
        builder.Remove(0, builder.Length);
		debugBuilder.Remove(0, debugBuilder.Length);

        foreach (KeyValuePair<string, object> kv in DisplayValues)
        {
        	builder.AppendLine(kv.Key + ": " + kv.Value.ToString());
        }
		
		
		debugBuilder.AppendLine("(Toggle debug text with F1)");
		
		debugBuilder.AppendLine(" ");
		
		foreach (KeyValuePair<string, object> kv in DebugOnlyDisplayValues)
        {
        	debugBuilder.AppendLine(kv.Key + ": " + kv.Value.ToString());
        }

    }
}
