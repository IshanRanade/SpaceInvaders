using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private GameController gameController;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if(gameController.gameIsOver)
        {
            return;
        }
        transform.position = new Vector3(player.transform.position.x, originalPosition.y, originalPosition.z);
        transform.rotation = originalRotation;
    }
}
