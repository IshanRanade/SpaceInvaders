using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class GammaController : MonoBehaviour {

    protected GameObject player;
    protected GameController gameController;

    protected AudioSource plasmaExplosionSound;
    protected AudioSource alienBoltSound;

    protected GameObject plasmaExplosion;
    protected GameObject alienBolt;
    
    protected Color flashColor;
    protected Color originalColor;
    
    protected float nextShotTime;
    protected float nextShotPeriod;
    protected int health;
    protected float alienBoltSpeed;
    protected float scorePoints;

    protected float createdTime;

    void Start()
    {
        alienBolt = Resources.Load<GameObject>("Prefab/AlienBolt");
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        originalColor = GetComponent<Renderer>().material.color;
        createdTime = Time.time;
        nextShotTime = createdTime;

        plasmaExplosionSound = GameObject.Find("PlasmaExplosionSound").GetComponent<AudioSource>();
        alienBoltSound = GameObject.Find("AlienBoltSound").GetComponent<AudioSource>();

        plasmaExplosion = Resources.Load<GameObject>("Prefab/PlasmaExplosionEffect");
            
        SetSpecificValues();
    }

    public abstract void SetSpecificValues();

    protected void Shoot()
    {
        if(Time.time > nextShotTime)
        {
            nextShotTime += nextShotPeriod;

            GameObject newAlienBolt = Instantiate(alienBolt, transform.position, Quaternion.identity);

            newAlienBolt.transform.rotation *= Quaternion.Euler(90, 0, 0);

            Rigidbody alienBoltRigidbody = newAlienBolt.GetComponent<Rigidbody>();
            alienBoltRigidbody.velocity = new Vector3(0, 0, -1) * alienBoltSpeed;
            
            alienBoltSound.Play();
        }
    }

    void Update()
    {
        Shoot();
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

                plasmaExplosionSound.GetComponent<AudioSource>().Play();
            }
            else
            {
                foreach (Material mat in GetComponent<Renderer>().materials)
                {
                    GetComponent<Renderer>().material.SetColor("_Color", flashColor);
                }
                Invoke("ResetColor", 0.3f);
            }
        }
        else
        {
            string[] tags = { "GammaZoid", "GammaRidged", "GammaBulky", "AlienBolt", "Bolt", "Boundary" };
            if (tags.Contains(collider.gameObject.tag))
            {
                Physics.IgnoreCollision(collider.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
            }
        }
    }

    void ResetColor()
    {
        foreach (Material mat in GetComponent<Renderer>().materials)
        {
            GetComponent<Renderer>().material.SetColor("_Color", originalColor);
        }
    }
}
