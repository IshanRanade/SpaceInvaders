using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Vector3 velocity;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(gameController.gameIsOver)
        {
            return;
        }

        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, player.transform.position - new Vector3(0, -2, 5), ref velocity, 0.2f);
    }
}
