﻿using System.Collections;
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
        float distanceSigned = Mathf.Sqrt(Mathf.Pow(player.transform.position.x, 2.0f) + Mathf.Pow(player.transform.position.y, 2.0f)) - radius;
        float distance = Mathf.Abs(distanceSigned);


        Color newColor = GetComponent<Renderer>().material.color;

        float startFadingDistance = 8;

        if (distance < startFadingDistance || distanceSigned > 0)
        {
            if (distanceSigned > 0)
            {
                newColor.a = 0.25f;
            }
            else
            {
                newColor.a = 0.25f - distance / startFadingDistance;
            }
        }
        else
        {
            newColor.a = 0.0f;
        }

        GetComponent<Renderer>().material.SetColor("_Color", newColor);
    }
}
