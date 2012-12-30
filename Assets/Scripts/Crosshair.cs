using UnityEngine;
using System.Collections;

public class Crosshair : MonoBehaviour {
	
	public Texture texture;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void OnGUI()
	{
		if (Main.Paused == false)
		{
			GUI.DrawTexture(new Rect((Screen.width / 2) - (texture.width / 2), (Screen.height / 2) - (texture.height / 2), texture.width, texture.height), texture);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
