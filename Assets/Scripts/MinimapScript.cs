using UnityEngine;
using System.Collections;

public class MinimapScript : MonoBehaviour {

    public Camera MinimapCam;
    public float MinOrthographicSize;
    public float MaxOrthographicSize;
    public float OrthographicSizeLerp = 0.5f;
    public float OrthographicSizeChangeSpeed = 1.0f;
    public float DefaultOrthographicSize = 1.0f;
    private float currentOrthographicSize = 0.0f;
    private float oldOrthographicSize = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
        float smoothSize = Mathf.Lerp(currentOrthographicSize, oldOrthographicSize, OrthographicSizeLerp);

        currentOrthographicSize = Mathf.Clamp(smoothSize, MinOrthographicSize, MaxOrthographicSize);

        if (Input.GetKey(KeyCode.KeypadPlus))
        {
            currentOrthographicSize -= OrthographicSizeChangeSpeed;
        }

        if (Input.GetKey(KeyCode.KeypadMinus))
        {
            currentOrthographicSize += OrthographicSizeChangeSpeed;
        }

        if (Input.GetKey(KeyCode.KeypadEnter))
        {
            currentOrthographicSize = DefaultOrthographicSize;
        }

        MinimapCam.orthographicSize = smoothSize;

        oldOrthographicSize = smoothSize;
	}
}
