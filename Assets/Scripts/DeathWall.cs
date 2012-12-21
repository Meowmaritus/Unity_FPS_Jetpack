using UnityEngine;
using System.Collections;

public class DeathWall : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	
	}
	
	void OnCollisionEnter(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerHandler player = other.gameObject.GetComponent<PlayerHandler>();
			player.Hurt(0.075f);
			player.rigidbody.AddForce(-(player.transform.eulerAngles) * 20);
		}
	}
	
	void OnCollisionStay(Collision other)
	{
		if (other.gameObject.tag == "Player")
		{
			PlayerHandler player = other.gameObject.GetComponent<PlayerHandler>();
			player.Hurt(0.075f);
			player.rigidbody.AddForce(-(player.transform.eulerAngles) * 20);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
