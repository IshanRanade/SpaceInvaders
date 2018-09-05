using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaZoidController : MonoBehaviour {

    private GameObject plasmaExplosion;

    void Start()
    {
        plasmaExplosion = GameObject.Find("PlasmaExplosionEffect");
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bolt")
        {
            GameObject newExplosion = Instantiate(plasmaExplosion, gameObject.transform.position, Quaternion.identity);
            float time = newExplosion.GetComponent<ParticleSystem>().main.duration;
            Destroy(newExplosion, time);
            Destroy(gameObject);
        }
    }
}
