using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityFieldController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        Renderer renderer = GetComponent<Renderer>();
        Color oldColor = renderer.material.GetColor(Shader.PropertyToID("_TintColor"));

        Vector3[] vertices = mesh.vertices;
        float maxZ = Mathf.NegativeInfinity;
        for(int i = 0; i < vertices.Length; i++)
        {
            maxZ = Mathf.Max(vertices[i].z, maxZ);
        }

        Color[] colors = new Color[vertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Color newColor = oldColor;

            if(vertices[i].z == maxZ)
            {
                newColor.a = 0.3f;
            } else
            {
                newColor.a = Mathf.Abs(Mathf.Sin(Time.time * 5.0f)) * 0.5f + 0.3f;
            }

            colors[i] = newColor;
        }

        // assign the array of colors to the Mesh.
        mesh.colors = colors;
        //print(mesh.subMeshCount);

        //Renderer renderer = GetComponent<Renderer>();
        //Color oldColor = renderer.material.GetColor(Shader.PropertyToID("_TintColor"));
        //Color newColor = oldColor;
        //newColor.a = Mathf.Sin(Time.time * 10.0f) * 0.03f + 0.1f;
        //GetComponent<Renderer>().material.SetColor("_TintColor", newColor);

        //Mesh mesh = GetComponent<MeshFilter>().mesh;
        //mesh.
        //Vector3[] vertices = mesh.vertices;
        //string result = "";
        //foreach(Vector3 v in vertices)
        //{
        //    result += v.ToString() + " "; 
        //}
        //print(vertices.Length);
    }
}
