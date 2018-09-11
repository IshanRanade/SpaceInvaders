using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoundaryController : MonoBehaviour {

    private GameObject player;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        Mesh mesh = GetComponent<MeshFilter>().mesh;
        mesh.triangles = mesh.triangles.Reverse().ToArray();
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        player = GameObject.Find("Player");
    }

    void Update()
    {
        if (gameController.gameIsOver)
        {
            return;
        }

        float radius = transform.localScale.x / 2.0f;
        float distance = Mathf.Sqrt(Mathf.Pow(player.transform.position.x, 2.0f) + Mathf.Pow(player.transform.position.y, 2.0f)) - radius;
        distance = Mathf.Min(0, distance);
        distance = Mathf.Abs(distance);

        Color newColor = GetComponent<Renderer>().material.color;

        float startFadingDistance = 5;

        if (distance < startFadingDistance)
        {
            GetComponent<Renderer>().enabled = true;
            newColor.a = 0.5f * (1.0f - (distance / startFadingDistance));
        }
        else
        {
            GetComponent<Renderer>().enabled = false;
            newColor.a = 0.0f;
        }

        GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }
}
