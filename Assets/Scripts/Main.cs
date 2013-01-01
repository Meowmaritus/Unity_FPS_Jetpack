using UnityEngine;
using System.Collections;

public enum Option
{
	Off,
	Low,
	High
}

public class Main : MonoBehaviour {

    public Camera viewCam;
    public Camera gunCam;
    public PauseMenu pauseMenu;
	
	public GUIText GUI_DisplayText;
	public GUIText GUI_DebugText;
	public HealthBar GUI_HealthBar;
	public HealthBar GUI_StaminaBar;
	public HealthBar GUI_JetpackFuelBar;
		
	public Option Options_MotionBlur = Option.Off;
	public Option Options_Vignetting = Option.Off;
	public Option Options_ChromaticAbberation = Option.Off;
	public Option Options_Creasing = Option.Off;
	public Option Options_AmbientOcclusion = Option.Off;
	public Option Options_PostProcessedAntiAliasing = Option.Off;
	
	public bool ShowDebug = true;

    public static bool Paused = false;
    public static bool GodMode = false;

	// Use this for initialization
	void Start () {
	
	}
	
	void OnGUI()
	{
		GUI_DebugText.enabled = (Debug.isDebugBuild && ShowDebug);
		
		if (Paused == false)
		{
			GUI_DisplayText.enabled = true;			
			GUI_HealthBar.Visible = true;
			GUI_StaminaBar.Visible = true;
			GUI_JetpackFuelBar.Visible = true;	
		}
		else if (Paused == true)
		{
			GUI_DisplayText.enabled = false;
			GUI_HealthBar.Visible = false;
			GUI_StaminaBar.Visible = false;
			GUI_JetpackFuelBar.Visible = false;
		}
	}
	
	// Update is called once per frame
    void FixedUpdate()
    {
        if (Main.Paused == false)
        {
            DoFixedUpdate();
        }
        else if (Main.Paused == true)
        {
            DoFixedUpdate_Paused();
        }
    }

    void Update()
    {
        if (Main.Paused == false)
        {
            DoUpdate();
        }
        else if (Main.Paused == true)
        {
            DoUpdate_Paused();
        }
    }

    void DoUpdate()
    {
        Time.timeScale = 1.0f;

        GodMode = (GodMode && Debug.isDebugBuild);
    }

    void DoUpdate_Paused()
    {
        Time.timeScale = 0.0f;			
    }

    void DoFixedUpdate()
    {

    }

    void DoFixedUpdate_Paused()
    {

    }

    public void QuitToTitle()
    {
        Application.LoadLevel("Title Screen");
		Paused = false;
		pauseMenu.OptionsMenu = false;
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Paused = true;
        pauseMenu.OptionsMenu = false;
    }

    public void UnPause()
    {
        Paused = false;
        pauseMenu.OptionsMenu = false;
    }


    public static Color RandomColor(float minValue, float maxValue)
    {
        Color result = Color.white;
        minValue = Mathf.Clamp(minValue, 0, 1);
        maxValue = Mathf.Clamp(maxValue, 0, 1);

        float r = Random.Range(minValue, maxValue);
        float g = Random.Range(minValue, maxValue);
        float b = Random.Range(minValue, maxValue);

        result = new Color(r, g, b, 1.0f);

        return (result);
    }

    public static void ToggleGodMode()
    {
        GodMode = !GodMode;
    }

    public static float MapRange(float s, float a1, float a2, float b1, float b2)
    {

        return ( b1 + (s - a1) * (b2 - b1) / (a2 - a1) );

    }

    public static float MapRangeClamp(float s, float a1, float a2, float b1, float b2)
    {
        float result = ( b1 + (s - a1) * (b2 - b1) / (a2 - a1) );
        return ( Mathf.Clamp(result, b1, b2) );

    }

}
