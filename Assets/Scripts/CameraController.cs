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
        transform.position = new Vector3(player.transform.position.x, originalPosition.y, originalPosition.z);
        transform.rotation = originalRotation;
        //if(gameController.gameIsOver)
        //{
        //    return;
        //}

        //gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, player.transform.position - new Vector3(0, -2, 5), ref velocity, 0.2f);
    }
}
