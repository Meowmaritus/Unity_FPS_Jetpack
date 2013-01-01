using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenu : MonoBehaviour {

    public Main main;

    public GUISkin MAIN_GUISKIN;

    public float ButtonBoxOffset = 0.0f;

    public GUIContent GUIContent_Resume = new GUIContent("Resume");
    public GUIContent GUIContent_Options = new GUIContent("Options");
    public GUIContent GUIContent_QuitToTitle = new GUIContent("Quit To Title");
    public GUIContent GUIContent_QuitToDesktop = new GUIContent("Quit To Desktop");
    public GUIContent GUIContent_Paused = new GUIContent("Paused");
				
	public GUIContent GUIContent_Options_MB = new GUIContent("Motion Blur:");
	public GUIContent GUIContent_Options_Back = new GUIContent("Back");
	public GUIContent GUIContent_Options_VG = new GUIContent("Vignetting:");
	public GUIContent GUIContent_Options_CA = new GUIContent("Chromatic Abberation:");
	public GUIContent GUIContent_Options_CR = new GUIContent("Creasing:");
	public GUIContent GUIContent_Options_AO = new GUIContent("Ambient Occlusion:");
	public GUIContent GUIContent_Options_PPAA = new GUIContent("Post-Processed Anti-Aliasing:");
	private int GUIContent_Options_MB_SelectedIndex = 0;
	private int GUIContent_Options_VG_SelectedIndex = 0;
	private int GUIContent_Options_CA_SelectedIndex = 0;
	private int GUIContent_Options_CR_SelectedIndex = 0;
	private int GUIContent_Options_AO_SelectedIndex = 0;
	private int GUIContent_Options_PPAA_SelectedIndex = 0;
	private int GUIContent_DebugDisplay_SelectedIndex = 1;
	
	public GUIContent[] GUIContent_Options_MB_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_Options_VG_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_Options_CA_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_Options_CR_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_Options_AO_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_Options_PPAA_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("Low"),new GUIContent("High") };
	public GUIContent[] GUIContent_DebugDisplay_Choices = new GUIContent[] { new GUIContent("Off"),new GUIContent("On") };

    public bool OptionsMenu = false;

    void fillWindow(int winID) 
    { 
	    switch(winID)
        {
		    default:
			    Debug.Log("Default case reached");
                break;
	    }
	    GUI.DragWindow (new Rect(0,0, 10000, 10000));
    }

	// Use this for initialization
	void Start () {
        GUIContent_DebugDisplay_SelectedIndex = 1;
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

    void OnGUI()
    {
        GUI.skin = MAIN_GUISKIN;

        if (Main.Paused == true)
        {
			GUI.Box(new Rect(-32, -32, Screen.width + 64, Screen.height + 64), "");
			
            if (OptionsMenu == false)
            {
                GUI.Label(new Rect(Screen.width / 2 + -150, Screen.height / 2 + -135, 300, 50), GUIContent_Paused);

                if (Debug.isDebugBuild == true)
                {                    
                    GUI.Box(new Rect(Screen.width / 2 + -200, 10, 400, 50), "Debug Display:");

                    GUIContent_DebugDisplay_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 31 + ButtonBoxOffset, 390, 24), GUIContent_DebugDisplay_SelectedIndex, GUIContent_DebugDisplay_Choices);

                    if (GUI.changed)
                    {

                        switch (GUIContent_DebugDisplay_SelectedIndex)
                        {
                            case (0):
                                main.ShowDebug = false;
                                break;

                            case (1):
                                main.ShowDebug = true;
                                break;
                        }

                    }

                }

                if (GUI.Button(new Rect(Screen.width / 2 + -128, Screen.height / 2 + -72, 256, 24), GUIContent_Resume))
                {
                    main.UnPause();
                }

                if (GUI.Button(new Rect(Screen.width / 2 + -128, Screen.height / 2 + -42, 256, 24), GUIContent_Options))
                {
                    OptionsMenu = true;
                }

                if (GUI.Button(new Rect(Screen.width / 2 + -128, Screen.height / 2 + -12, 256, 24), GUIContent_QuitToTitle))
                {
                    main.QuitToTitle();
                }

                if (GUI.Button(new Rect(Screen.width / 2 + -128, Screen.height / 2 + 18, 256, 24), GUIContent_QuitToDesktop))
                {
                    main.QuitToDesktop();
                }								                
            }
            else
            {								
				if(GUI.Button(new Rect(Screen.width/2 + -50, Screen.height-24, 100, 24), GUIContent_Options_Back))
				{
					
					OptionsMenu = false;
					
				}
				
				GUI.Box(new Rect(Screen.width/2 + -200, 10, 400, 51), GUIContent_Options_MB);
				
				GUI.Box(new Rect(Screen.width/2 + -200, 70, 400, 51), GUIContent_Options_VG);
				
				GUI.Box(new Rect(Screen.width/2 + -200, 130, 400, 51), GUIContent_Options_CA);
				
				GUI.Box(new Rect(Screen.width/2 + -200, 190, 400, 51), GUIContent_Options_CR);
				
				GUI.Box(new Rect(Screen.width/2 + -200, 250, 400, 51), GUIContent_Options_AO);
				
				GUI.Box(new Rect(Screen.width/2 + -200, 310, 400, 51), GUIContent_Options_PPAA);

                GUIContent_Options_MB_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 31 + ButtonBoxOffset, 390, 24), GUIContent_Options_MB_SelectedIndex, GUIContent_Options_MB_Choices);
                GUIContent_Options_VG_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 91 + ButtonBoxOffset, 390, 24), GUIContent_Options_VG_SelectedIndex, GUIContent_Options_VG_Choices);
                GUIContent_Options_CA_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 151 + ButtonBoxOffset, 390, 24), GUIContent_Options_CA_SelectedIndex, GUIContent_Options_CA_Choices);
                GUIContent_Options_CR_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 211 + ButtonBoxOffset, 390, 24), GUIContent_Options_CR_SelectedIndex, GUIContent_Options_CR_Choices);
                GUIContent_Options_AO_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 271 + ButtonBoxOffset, 390, 24), GUIContent_Options_AO_SelectedIndex, GUIContent_Options_AO_Choices);
                GUIContent_Options_PPAA_SelectedIndex = GUI.Toolbar(new Rect(Screen.width / 2 + -195, 331 + ButtonBoxOffset, 390, 24), GUIContent_Options_PPAA_SelectedIndex, GUIContent_Options_PPAA_Choices);
				
				if(GUI.changed)
				{
					
					switch (GUIContent_Options_MB_SelectedIndex)
					{
						
						case (0):
							main.Options_MotionBlur = Option.Off;
							break;
						
						case (1):
							main.Options_MotionBlur = Option.Low;
							break;
						
						case (2):
							main.Options_MotionBlur = Option.High;
							break;
						
					}
					
					switch (GUIContent_Options_VG_SelectedIndex)
					{
						
						case (0):
							main.Options_Vignetting = Option.Off;
							break;
						
						case (1):
							main.Options_Vignetting = Option.Low;
							break;
						
						case (2):
							main.Options_Vignetting = Option.High;
							break;
						
					}
					
					switch (GUIContent_Options_CA_SelectedIndex)
					{
						
						case (0):
							main.Options_ChromaticAbberation = Option.Off;
							break;
						
						case (1):
							main.Options_ChromaticAbberation = Option.Low;
							break;
						
						case (2):
							main.Options_ChromaticAbberation = Option.High;
							break;
						
					}
					
					switch (GUIContent_Options_CR_SelectedIndex)
					{
						
						case (0):
							main.Options_Creasing = Option.Off;
							break;
						
						case (1):
							main.Options_Creasing = Option.Low;
							break;
						
						case (2):
							main.Options_Creasing = Option.High;
							break;
						
					}
					
					switch (GUIContent_Options_AO_SelectedIndex)
					{
						
						case (0):
							main.Options_AmbientOcclusion = Option.Off;
							break;
						
						case (1):
							main.Options_AmbientOcclusion = Option.Low;
							break;
						
						case (2):
							main.Options_AmbientOcclusion = Option.High;
							break;
						
					}
					
					switch (GUIContent_Options_PPAA_SelectedIndex)
					{
						
						case (0):
							main.Options_PostProcessedAntiAliasing = Option.Off;
							break;
						
						case (1):
							main.Options_PostProcessedAntiAliasing = Option.Low;
							break;
						
						case (2):
							main.Options_PostProcessedAntiAliasing = Option.High;
							break;
						
					}
					
				}

            }
			
        }
		else
		{
            if (Debug.isDebugBuild == true)
            {
                if (Main.GodMode)
                {
                    GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height - 48, 400, 24), "GOD MODE ACTIVE");
                }
                else
                {
                    GUI.Box(new Rect(Screen.width / 2 - 200, Screen.height - 48, 400, 24), "Press G to enable God Mode.");
                }
            }
		}
    }

    void DoFixedUpdate()
    {

    }

    void DoFixedUpdate_Paused()
    {

    }

    void DoUpdate()
    {
        
    }

    void DoUpdate_Paused()
    {
        
    }
}
