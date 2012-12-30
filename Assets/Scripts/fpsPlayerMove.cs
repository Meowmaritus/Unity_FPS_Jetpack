using UnityEngine;
using System.Collections;

[AddComponentMenu("_Main/Player/Player Mover")]
public class fpsPlayerMove : MonoBehaviour {
	
	public float speed;
	public float jetpackForce;
	public float rotSpeed;
	public float maxYangle;	
	public Camera playerCam;
	public ParticleSystem jetpackParticles;
	public bool Sprinting;
	public bool onGround;
	public float MovementInputSmooth; // 1.0f - no movement cuz its so smoothed, 0.0f = full movement; no smooth...//
    public float BoostSpeedSmooth;    //<---Same here//
    public float BoostMult = 2.0f;
	
	public bool IsUnderwater = false;
	
	public float MouseSpeedMult = 1.5f;
	public float MouseLerp = 0.5f;
	
	private Vector2 MouseMove = new Vector2(0, 0);
	private Vector2 oldMouseMove = new Vector2(0, 0);
	
	public bool IsMoving = false;
	
	public bool CanSprint = true;
	
    private float oldBoost = 0;
    private float currentBoost = 0;
	
	private Vector2 prevInput = new Vector2(0,0);
	private Vector2 currentInput = new Vector2(0,0);   
	
	public bool Jetpack = false;
	public bool CanUseJetpack = true;
	
	void Start () {
		
		currentBoost = BoostMult;
		Sprinting = false;
		onGround = true;
	}

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

    void DoFixedUpdate()
    {

        Jetpack = Input.GetButton("JetPack");

        if (CanUseJetpack)
        {
            if (Input.GetButtonDown("JetPack"))
            {
                jetpackParticles.Play();
            }
            else if (Input.GetButton("JetPack"))
            {
                Jetpack = true;
                Vector3 r = playerCam.transform.rotation.eulerAngles;
                float y = (135 + r.x) % 360;
                r = new Vector3(0f, (y > 135) ? 90 - (y % 90) : y, -(90 - y));
                rigidbody.AddRelativeForce(r.normalized * jetpackForce);
            }
            else
            {
                Jetpack = false;
                jetpackParticles.Stop();
            }
        }
        else
        {
            Jetpack = false;
            jetpackParticles.Stop();
        }

    }

    void DoFixedUpdate_Paused()
    {

    }

    void DoUpdate()
    {
        GameGUI.DebugValue["onGround"] = onGround;
		GameGUI.DebugValue["IsUnderwater"] = IsUnderwater;

        float mousex = Input.GetAxis("Mouse X") + (Input.GetAxis("Right Stick X") * 1.5f);
        float mousey = Input.GetAxis("Mouse Y") + (Input.GetAxis("Right Stick Y") * 1.5f);

        MouseMove.x = Mathf.Lerp(mousex, oldMouseMove.x, MouseLerp);
        MouseMove.y = Mathf.Lerp(mousey, oldMouseMove.y, MouseLerp);

        currentInput.x = Mathf.Lerp(Input.GetAxis("Horizontal"), prevInput.x, MovementInputSmooth);
        currentInput.y = Mathf.Lerp(Input.GetAxis("Vertical"), prevInput.y, MovementInputSmooth);

        Sprinting = (Sprinting && IsMoving);

        if (Sprinting == true && Jetpack == false)
        {
            currentBoost = Mathf.Lerp(BoostMult, oldBoost, BoostSpeedSmooth);
        }
        else
        {
            currentBoost = Mathf.Lerp(1, oldBoost, BoostSpeedSmooth);
        }

        Vector3 t = new Vector3(currentInput.x, 0, currentInput.y);
        t *= currentBoost;

        //GameGUI.DebugValue["Current Boost Mult"] = currentBoost;
        GameGUI.DebugValue["Smoothed X Input:"] = currentInput.x;
        GameGUI.DebugValue["Smoothed Y Input:"] = currentInput.y;

        IsMoving = (((float)((int)(t.x * 100)) / 100) != 0 || ((float)((int)(t.z * 100)) / 100) != 0);
        GameGUI.DebugValue["IsMoving"] = IsMoving;
        GameGUI.DebugValue["IsMoving - t.x"] = ((float)((int)(t.x * 100)) / 100);
        GameGUI.DebugValue["IsMoving - t.z"] = ((float)((int)(t.z * 100)) / 100);

        if (CanSprint)
        {
            if (Input.GetButtonDown("SpeedBoost"))
            {
                Sprinting = true;
            }
            else if (Input.GetButtonUp("SpeedBoost"))
            {
                Sprinting = false;
            }
        }
        else
        {
            Sprinting = false;
        }



        transform.Translate(speed * Time.deltaTime * t, Space.Self);


        prevInput = currentInput;

        oldBoost = currentBoost;

        float yRot = -MouseMove.y * rotSpeed;
        transform.Rotate(0, rotSpeed * MouseMove.x, 0);
        playerCam.transform.Rotate(yRot, 0, 0, Space.Self);
        if (!cameraWithinAngle())
            playerCam.transform.Rotate(-yRot, 0, 0, Space.Self);

        //float yRot = -MouseMove.y * rotSpeed * Time.deltaTime;
        //transform.Rotate(0, rotSpeed * Time.deltaTime * MouseMove.x, 0);
        //playerCam.transform.Rotate(yRot, 0, 0, Space.Self);
        //if (!cameraWithinAngle())
        //    playerCam.transform.Rotate(-yRot, 0, 0, Space.Self);

        oldMouseMove = MouseMove;
    }

    void DoUpdate_Paused()
    {

    }
	
	bool cameraWithinAngle() {
		float y = ((playerCam.transform.localRotation.eulerAngles.x + 90) % 360);
		return (y > maxYangle && y < 180-maxYangle);
	}
	
	
	void OnCollisionStay(Collision other) {
			if (other.gameObject.tag == "Water")
			{
				IsUnderwater = true;
			}
			else
			{				
				onGround = true;
			}
	}
	
	void OnCollisionExit(Collision other)
	{				
		if (other.gameObject.tag == "Water")
			{
				IsUnderwater = false;
			}
			else
			{				
				onGround = false;
			}		
	}
}
