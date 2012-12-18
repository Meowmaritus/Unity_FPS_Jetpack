using UnityEngine;
using System.Collections;

public enum ReloadState
{
	None,
	ReloadStart,
	ReloadEnd,
	ReloadFull	
}

public enum ShootState
{
	None,
	LoopStart,
	Loop,
	LoopEnd,
	Once
}

public enum MovementState
{
	None,
	Walking,
	Sprinting
}

[AddComponentMenu("_Main/Weapons/Weapon")]
public class Weapon : MonoBehaviour 
{
	public GameObject MuzzleFlashObject;
	
	public PlayerHandler Player;
	public GameObject WeaponObject;
	
	public GameObject GunLight;
	
	public int AmmoTotal = 0;	
	public int AmmoInClip = 0;
	public int AmmoInBarrel = 0;
	public int MaxAmmoPerBarrel = 0;
	public int MaxAmmoPerClip = 0;
	public int MaxAmmoTotal = 0;
	
	public float FireDelay = 1.0f;
	public float MuzzleFlashDelay = 1.0f;
	
	public AudioClip[] GunshotSounds;
	public AudioClip[] ShortReloadSounds;
	public AudioClip[] LongReloadSounds;
	public AudioClip[] LoadBarrelSounds;
	
	public Texture[] MuzzleFlashes;
	public Material MuzzleFlashMaterial;
	
	public GameObject[] BulletObjects;
	
	public Transform Barrel;	
	
	public ReloadState CurrentReload = ReloadState.None;
	public MovementState CurrentMovement = MovementState.None;
	public MovementState oldMovement = MovementState.None;
	public ShootState CurrentShoot = ShootState.None;
	
	public bool CanReload = false;
	
	public bool NeedsToReload = false;
	
	private float NextFire = 0.0f;
	private float NextMuzzleFlash = 0.0f;
		
	// Use this for initialization
	void Start () 
	{
		
	}
	
	void FixedUpdate()
	{
		
	}
	
	// Update is called once per frame
	void Update () 
	{		
		GunLight.active = MuzzleFlashObject.active;
				
		if (Time.time > NextMuzzleFlash && MuzzleFlashObject.active == true)
		{
			MuzzleFlashObject.active = false;			
		}
		
		
		MuzzleFlashObject.transform.position = Barrel.transform.position;
		MuzzleFlashObject.transform.rotation = Barrel.transform.rotation;
		MuzzleFlashObject.transform.Rotate(new Vector3(180, 0, 0));
		
		GameGUI.DebugValue["AmmoTotal"] = AmmoTotal;
		GameGUI.DebugValue["AmmoInClip"] = AmmoInClip;
		GameGUI.DebugValue["AmmoAmmoInBarrel"] = AmmoInBarrel;
		GameGUI.DebugValue["MaxAmmoPerBarrel"] = MaxAmmoPerBarrel;
		GameGUI.DebugValue["MaxAmmoPerClip"] = MaxAmmoPerClip;
		GameGUI.DebugValue["MaxAmmoTotal"] = MaxAmmoTotal;
		GameGUI.DebugValue["ReloadState"] = CurrentReload;
		GameGUI.DebugValue["MovementState"] = CurrentMovement;
		GameGUI.DebugValue["ShootState"] = CurrentShoot;
		GameGUI.DebugValue["NeedsToReload"] = NeedsToReload;
		GameGUI.DebugValue["CanReload"] = CanReload;
		
		if (CurrentReload == ReloadState.None)
		{						
			if (AmmoInClip <= 0)
			{
				if (AmmoInBarrel <= 0)
				{
					NeedsToReload = true;
				}
			}
			else
			{
				if (AmmoInBarrel <= 0)
				{
					LoadBarrelFromClip();	
				}
			}
		}
		
		if (CurrentReload == ReloadState.None)
		{
			if (AmmoInClip >= MaxAmmoPerClip)
			{
				CanReload = false;
			}
			else
			{
				CanReload = true;	
			}
		}
		
		AmmoInBarrel = Mathf.Clamp(AmmoInBarrel, 0, MaxAmmoPerBarrel);
		AmmoInClip = Mathf.Clamp(AmmoInClip, 0, MaxAmmoPerClip);
		AmmoTotal = Mathf.Clamp(AmmoTotal, 0, MaxAmmoTotal);				
		
		
		//if (WeaponObject.animation.IsPlaying("reloadEmptyStart") == false && CurrentReload == ReloadState.ReloadStart)
		//{
		//	  WeaponObject.animation.Stop("reloadEmptyStart");	
		//}
		
		if (WeaponObject.animation.IsPlaying("reloadEmptyEnd") == false && CurrentReload == ReloadState.ReloadEnd)
		{
			Reload_End();
		}
		
		if (WeaponObject.animation.IsPlaying("reload") == false && CurrentReload == ReloadState.ReloadFull)
		{
			Reload_End();	
		}
		
		if (WeaponObject.animation.IsPlaying("reloadEmptyStart") == false && CurrentReload == ReloadState.ReloadStart)
		{
			Reload_PlayEnd();	
		}
		
		if (WeaponObject.animation.IsPlaying("shootOnce") == false && CurrentShoot == ShootState.Once)
		{
			StopShooting(true);
		}
		
		if (AmmoInBarrel <= 0 && CurrentShoot == ShootState.Loop)
		{
			StopShooting(true);
		}
		
		//if (WeaponObject.animation.IsPlaying("shootLoop") == false && CurrentShoot == ShootState.Loop)
		//{
		//	StopShooting(false);
		//}
		
		if (WeaponObject.animation.IsPlaying("shootEnd") == false && CurrentShoot == ShootState.LoopEnd)
		{
			StopShooting(true);
		}
		
		if (WeaponObject.animation.IsPlaying("shootStart") == false && CurrentShoot == ShootState.LoopStart)
		{
			EnterFireLoop();
		}
		
		if (CurrentShoot == ShootState.None && CurrentReload == ReloadState.None)
		{
			if (oldMovement != CurrentMovement)
			{
				if (CurrentMovement == MovementState.None)
				{				
					WeaponObject.animation.Play("idle");
					WeaponObject.animation.PlayQueued("idle");
				}
				else if (CurrentMovement == MovementState.Walking)
				{
					WeaponObject.animation.Play("walkStart");
					WeaponObject.animation.PlayQueued("walkLoop");
				}
				else if (CurrentMovement == MovementState.Sprinting)
				{
					WeaponObject.animation.Play("sprintStart");
					WeaponObject.animation.PlayQueued("sprintLoop");
				}
			}
			else
			{
				if (oldMovement == MovementState.None && CurrentMovement == MovementState.Walking)
				{				
					WeaponObject.animation.Play("walkStart");
					WeaponObject.animation.PlayQueued("walkLoop");
				}
				else if (oldMovement == MovementState.Walking && CurrentMovement == MovementState.None)
				{				
					WeaponObject.animation.Play("walkEnd");
					WeaponObject.animation.PlayQueued("idle");
				}
				else if (oldMovement == MovementState.None && CurrentMovement == MovementState.Sprinting)
				{
					WeaponObject.animation.Play("sprintStart");
					WeaponObject.animation.PlayQueued("sprintLoop");
				}
				else if (oldMovement == MovementState.Sprinting && CurrentMovement == MovementState.None)
				{
					WeaponObject.animation.Play("sprintEnd");
					WeaponObject.animation.PlayQueued("idle");
				}
				else if (oldMovement == MovementState.Walking && CurrentMovement == MovementState.Sprinting)
				{
					WeaponObject.animation.Play("walkEnd");
					WeaponObject.animation.Play("sprintStart");
					WeaponObject.animation.PlayQueued("sprintLoop");
				}
				else if (oldMovement == MovementState.Sprinting && CurrentMovement == MovementState.Walking)
				{
					WeaponObject.animation.Play("sprintEnd");
					WeaponObject.animation.Play("walkStart");
					WeaponObject.animation.PlayQueued("walkLoop");
				}
			}						
		}			
		
		oldMovement = CurrentMovement;		
		
	}
	
	private void FireRound()
	{		
		NextMuzzleFlash = Time.time + MuzzleFlashDelay;
		
		int rm = Random.Range (0, MuzzleFlashes.Length);
		MuzzleFlashMaterial.mainTexture = MuzzleFlashes[rm];
		MuzzleFlashObject.renderer.material = MuzzleFlashMaterial;
							
		int ra = Random.Range(0, GunshotSounds.Length);
		
		Player._AudioSource.PlayOneShot(GunshotSounds[ra]);				
		
		int rb = Random.Range(0, BulletObjects.Length);
		
		Instantiate(BulletObjects[rb], Barrel.position, (Barrel.rotation * BulletObjects[rb].transform.rotation));
		
		AmmoInBarrel--;					
	}
	
	private void EnterFireLoop()
	{
		CurrentShoot = ShootState.Loop;
	}
	
	public void Fire()
	{		
		if (Time.time > NextFire)
		{
			if (CurrentReload == ReloadState.None && NeedsToReload == false)
			{			
				MuzzleFlashObject.active = true;
				if (CurrentShoot == ShootState.None)
				{
					CurrentShoot = ShootState.LoopStart;				
					WeaponObject.animation.Play("shootStart");													
				}
				else if (CurrentShoot == ShootState.Loop)
				{		
					WeaponObject.animation.Play("shootLoop");															
				}
				FireRound();	
				NextFire = Time.time + FireDelay;
			}	
		}
	}
	
	public void EndFire()
	{
		if (CurrentShoot != ShootState.None)
		{
			if (CurrentShoot == ShootState.Once)
			{
				StopShooting(true);
			}
			else
			{
				StopShooting(false);
			}
		}
	}
	
	private void StopShooting(bool isOnce)
	{				
		if (CurrentReload == ReloadState.None)
		{
			if (isOnce == false)
			{
				WeaponObject.animation.Play("shootEnd");
				
				CurrentShoot = ShootState.LoopEnd;
			}
			else
			{		
				StartIdling();
				
				CurrentShoot = ShootState.None;
			}
		}
	}	
	
	private void LoadBarrelFromClip()
	{
		if (LoadBarrelSounds.Length >= 1)
		{
			int r = Random.Range(0, LoadBarrelSounds.Length);			
			Player._AudioSource.PlayOneShot(LoadBarrelSounds[r]);
		}
		
		if (AmmoInClip >= MaxAmmoPerBarrel)
		{
			AmmoInBarrel = MaxAmmoPerBarrel;
			AmmoInClip -= MaxAmmoPerBarrel;
		}
		else
		{
			if (AmmoInClip > 0)
			{
				AmmoInBarrel = AmmoInClip;
				AmmoInClip = 0;
			}
			else
			{
				AmmoInBarrel = 0;
				AmmoInClip = 0;
			}
		}
	}
	
	public void Reload()
	{				
		if (CurrentReload == ReloadState.None && CanReload == true)
		{			
			CanReload = false;
			
			if (AmmoInBarrel > 0)
			{
				int r = Random.Range(0, ShortReloadSounds.Length);
				CurrentReload = ReloadState.ReloadFull;
				Player._AudioSource.PlayOneShot(ShortReloadSounds[r]);
				WeaponObject.animation.Play("reload");			
				
			}
			else
			{
				int r = Random.Range(0, LongReloadSounds.Length);
				CurrentReload = ReloadState.ReloadStart;
				Player._AudioSource.PlayOneShot(LongReloadSounds[r]);
				WeaponObject.animation.Play("reloadEmptyStart");
				WeaponObject.animation.PlayQueued("reloadEmptyEnd");
			}
			
		}
		
	}
	
	public void StartIdling()
	{
		//WeaponObject.animation.Stop();
		
		if (CurrentMovement == MovementState.None)
		{
			WeaponObject.animation.Play("idle");
		}
		else if (CurrentMovement == MovementState.Walking)
		{
			WeaponObject.animation.Play("walkLoop");
		}
		else if (CurrentMovement == MovementState.Sprinting)
		{
			WeaponObject.animation.Play("sprintLoop");
		}
	}
	
	private void Reload_PlayEnd()
	{
		WeaponObject.animation.Stop();
		WeaponObject.animation.Play("reloadEmptyEnd");
		CurrentReload = ReloadState.ReloadEnd;
		CanReload = true;
		NeedsToReload = false;
	}
	
	private void Reload_End()
	{
		CurrentReload = ReloadState.None;		
		
		StartIdling();
		
		if (AmmoTotal >= (MaxAmmoPerClip - AmmoInClip))
		{
			AmmoTotal -= (MaxAmmoPerClip - AmmoInClip);
			AmmoInClip = MaxAmmoPerClip;			
		}
		else
		{
			AmmoInClip = (int)AmmoTotal;
			AmmoTotal = 0;
		}
					
		CanReload = true;
		NeedsToReload = false;
		
	}
}
