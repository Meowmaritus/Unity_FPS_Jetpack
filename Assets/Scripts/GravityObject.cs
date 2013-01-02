using UnityEngine;
using System.Collections;

public class GravityObject : MonoBehaviour {
	public static GravityObject mainObject;
	public static bool globalGravityOn = false;
	public bool isSphere;
	public bool isPlane;
	public Vector3 gravity;
	public float gravityForce = 3f;

	// Use this for initialization
	void Start () {
		if (isSphere && isPlane)
			Debug.LogError("Gravity Object cannot be both a plane and sphere. Please check only either isSphere, or isPlane", this.gameObject);
		
		if (isPlane)  {
			gravity = transform.up;
		}
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log(globalGravityOn);
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Bullet" || other.gameObject.tag == "Player") {
			globalGravityOn = true;
			mainObject = this;
		}
		
	}
	
	public Vector3 GetGravityFor(Vector3 position) {
		if (isPlane) {
			return gravity;
		}
		else if (isSphere) {
			return (position - transform.position).normalized;
		}
		
		return Vector3.zero;
	}
	
	//public Vector3 
}
