using UnityEngine;
using System.Collections;

public class MuzzleFlashSpriteObject : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void SetVisibility(bool visibility) 
	{
	    // toggles the visibility of this gameobject and all it's children
	    Renderer[] renderers = gameObject.GetComponentsInChildren<Renderer>();
	    
	    foreach (Renderer r in renderers) 
		{
	        r.enabled = visibility;
	    }
	}

	
}
