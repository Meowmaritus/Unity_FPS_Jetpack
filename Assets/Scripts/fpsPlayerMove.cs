using UnityEngine;
using System.Collections;

[AddComponentMenu("_Main/Player/Player Mover")]
public class fpsPlayerMove : MonoBehaviour {
	
	public float speed;
	public float jetpackForce;
	public float rotSpeed;
	public float maxYangle;
	public float terminalVelocity = 45f;
	public float terminalVelocityY = 40f;
	
	public float MinPitch = -180f;
	public float MaxPitch = 180f;
	
	public Camera playerCam;
	public ParticleSystem jetpackParticles;
	public bool Sprinting;
	public bool onGround;
	public float MovementInputSmooth; // 1.0f - no movement cuz its so smoothed, 0.0f = full movement; no smooth...//
    public float BoostSpeedSmooth;    //<---Same here//
    public float BoostMult = 20.0f;
	
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
	
	public float gravityConstant = 9.81f;
	
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
		
		
		// attempt 1
		if (!onGround && !Jetpack) {
			rigidbody.drag = 0.05f;
			Vector3 v = rigidbody.velocity;
			v = v.normalized * Mathf.Clamp(v.magnitude, -terminalVelocity, terminalVelocity);
			rigidbody.velocity = v;
		}
		else {
			rigidbody.drag = 1f;
		}
		
		//attempt 2
		/*if (rigidbody.velocity.y < -0.1f) {
			rigidbody.drag = 0f;
			Vector3 v = rigidbody.velocity;
			rigidbody.velocity = new Vector3(v.x, Mathf.Clamp(v.y, -terminalVelocityY, 0), v.z);
		}
		else {
			rigidbody.drag = 1f;
		}*/
		
		
		//Debug.Log(rigidbody.velocity);
		
		/*rigidbody.AddForce(rigidbody.mass*Physics.gravity);
		if (!onGround) {
			float idealDrag = 1 / terminalVelocity;
			rigidbody.drag = idealDrag / (idealDrag * Time.fixedDeltaTime + 1);
		}
		else {
			rigidbody.drag = 1f;
		}*/
		
		GravityUpdate();
    }
	
	void GravityUpdate() {
		
		if (GravityObject.globalGravityOn) {
			rigidbody.useGravity = false;
			
			Vector3 toCenter = GravityObject.mainObject.GetGravityFor(transform.position);
			rigidbody.AddForce(-toCenter.normalized * GravityObject.mainObject.gravityForce * gravityConstant);
			
			Quaternion lookAt = Quaternion.LookRotation(transform.forward, toCenter);
			transform.rotation = Quaternion.Lerp(transform.rotation, lookAt, Time.smoothDeltaTime);
			//transform.rotation = lookAt;
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

        GameGUI.DebugValue["playerCam.transform.localEulerAngles.x"] = playerCam.transform.localEulerAngles.x;
        GameGUI.DebugValue["playerCam.transform.eulerAngles.x"] = playerCam.transform.eulerAngles.x;

        float anglex = playerCam.transform.localEulerAngles.x;
        float angley = playerCam.transform.localEulerAngles.y;
        float anglez = playerCam.transform.localEulerAngles.z;

        //anglex = ClampEulerAngle(anglex, MinPitch, MaxPitch);

        GameGUI.DebugValue["playerCam pitch limiter - clamped pitch"] = anglex;

        playerCam.transform.localEulerAngles = new Vector3(anglex, angley, anglez);
		

        //float yRot = -MouseMove.y * rotSpeed * Time.deltaTime;
        //transform.Rotate(0, rotSpeed * Time.deltaTime * MouseMove.x, 0);
        //playerCam.transform.Rotate(yRot, 0, 0, Space.Self);
        //if (!cameraWithinAngle())
        //    playerCam.transform.Rotate(-yRot, 0, 0, Space.Self);

        oldMouseMove = MouseMove;
    }

    float ClampEulerAngle(float angle, float min, float max)
    {
        float temp1low = Main.MapRangeClamp(angle, 0, 180, 0, -180);
        float temp1high = Main.MapRangeClamp(angle, 180, 360, 180, 0);

        float mapped1 = (temp1low + temp1high);

        float clamped = Mathf.Clamp(mapped1, min, max);

        float temp2low = Main.MapRangeClamp(clamped, 0, 180, 360, 180);
        float temp2high = Main.MapRangeClamp(clamped, -180, 0, 180, 0);

        return (temp2low + temp2high);     
    }

    void DoUpdate_Paused()
    {

    }
	
	
	void OnCollisionStay(Collision other) {
					
		onGround = true;

	}
	
	void OnCollisionExit(Collision other)
	{							
		onGround = false;	
	}

    void OnTriggerStay(Collider other)
    {		
		if (other.gameObject.tag == "Water")
		{
			IsUnderwater = true;
		}
	}
	
	void OnTriggerEnter(Collider other) {
		
		if (other.gameObject.tag == "Water")
		{
			IsUnderwater = true;
		}
	}

    void OnTriggerExit(Collider other)
	{				
		if (other.gameObject.tag == "Water")
		{
			IsUnderwater = false;
		}	
	}

    public Quaternion AimDirectionFromTransform(Transform sourceTransform)
    {
        Ray centerRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit center;

        Physics.Raycast(centerRay, out center);

        Vector3 rayrotation = (center.point - sourceTransform.position).normalized;


        if (Debug.isDebugBuild)
        {
            Debug.DrawRay(sourceTransform.position, (center.point - sourceTransform.position), Main.RandomColor(0.5f, 1.0f), 5f, true);
        }


        return (Quaternion.Euler(rayrotation));
    }

    public Vector3 AimDestination()
    {
        Ray centerRay = playerCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));

        RaycastHit center;

        Physics.Raycast(centerRay, out center);

        return (center.point);
    }
}
