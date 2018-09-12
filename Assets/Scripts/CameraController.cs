using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private GameController gameController;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(player.transform.position.x, 16.5f, -18.3f);
        transform.rotation = Quaternion.Euler(19.066f, 0.0f, 0.0f);
        //if(gameController.gameIsOver)
        //{
        //    return;
        //}

        //gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, player.transform.position - new Vector3(0, -2, 5), ref velocity, 0.2f);
    }
}
