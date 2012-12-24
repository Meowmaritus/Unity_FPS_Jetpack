using UnityEngine;
using System.Collections;

public class ExplosivePart : MonoBehaviour {
	public GameObject parent;
	Vector3 forcePerFrame;
	bool exploding;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (exploding) {
			rigidbody.AddForce(forcePerFrame);
			forcePerFrame *= 1.08f;
			if ( forcePerFrame.magnitude > 10000000)
				Destroy(this.gameObject);
		}
		
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Bullet")
			parent.GetComponent<Explosive>().Explode();
	}
	
	public void Explode(Vector3 force) {
		exploding = true;
		forcePerFrame = force;
	}
}
