using UnityEngine;
using System.Collections;

[AddComponentMenu("_Main/HUD/Health Bar")]
public class HealthBar : MonoBehaviour 
{
	public bool Visible = true;
	
	public string Text = "[???]";
    public float Value; //current progress
	public float TempValue; //current progress
	public Rect Placement;
	
	public Texture2D EmptyTexture;
	public Texture2D TempTexture;
	public Texture2D FullTexture;

	private GUIStyle _FullGUIStyle = new GUIStyle();
	private GUIStyle _TempGUIStyle = new GUIStyle();
	private GUIStyle _EmptyGUIStyle = new GUIStyle();
	
	public Font font;
	
	public Color EmptyTextColor = Color.white;
	public Color TempTextColor = Color.gray;
	public Color FullTextColor = Color.black;
	
	void Start()
	{
		_EmptyGUIStyle.font = font;
		_EmptyGUIStyle.fontStyle = FontStyle.Bold;
		_EmptyGUIStyle.fontSize = 12;
		_EmptyGUIStyle.alignment = TextAnchor.MiddleCenter;
		
		
		_TempGUIStyle.font = font;
		_TempGUIStyle.fontStyle = FontStyle.Bold;
		_TempGUIStyle.fontSize = 12;
		_TempGUIStyle.alignment = TextAnchor.MiddleCenter;
		
		_FullGUIStyle.font = font;
		_FullGUIStyle.fontStyle = FontStyle.Bold;
		_FullGUIStyle.fontSize = 12;
		_FullGUIStyle.alignment = TextAnchor.MiddleCenter;
		
		_EmptyGUIStyle.normal.background = EmptyTexture;
		_EmptyGUIStyle.normal.textColor = EmptyTextColor;
		
		_TempGUIStyle.normal.background = TempTexture;
		_TempGUIStyle.normal.textColor = TempTextColor;
		
		_FullGUIStyle.normal.background = FullTexture;
		_FullGUIStyle.normal.textColor = FullTextColor;
	}

    void OnGUI() {			
		Value = Mathf.Clamp(Value, 0.0f, 1.0f);
		TempValue = Mathf.Clamp(TempValue, 0.0f, 1.0f);
		
		_EmptyGUIStyle.normal.background = EmptyTexture;
		_EmptyGUIStyle.normal.textColor = EmptyTextColor;
		
		_TempGUIStyle.normal.background = TempTexture;
		_TempGUIStyle.normal.textColor = TempTextColor;
		
		_FullGUIStyle.normal.background = FullTexture;
		_FullGUIStyle.normal.textColor = FullTextColor;
		
       //draw the background:
		if (Visible == true)
		{
	       GUI.BeginGroup(new Rect(Placement.x, Placement.y, Placement.width, Placement.height));
	       	 GUI.Box(new Rect(0,0, Placement.width, Placement.height),(Text + ": " + (Value * 100).ToString("0.00") + "%"), _EmptyGUIStyle);
			
	         GUI.BeginGroup(new Rect(0,0, Placement.width * TempValue, Placement.height));
	          GUI.Box(new Rect(0,0, Placement.width, Placement.height),(Text + ": " + (Value * 100).ToString("0.00") + "%"), _TempGUIStyle);
			 GUI.EndGroup();
			
			 GUI.BeginGroup(new Rect(0,0, Placement.width * Value, Placement.height));
			  GUI.Box(new Rect(0,0, Placement.width, Placement.height),(Text + ": " + (Value * 100).ToString("0.00") + "%"), _FullGUIStyle);						
			 GUI.EndGroup();		         
			
	       GUI.EndGroup();			
		}
    }

    void FixedUpdate() {				
		
    }
}
