using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlienBoltController : MonoBehaviour {

	// Use this for initialization
	void Start () {
        // Delete the bolt when it goes too far
        if (GetComponent<Rigidbody>().position.z < -0.5 * GameObject.Find("Boundary").transform.localScale.z * 8)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }

        string[] tags = { "GammaZoid", "GammaRidged", "GammaBulky", "AlienBolt", "Bolt", "Boundary" };
        if (tags.Contains(collider.gameObject.tag))
        {
            Physics.IgnoreCollision(collider.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
