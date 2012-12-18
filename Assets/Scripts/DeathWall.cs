using UnityEngine;
using System.Collections;

public class DeathWall : MonoBehaviour {
	
	public PlayerHandler Player;
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject == Player.PlayerMover.gameObject)
		{
			Player.Hurt(0.075f);
			Player.rigidbody.AddForce(-(Player.transform.eulerAngles) * 20);
		}
	}
	
	void OnCollisionStay(Collision other)
	{
		if (other.gameObject == Player.PlayerMover.playerCam)
		{
			Player.Hurt(0.075f);
			Player.rigidbody.AddForce(-(Player.transform.eulerAngles) * 20);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
