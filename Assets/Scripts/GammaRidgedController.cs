 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaRidgedController : MonoBehaviour {

    private GameObject plasmaExplosion;
    private int health;
    private Color flashColor;
    private Color originalColor;

    // Use this for initialization
    void Start ()
    {
        plasmaExplosion = GameObject.Find("PlasmaExplosionEffect");
        health = 3;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        originalColor = GetComponent<Renderer>().material.color;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bolt")
        {
            health--;

            if (health == 0)
            {
                GameObject newExplosion = Instantiate(plasmaExplosion, gameObject.transform.position, Quaternion.identity);
                float time = newExplosion.GetComponent<ParticleSystem>().main.duration;
                Destroy(newExplosion, time);
                Destroy(gameObject);
            }
            else
            {
                GetComponent<Renderer>().material.SetColor("_Color", flashColor);
                Invoke("ResetColor", 0.3f);
            }
        }
    }

    void ResetColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color", originalColor);
    }
}
