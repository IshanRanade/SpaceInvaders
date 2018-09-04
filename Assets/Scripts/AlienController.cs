using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienController : MonoBehaviour {

    public GameObject plasmaExplosion;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bolt")
        {
            Destroy(gameObject);

            Instantiate(plasmaExplosion, transform.position, Quaternion.identity);
        }
    }
}
