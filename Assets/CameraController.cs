using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private Vector3 velocity;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        velocity = Vector3.zero;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        gameObject.transform.position = Vector3.SmoothDamp(gameObject.transform.position, player.transform.position - new Vector3(0,0,5), ref velocity, 0.3f);
	}
}
