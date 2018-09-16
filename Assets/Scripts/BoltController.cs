using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BoltController : MonoBehaviour {

    public bool isAlive;
    private GameObject player;
    private GameController gameController;

    void Start()
    {
        isAlive = true;
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        if(gameController.resourceAcquired && gameObject.tag == "Bolt")
        {
            gameObject.layer = LayerMask.NameToLayer("Resource");
        }
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
        if(collision.gameObject.tag == "Player" && isAlive)
        {
            player.GetComponent<PlayerController>().GotHit();
        }

        string[] tags = { "GammaZoid", "GammaRidged", "GammaBulky" };
        if (tags.Contains(collision.gameObject.tag) && isAlive)
        {
            collision.gameObject.GetComponent<GammaController>().GotHit();
        }

        if(collision.gameObject.tag == "GammaSmall")
        {
            gameController.ResourceAcquired();
            collision.gameObject.GetComponent<ResourceGammaController>().GotHit();
        }

        isAlive = false;
        gameObject.layer = LayerMask.NameToLayer("Default");
        GetComponent<Rigidbody>().useGravity = true;
    }
}
