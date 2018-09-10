using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AlienBoltController : MonoBehaviour {

    // Update is called once per frame
    void Update()
    {
        // Delete the bolt when it goes too far
        if (GetComponent<Rigidbody>().position.z < 0 && GetComponent<Rigidbody>().position.z > -5)
        {
            Destroy(gameObject);
        }

        if(GetComponent<Rigidbody>().position.z < -40)
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
