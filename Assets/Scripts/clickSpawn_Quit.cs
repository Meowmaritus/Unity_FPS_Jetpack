using UnityEngine;
using System.Collections;

public class clickSpawn_Quit : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Application.LoadLevel("Title Screen");	
		}
	}
	
	void OnGUI()
	{
		Screen.lockCursor = true;
		Screen.showCursor = false;	
	}
}
