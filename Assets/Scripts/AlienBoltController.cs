using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlienBoltController : MonoBehaviour {

    bool isActive;
    GameObject player;

    void Start()
    {
        isActive = true;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        // Delete the bolt when it goes too far
        if (GetComponent<Rigidbody>().position.z < -30)
        { 
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // If this is the first collision with a player decrease its health
        if(collision.gameObject.tag == "Player" && isActive)
        {
            player.GetComponent<PlayerController>().GotHit();
        }

        isActive = false;
    }
}
