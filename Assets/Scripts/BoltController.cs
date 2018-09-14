﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoltController : MonoBehaviour {

    public bool isAlive;
    private GameObject player;

    void Start()
    {
        isAlive = true;
        player = GameObject.Find("Player");
    }
	
	// Update is called once per frame
	void Update () {
        // Delete the bolt when it goes too far
        if (GetComponent<Rigidbody>().position.z < -20 || GetComponent<Rigidbody>().position.z > 200)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(tag == "Bolt")
        {
            print(collision.gameObject.tag);
        }
        if(collision.gameObject.tag == "Player" && isAlive)
        {
            player.GetComponent<PlayerController>().GotHit();
        }

        isAlive = false;
        GetComponent<Rigidbody>().useGravity = true;
    }
}
