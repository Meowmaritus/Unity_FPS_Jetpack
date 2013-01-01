using UnityEngine;
using System.Collections;
//using System.Collections.Generic.List;

[AddComponentMenu("_Main/Weapons/Bullet Handler")]
public class BulletHandler : MonoBehaviour {
	public float speed;
	public float bulletLife = 10;
	private float spawnTime;
	
	
	void Start () {
		rigidbody.AddRelativeForce(Vector3.forward*speed*100);
		spawnTime = Time.time;
		transform.Rotate(90,0,0);
		Physics.IgnoreCollision(GameObject.FindGameObjectWithTag("Player").collider, collider);
	}
	
	void Update () {
		if (Time.time - spawnTime > bulletLife)
		{
			Destroy(this.gameObject);			
			
		}
	}
	
	void OnCollisionEnter(Collision other) {
		HandleCollision(other);
	}
	
	
	void OnCollisionStay(Collision other) {
		HandleCollision(other);
	}
	
	void HandleCollision(Collision other) {
		//Debug.Log(other.gameObject.name);
		if (other.gameObject.tag == "Wall") {
			GameGUI.wallCount++;
			//addVertex(other.gameObject);
		}
        else if (other.gameObject.tag == "Target")
        {
            GameGUI.targetCount++;
        }

		if (other.gameObject.tag != "Bullet" && other.gameObject.tag != "WEAPONS") {						
			Destroy(this.gameObject);
		}
	}
	
	void addVertex(GameObject go) {
		Mesh mesh = go.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = mesh.vertices;
		//GameGUI.DebugValue["vertices"] = vertices;
		//Debug.Log(vertices);
	}

	/*
	public void AddVertexScreenPos(Vector2 screenPos)
	{
		RaycastHit hit;
		if (HitScreenPos(Input.mousePosition, out hit))
		{
		if (hit.collider.GetType() == typeof(MeshCollider))
		{
			MeshCollider meshCollider = (MeshCollider)hit.collider;
			Mesh mesh = meshCollider.GetComponent<MeshFilter>().mesh;
			List<Vector3> vertices = new List<Vector3>(mesh.vertices);
			List<int> triangles = new List<int>(mesh.triangles);
			
			vertices.Add(meshCollider.transform.InverseTransfo rmPoint(hit.point));
			
			int[] triangleHit = GetTriangleIndices(ref hit);
			
			triangles.Clear();
			
			triangles.AddRange(RemoveTriangle(hit.triangleInde x, mesh.triangles));
			
			for (int i = 0; i < triangleHit.Length; i++)
			{
				triangles.Add(triangleHit[i % triangleHit.Length]);
				triangles.Add(triangleHit[(i + 1) % triangleHit.Length]);
				triangles.Add(vertices.Count - 1);
			
			}
			
			mesh.vertices = vertices.ToArray();
			mesh.triangles = triangles.ToArray();
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			
			//The following line should update the meshCollider, but it fails.
			//meshCollider.sharedMesh = mesh;
			
			//Hack. This fixes the problem
			GameObject go = hit.transform.gameObject;
			GameObject.DestroyImmediate(meshCollider);
			
			go.AddComponent<MeshCollider>();
			//Hack end.
			
			}
		}
		
	}//*/
}
