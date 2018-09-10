using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GammaController : MonoBehaviour {

    private GameObject plasmaExplosion;
    private int health;
    private Color flashColor;
    private Color originalColor;
    private Vector3 centerOfMovement;
    private bool spinDirection;
    private float nextShotTime;
    private float nextShotPeriod;
    private GameObject alienBolt;
    private float alienBoltSpeed;
    private GameObject player;

    void Start()
    {
        plasmaExplosion = GameObject.Find("PlasmaExplosionEffect");
        alienBolt = GameObject.Find("AlienBolt");
        player = GameObject.Find("Player");
        alienBoltSpeed = 0.3f;
        health = 1;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        originalColor = GetComponent<Renderer>().material.color;
        centerOfMovement = transform.position;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        spinDirection = (Random.value > 0.5f);

        if (spinDirection)
        {
            rigidbody.velocity = new Vector3(0, 2, 0);
            rigidbody.velocity = Quaternion.Euler(0, 0, Random.value * 360) * rigidbody.velocity;
        }
        else
        {
            rigidbody.velocity = new Vector3(0, -2, 0);
            rigidbody.velocity = Quaternion.Euler(0, 0, Random.value * 360) * rigidbody.velocity;

        }

        nextShotTime = 0.0f;
        nextShotPeriod = 1.0f;
    }

    void FixedUpdate()
    {
        float spinSpeed = 3;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (spinDirection)
        {
            rigidbody.velocity = Quaternion.Euler(new Vector3(0, 0, spinSpeed)) * rigidbody.velocity;
        }
        else
        {
            rigidbody.velocity = Quaternion.Euler(new Vector3(0, 0, -spinSpeed)) * rigidbody.velocity;
        }

        ShootAtPlayer();
    }

    void ShootAtPlayer()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime += nextShotPeriod;
            GameObject newAlienBolt = Instantiate(alienBolt, transform.position, transform.rotation);

            Rigidbody rigidbody = newAlienBolt.GetComponent<Rigidbody>();
            rigidbody.velocity = player.transform.position - transform.position;
            rigidbody.velocity.Normalize();
            rigidbody.velocity *= alienBoltSpeed;

        }
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

        string[] tags = { "GammaZoid", "GammaRidged", "GammaBulky", "AlienBolt", "Bolt", "Boundary" };
        if (tags.Contains(collider.gameObject.tag))
        {
            Physics.IgnoreCollision(collider.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }

    void ResetColor()
    {
        GetComponent<Renderer>().material.SetColor("_Color", originalColor);
    }
}
