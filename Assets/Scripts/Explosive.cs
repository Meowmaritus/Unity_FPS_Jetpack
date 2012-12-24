using UnityEngine;
using System.Collections;

public class Explosive : MonoBehaviour {
	
	public float explosiveForce;
	public float smallExpForce;

	// Use this for initialization
	void Start () {
		foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>()) {
			r.Sleep();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public void Explode() {
		foreach (Rigidbody r in GetComponentsInChildren<Rigidbody>()) {
			r.AddExplosionForce(explosiveForce, transform.position, 50);
			//Vector3 dir = (r.transform.position - transform.position).normalized;
			//r.AddForce(dir*explosiveForce);
			//r.GetComponent<ExplosivePart>().Explode(dir*smallExpForce);
		}
	}
}
