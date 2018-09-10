using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class GammaController : MonoBehaviour {

    protected GameObject plasmaExplosion;
    protected GameObject alienBolt;
    protected GameObject player;
    protected GameController gameController;

    protected Color flashColor;
    protected Color originalColor;
    protected Vector3 centerOfMovement;

    protected bool spinDirection;
    protected float nextShotTime;
    protected float nextShotPeriod;
    protected int health;
    protected float alienBoltSpeed;
    protected float scorePoints;

    protected float createdTime;

    void Start()
    {
        alienBolt = GameObject.Find("AlienBolt");
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        originalColor = GetComponent<Renderer>().material.color;
        centerOfMovement = transform.position;
        createdTime = Time.time;
        nextShotTime = createdTime;

        SetSpecificValues();
    }

    public abstract void SetSpecificValues();

    protected void ShootAtPlayer()
    {
        if (Time.time > nextShotTime)
        {
            nextShotTime += nextShotPeriod;

            Vector3 target = player.transform.position;
            target.x += Random.value * 4 - 2;
            target.y += Random.value * 4 - 2;

            GameObject newAlienBolt = Instantiate(alienBolt, transform.position, transform.rotation);

            Rigidbody rigidbody = newAlienBolt.GetComponent<Rigidbody>();

            Vector3 vel = target - transform.position;
            vel.Normalize();

            Vector3 axis = new Vector3(0, 1, 0);
            rigidbody.rotation = Quaternion.FromToRotation(axis, vel);

            rigidbody.velocity = vel * alienBoltSpeed;
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

                gameController.UpdateScore(scorePoints);
                gameController.currentNumAliens--;
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
