using UnityEngine;
using System.Collections;

public class TitleScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.Quit();	
		}
	}
	
	void OnGUI()
	{
		if (Input.compositionCursorPos.x >= 0 &&
			Input.compositionCursorPos.x <= Screen.width &&
			Input.compositionCursorPos.y >= 0 &&
			Input.compositionCursorPos.y <= Screen.height)
		{
		
			Screen.lockCursor = false;
			Screen.showCursor = true;
			
			GUI.BeginGroup(new Rect((Screen.width / 2) - 128, (Screen.height / 2) - 56, 256, 112));
			
				if(GUI.Button(new Rect(0,0,256,32), "Start FPS-Jetpack Game"))
				{
					Application.LoadLevel("fps-testing");
				}
			
				if(GUI.Button(new Rect(0,40,256,32), "Options (None Yet)"))
				{
					
				}
			
				if(GUI.Button(new Rect(0,80,256,32), "Quit"))
				{
					Application.Quit();
				}
			
			GUI.EndGroup();
			
		}
	}
}
