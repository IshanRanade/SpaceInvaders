using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoundaryController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Reverse the normals of the boundary cube so it can be seen from inside
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
    }
}
