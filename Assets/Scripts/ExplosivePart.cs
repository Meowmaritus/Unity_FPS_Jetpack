using UnityEngine;
using System.Collections;

public class ExplosivePart : MonoBehaviour {
	public GameObject parent;
	Vector3 forcePerFrame, center;
	float fpfStart, expTime, timeLim, dist;
	bool explodingFar, explodingForward, explodingRewind;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (explodingFar) {
			rigidbody.AddForce(forcePerFrame);
			forcePerFrame *= 1.08f;
			if ( forcePerFrame.magnitude > 10000000)
				Destroy(this.gameObject);
		}
		if (explodingForward) {
			rigidbody.AddForce(-forcePerFrame*.1f);
			//forcePerFrame *= 0.9f;
			transform.localScale *= 1.01f;
			
			if (Time.time - expTime > timeLim) {
				expTime = Time.time;
				forcePerFrame *= -1;
				rigidbody.velocity *= -1;
				explodingRewind = true;
				explodingForward = false;
			}
		}
		if (explodingRewind) {
			rigidbody.AddForce(forcePerFrame*0.02f);
			forcePerFrame *= 1.1f;
			transform.localScale *= 0.95f;
			
			if ((transform.position-center).magnitude < dist*1.5) {
				Destroy(gameObject);
			}
			if (Time.time - expTime > timeLim) {
				Destroy(gameObject);
			}
			
		}
		
	}
	
	void OnCollisionEnter(Collision other) {
		if (other.gameObject.tag == "Bullet")
			parent.GetComponent<Explosive>().Explode();
	}
	
	public void ExplodeFar(Vector3 force) {
		explodingFar = true;
		forcePerFrame = force;
	}
	
	public void ExplodeRewind(Vector3 force, Vector3 c, float timeLim) {
		rigidbody.AddForce(force);
		center = c;
		dist = (transform.position - c).magnitude;
		this.timeLim = timeLim;
		expTime = Time.time;
		forcePerFrame = force;
		fpfStart = force.magnitude;
		explodingForward = true;
	}
}
