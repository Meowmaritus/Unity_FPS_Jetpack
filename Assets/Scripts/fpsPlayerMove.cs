using UnityEngine;
using System.Collections;

public class fpsPlayerMove : MonoBehaviour {
	
	public float speed;
	public float jetpackForce;
	public float rotSpeed;
	public float maxYAngle;
	public Camera playerCam;
	public ParticleSystem jetpackParticles;
	public static bool boost;
	private bool onGround;
	public float MovementInputSmooth; // 1.0f - no movement cuz its so smoothed, 0.0f = full movement; no smooth...//
    public float BoostSpeedSmooth;    //<---Same here//
    public float BoostMult = 2.0f;	
	
	
	public static bool IsMoving = false;
	
    private float oldBoost = 0;
    private float currentBoost = 0;
	
	private Vector2 prevInput = new Vector2(0,0);
	private Vector2 currentInput = new Vector2(0,0);    
	
	void Start () {
		
		currentBoost = BoostMult;
		boost = false;
		onGround = true;
	}	
	
	void Update () {		
        currentInput.x = Mathf.Lerp(Input.GetAxis("Horizontal"), prevInput.x, MovementInputSmooth);
        currentInput.y = Mathf.Lerp(Input.GetAxis("Vertical"), prevInput.y, MovementInputSmooth);		
		
		if (boost)
		{
			currentBoost = Mathf.Lerp(BoostMult, oldBoost, BoostSpeedSmooth);	
		}
		else
		{
			currentBoost = Mathf.Lerp(1, oldBoost, BoostSpeedSmooth);	
		}

        Vector3 t = new Vector3(currentInput.x, 0, currentInput.y);
		t *= currentBoost;
		
		GameGUI.DebugValue["Current Boost Mult"] = currentBoost;
		GameGUI.DebugValue["Smoothed X Input:"] = currentInput.x;
		GameGUI.DebugValue["Smoothed Y Input:"] = currentInput.y;
		
		IsMoving = (((float)((int)(t.x * 100)) / 100) != 0 || ((float)((int)(t.z * 100)) / 100) != 0);
		GameGUI.DebugValue["IsMoving"] = IsMoving;
		GameGUI.DebugValue["IsMoving - t.x"] = ((float)((int)(t.x * 100)) / 100);
		GameGUI.DebugValue["IsMoving - t.z"] = ((float)((int)(t.z * 100)) / 100);
		
		if (Input.GetButtonDown("SpeedBoost"))
			boost = true;
		else if (Input.GetButtonUp("SpeedBoost"))
			boost = false;        	
		
		if (Input.GetButtonDown("JetPack")) {
			jetpackParticles.Play();
		}
		else if (Input.GetButton("JetPack")) {
			Vector3 r = playerCam.transform.rotation.eulerAngles;
			float y = (90+r.x) % 360;
			r = new Vector3(0f, (y > 90) ? 90-(y%90) : y, -(90-y)).normalized;
			rigidbody.AddRelativeForce(r*jetpackForce);
		}
		else {
			jetpackParticles.Stop();
		}
		
		transform.Translate(speed*Time.deltaTime*t, Space.Self);
		transform.Rotate(0,rotSpeed*Time.deltaTime*Input.GetAxis("Mouse X"),0);
		//transform.Rotate(0, Input.GetAxis("Mouse X")*rotSpeed*Time.deltaTime, 0, Space.World);
		playerCam.transform.Rotate(-Input.GetAxis("Mouse Y")*rotSpeed*Time.deltaTime, 0, 0, Space.Self);
		
		prevInput = currentInput;

        oldBoost = currentBoost;
	}
	
	
	/*void OnCollisionStay(Collision other) {
		Ray ray = new Ray(transform.position, Vector3.down);
		
	}*/
}
