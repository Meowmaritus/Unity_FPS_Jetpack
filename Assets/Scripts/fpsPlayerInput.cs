using UnityEngine;
using System.Collections;

////public enum PlayerStates
////{
////	Idle,
////	WalkStart,
////	Walking,
////	RunStart,
////	Running,
////	ShootStart,
////	Shooting
////}

public class fpsPlayerInput : MonoBehaviour {
	public GameObject bulletPF;
	public GameObject gun;
	public float fireRate = 0.5f;
	public int maxBullets = 15;
	private int bullets;
	private bool reloading;
	private bool mouseDown;
	private float nextFire = 0.0f;
	public AnimationClip shootLoopAnimation;
	public AudioSource AS;
	public AudioClip Gunshot;
	
	public bool Idling = true;
	
	////private PlayerStates _state = PlayerStates.Idle;
	
	////public PlayerStates State
	////{
	////	get { return(_state); }
	////	set 
	////	{ 
	////		if (value != _state)
	////		{				
	////			DoStateAnimation();
	////		}
	////		
	////		_state = value;
	////		
	////	}
	////}
	
	void Start () {
		StartIdling();
		bullets = maxBullets;
		reloading = false;
		mouseDown = false;
	}
	
	float MapRange(float s, float a1, float a2, float b1, float b2){
		return b1 + ((s - a1)*(b2 - b1))/(a2 - a1);
	}
	
	void Update () {
		GameGUI.bulletCount = bullets;			
		
		if (Input.GetButtonUp("Fire Rate Up"))
			fireRate += 0.05f;
		if (Input.GetButtonUp("Fire Rate Down"))
			fireRate -= 0.05f;
		
		if (Input.GetButtonUp("Reload")) {
			reloadBullets();
		}
		
		if (!reloading) {
			
			if (Input.GetMouseButtonDown(0)) {
				mouseDown = true;
				StopIdling();
				gun.animation.Play("shootStart");
				gun.animation.PlayQueued("shootLoop", QueueMode.PlayNow, PlayMode.StopAll);				
				
			}
			
			if (Input.GetMouseButton(0) && Time.time > nextFire) {
				
				if (bullets == 0) {
					
					reloadBullets();
					
				}
				else {
					
					if (!mouseDown) {
						
						mouseDown = true;
						StopIdling();
						gun.animation.Play("shootStart");
						gun.animation.PlayQueued("shootLoop");
						
					}
					nextFire = Time.time + fireRate;
					Instantiate(bulletPF, transform.position, transform.rotation*bulletPF.transform.rotation);
					AS.PlayOneShot(Gunshot);
					bullets--;
					
				}
			}
			if (Input.GetMouseButtonUp(0)) {
				gun.animation.PlayQueued("shootEnd");
				gun.animation.Stop("shootLoop");
				gun.animation.PlayQueued("idle");										
			}
			
			if (Input.GetMouseButton(0) == false)
			{
				
				if (fpsPlayerMove.IsMoving == true)
				{
					
					if (fpsPlayerMove.boost)
					{
						if (gun.animation.IsPlaying("sprintLoop") == false)
						{
							gun.animation.Play("sprintStart");
						}
						
						gun.animation.PlayQueued("sprintLoop");
					}
					else
					{					
						if (gun.animation.IsPlaying("walkLoop") == false)
						{
							gun.animation.Play("walkStart");
						}
						
						gun.animation.PlayQueued("walkLoop");
					}
					
				}
				else
				{
					if (gun.animation.IsPlaying("sprintLoop"))
					{											
						gun.animation.Stop("sprintLoop");
						gun.animation.Blend("sprintEnd");
						gun.animation.PlayQueued("idle");
					}
					else if (gun.animation.IsPlaying("walkLoop"))
					{
						gun.animation.Stop ("walkLoop");
						gun.animation.PlayQueued("idle");
					}
					
				}
				
			}
			
		}
		if (reloading && Idling) {
			reloading = false;
			if (bullets == 0)
				bullets = maxBullets;
			else
				bullets = maxBullets+1;
		}
		
		
		
		
		
	}	
	
	void StartIdling()
	{
		Idling = true;
		gun.animation.PlayQueued("idle");	
	}
	
	void StopIdling()
	{
		Idling = false;
		gun.animation.Stop("idle");
		gun.animation.Stop("walkStart");
		gun.animation.Stop("walkLoop");
		gun.animation.Stop("sprintStart");
		gun.animation.Stop("sprintLoop");
		gun.animation.Stop("sprintEnd");
	}
		
	
	////void DoStateAnimation()
	////{
	////	switch(State)
	////	{
	////		case PlayerStates.Idle:					
	////			gun.animation.PlayQueued("idle");
	////		
	////			if (
	////		break;
	////		
	////		case PlayerStates.WalkStart:			
	////		break;
	////		
	////		case PlayerStates.Walking:
	////		break;
	////		
	////		case PlayerStates.RunStart:
	////		break;
	////		
	////		case PlayerStates.Running:
	////		break;
	////		
	////		case PlayerStates.ShootStart:
	////		break;
	////		
	////		case PlayerStates.Shooting:
	////		break;
	////	}
	////}
	
	void reloadBullets() {
		if (!reloading && bullets < maxBullets+1) {
			mouseDown = false;
			reloading = true;
			gun.animation.Stop();
			if (bullets == 0) {
				gun.animation.Play("reloadEmptyStart");
				gun.animation.PlayQueued("reloadEmptyEnd");
				//bullets = maxBullets;
			}
			else {
				gun.animation.Play("reload");
				//bullets = maxBullets + 1;
			}
			gun.animation.PlayQueued("idle");
		}
	}
}
