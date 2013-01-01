using UnityEngine;
using System.Collections;

[AddComponentMenu("_Main/Player/Player Handler")]
public class PlayerHandler : MonoBehaviour {	
	public float Health = 1.0f;
	public float HealthLerp = 1.0f;
	public float SmoothHealth = 0.0f;
	public float HealthDelta = 0;
	private float oldHealth = 0;
	
	public float Stamina = 1.0f;
	public float StaminaLerp = 1.0f;
	public float SmoothStamina = 0.0f;
	public float StaminaDelta = 0;
	private float oldStamina = 0;
	
	public float JetpackFuel = 1.0f;
	public float JetpackFuelLerp = 1.0f;
	public float SmoothJetpackFuel = 0.0f;
	public float JetpackFuelDelta = 1.0f;
	private float oldJetpackFuel = 0;
	
	public bool IsUpdatingTempHealth = false;
	public bool IsUpdatingTempStamina = false;
	public bool IsUpdatingTempJetpackFuel = false;
	
	public float HealthBarTempUpdateDelay = 0.0f;
	private float nextHealthBarTempUpdate = 0.0f;
	
	public float StaminaBarTempUpdateDelay = 0.0f;
	private float nextStaminaBarTempUpdate = 0.0f;
	
	public float JetpackFuelBarTempUpdateDelay = 0.0f;
	private float nextJetpackFuelBarTempUpdate = 0.0f;

	public float SprintStaminaLoss; //0.0025//
	public float IdleStaminaRegen;//0.001//
	public float WalkingStaminaRegen;//0.0005//
	
	public float JetpackFuelLoss;
	public float JetpackFuelRegen;
	
	public WeaponInventory WeaponInventory;
	public Weapon CurrentWeapon;
	public fpsPlayerMove PlayerMover;
	public AudioSource _AudioSource;
	public AudioSource _JetpackAudioSource;
	public AudioClip[] HurtSounds;
	public AudioClip[] RegularFootstepSounds;
	public AudioClip[] SprintingFootstepSounds;
	public float RegularFOV = 34;
	public float SprintingFOVMod = 0.0f;
	public float SprintingFOVLerp = 0.5f;
	private float currentFOVMod = 0.0f;
	private float oldFOVMod = 0.0f;
	public float FootstepDelay = 1.0f;
	private float nextFootstep = 0.0f;
	public float SprintingFootstepDelay = 1.0f;
	private float nextSprintingFootstep = 0.0f;
	
	// Use this for initialization
	void Start() 
	{
		Health = 1.0f;
		Stamina = 1.0f;
		CurrentWeapon = this.WeaponInventory.Weapons[0];			
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
		
		if (Time.time > nextHealthBarTempUpdate)
		{
			IsUpdatingTempHealth = true;			
		}
		else
		{
			IsUpdatingTempHealth = false;	
		}
		
		
		if (Time.time > nextStaminaBarTempUpdate)
		{
			IsUpdatingTempStamina = true;
		}
		else
		{
			IsUpdatingTempStamina = false;	
		}
		
		
		if (Time.time > nextJetpackFuelBarTempUpdate)
		{
			IsUpdatingTempJetpackFuel = true;				
		}
		else
		{
			IsUpdatingTempJetpackFuel = false;	
		}
		
	}

    void DoFixedUpdate_Paused()
    {

    }
	
	void DoUpdate()
	{        
        if (Debug.isDebugBuild == true && Input.GetKeyDown(KeyCode.G))
        {
            Main.ToggleGodMode();
        }

        if (Main.GodMode)
        {
            for (int i = 0; i < WeaponInventory.Weapons.Length; i++)
            {
                WeaponInventory.Weapons[i].MaxAmmoTotal = 999999999;
                WeaponInventory.Weapons[i].MaxAmmoPerClip = 999999999;

                WeaponInventory.Weapons[i].AmmoTotal = 999999999;
                WeaponInventory.Weapons[i].AmmoInClip = 999999999;
            }

            Health = 1.0f;
            Stamina = 1.0f;
            JetpackFuel = 1.0f;
        }

		PlayerMover.playerCam.fov = currentFOVMod;
		
		HealthDelta = (Health - oldHealth);
		StaminaDelta = (Stamina - oldStamina);
		JetpackFuelDelta = (JetpackFuel - oldJetpackFuel);
		
		SmoothHealth = Mathf.Lerp(Health, oldHealth, HealthLerp);
		SmoothStamina = Mathf.Lerp(Stamina, oldStamina, StaminaLerp);
		SmoothJetpackFuel = Mathf.Lerp(JetpackFuel, oldJetpackFuel, JetpackFuelLerp);
		
		GameGUI.DebugValue["IsUpdatingTempHealth"] = IsUpdatingTempHealth;
		GameGUI.DebugValue["IsUpdatingTempStamina"] = IsUpdatingTempStamina;
		GameGUI.DebugValue["IsUpdatingTempJetpackFuel"] = IsUpdatingTempJetpackFuel;						
		
		
		////if (HealthDelta != 0)
		////{
		////	nextHealthBarTempUpdate = Time.time + HealthBarTempUpdateDelay;	
		////}
		////
		////if (StaminaDelta != 0)
		////{
		////	nextStaminaBarTempUpdate = Time.time + StaminaBarTempUpdateDelay;	
		////}
		////
		////if (JetpackFuelDelta != 0)
		////{
		////	nextJetpackFuelBarTempUpdate = Time.time + JetpackFuelBarTempUpdateDelay;	
		////}
		
		
		if (Input.GetKeyDown(KeyCode.Tab))
		{
			CurrentWeapon.CycleState();	
		}
		
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			CurrentWeapon.ToggleSafety();	
		}
		
		if (CurrentWeapon.CurrentMovement == MovementState.Walking)
		{
			if(Time.time > nextFootstep)
			{
				MakeFootstep(false);
			}
		}
		else if (CurrentWeapon.CurrentMovement == MovementState.Sprinting)
		{
			if(Time.time > nextSprintingFootstep)
			{
				MakeFootstep(true);
			}
		}
		
		if (CurrentWeapon.CurrentMovement == MovementState.Sprinting)
		{
			Tire(SprintStaminaLoss);
			currentFOVMod = Mathf.Lerp(RegularFOV + SprintingFOVMod, oldFOVMod, SprintingFOVLerp);	
		}
		else if (CurrentWeapon.CurrentMovement == MovementState.Walking)
		{
			RegenerateStamina(WalkingStaminaRegen);
			currentFOVMod = Mathf.Lerp(RegularFOV, oldFOVMod, SprintingFOVLerp);
		}
		else if (CurrentWeapon.CurrentMovement == MovementState.None)
		{
			RegenerateStamina(IdleStaminaRegen);
			currentFOVMod = Mathf.Lerp(RegularFOV, oldFOVMod, SprintingFOVLerp);
		}
		
		
		
		if (PlayerMover.Jetpack && PlayerMover.CanUseJetpack)
		{		
			if (!_JetpackAudioSource.isPlaying)
			{
				_JetpackAudioSource.Play();
			}
			
			LoseJetpackFuel(JetpackFuelLoss);		
		}
		else
		{
			if (_JetpackAudioSource.isPlaying)
			{
				_JetpackAudioSource.Stop();
			}	
		}
		
		if	(PlayerMover.Jetpack == false || PlayerMover.CanUseJetpack == false)
		{
			GainJetpackFuel(JetpackFuelRegen);	
		}						
		
		if (PlayerMover.onGround)
		{
			if (PlayerMover.IsMoving)
			{
				if (PlayerMover.Sprinting)
				{					
					CurrentWeapon.CurrentMovement = MovementState.Sprinting;	
				}
				else if (PlayerMover.Sprinting == false)
				{
					CurrentWeapon.CurrentMovement = MovementState.Walking;
				}
			}
			else if (PlayerMover.IsMoving == false)
			{
				CurrentWeapon.CurrentMovement = MovementState.None;					
			}	
		}
		else
		{
			CurrentWeapon.CurrentMovement = MovementState.None;						
		}
		
		if (Stamina <= 0)
		{
			PlayerMover.CanSprint = false;	
		}
		else if (Stamina > 0)
		{
			PlayerMover.CanSprint = true;	
		}
		
		if (JetpackFuel <= 0)
		{
			PlayerMover.CanUseJetpack = false;	
		}
		else if (JetpackFuel > 0)
		{
			PlayerMover.CanUseJetpack = true;	
		}
		
		if (Input.GetKeyDown(KeyCode.H))
		{
			IsUpdatingTempHealth = false;
			Hurt(0.05f);
		}
		
		if (Input.GetMouseButtonDown(0))
		{
			if (CurrentWeapon.NeedsToReload && CurrentWeapon.CanReload)
			{
				CurrentWeapon.Reload();
			}
		}	
		
		if (Input.GetMouseButton(0))
		{
			CurrentWeapon.Fire();
		}
		
		if (Input.GetMouseButtonUp(0))
		{
			CurrentWeapon.EndFire();	
		}
		
		if (Input.GetButtonDown("Reload"))
		{
			if (CurrentWeapon.CanReload && CurrentWeapon.CurrentMovement != MovementState.Sprinting)
			{
				CurrentWeapon.Reload();
			}
		}
		
		////if (PlayerMover.IsMoving == false)
		////{
		////	Idle();
		////}
		////else
		////{
		////	StopIdle();	
		////}				
		
		
		oldHealth = Health;
		oldStamina = Stamina;
		oldJetpackFuel = JetpackFuel;
		oldFOVMod = currentFOVMod;
	}

    void DoUpdate_Paused()
    {

    }
	
	public void LoseJetpackFuel(float amount)
	{
		if (JetpackFuel > 0)
		{
			JetpackFuel -= amount;
			
			nextJetpackFuelBarTempUpdate = Time.time + JetpackFuelBarTempUpdateDelay;
		}
	}
	
	public void GainJetpackFuel(float amount)
	{
		if (JetpackFuel < 1)
		{
			JetpackFuel += amount;
		}
	}
	
	public void Die()
	{
		Application.LoadLevel("Title Screen");	
	}
	
	public void MakeFootstep(bool sprinting)
	{
		if (sprinting == true)
		{
			int r = Random.Range(0, SprintingFootstepSounds.Length);
			_AudioSource.PlayOneShot(SprintingFootstepSounds[r]);
			nextSprintingFootstep = Time.time + SprintingFootstepDelay;
		}
		else
		{
			int r = Random.Range(0, RegularFootstepSounds.Length);
			_AudioSource.PlayOneShot(RegularFootstepSounds[r]);
			nextFootstep = Time.time + FootstepDelay;
		}
	}
	
	public void Hurt(float healthLoss)
	{
		if (Health > 0)
		{
			Health -= healthLoss;
			
			int r = Random.Range(0, HurtSounds.Length);
			
			_AudioSource.PlayOneShot(HurtSounds[r]);
			
			nextHealthBarTempUpdate = Time.time + HealthBarTempUpdateDelay;	
		}
	}
	
	public void Tire(float staminaLoss)
	{
		if (Stamina > 0)
		{
			Stamina -= staminaLoss;	
			
			nextStaminaBarTempUpdate = Time.time + StaminaBarTempUpdateDelay;
		}
	}
	
	public void RegenerateStamina(float staminaToAdd)
	{
		if (Stamina < 1)
		{
			Stamina += staminaToAdd;
		}		
	}		
}
