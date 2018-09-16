using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GammaController : MonoBehaviour
{
    protected GameObject player;
    protected GameController gameController;

    protected AudioSource plasmaExplosionSound;
    protected AudioSource alienBoltSound;

    protected GameObject plasmaExplosion;
    protected GameObject alienBolt;
    protected GameObject debris;

    public Color flashColor;
    protected Color originalColor;

    public float nextShotTime;
    public float nextShotPeriod;
    public int health;
    public float alienBoltSpeed;
    public float scorePoints;

    protected float createdTime;

    public bool canShoot;
    public float distanceX;
    public float distanceZ;
    private Vector3 target;
    private int tracker;

    void Start()
    {
        alienBolt = Resources.Load<GameObject>("Prefab/AlienBolt");
        debris = Resources.Load<GameObject>("Prefab/Debris");

        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();

        originalColor = GetComponent<Renderer>().material.color;
        createdTime = Time.time;
        nextShotTime = createdTime + 2.0f;

        plasmaExplosionSound = GameObject.Find("PlasmaExplosionSound").GetComponent<AudioSource>();
        alienBoltSound = GameObject.Find("AlienBoltSound").GetComponent<AudioSource>();

        plasmaExplosion = Resources.Load<GameObject>("Prefab/PlasmaExplosionEffect");

        // Make the player not interact with buffer and back wall
        Physics.IgnoreCollision(GameObject.Find("Buffer").GetComponent<Collider>(), player.GetComponent<Collider>());
        Physics.IgnoreCollision(GameObject.Find("BackWall").GetComponent<Collider>(), player.GetComponent<Collider>());

        distanceX = gameController.distanceX;
        distanceZ = gameController.distanceZ;
        
        tracker = 0;
        target = new Vector3(transform.position.x + distanceX, 0, transform.position.z);
    }

    protected void Shoot()
    {
        if(gameController.gameIsOver)
        {
            return;
        }

        if (Time.time > nextShotTime)
        {
            nextShotTime += nextShotPeriod;

            GameObject newAlienBolt = Instantiate(alienBolt, transform.position, Quaternion.identity);

            newAlienBolt.transform.rotation *= Quaternion.Euler(90, 0, 0);

            Rigidbody alienBoltRigidbody = newAlienBolt.GetComponent<Rigidbody>();
            alienBoltRigidbody.velocity = new Vector3(0, 0, -1) * alienBoltSpeed;

            newAlienBolt.transform.position += new Vector3(0, 0, -3.0f);

            alienBoltSound.Play();
        }
    }

    void FixedUpdate()
    {
        if (canShoot)
        {
            Shoot();
        }

        MoveSelf();

        if(transform.position.z <= 0)
        {
            gameController.gameIsOver = true;
        }
    }

    private void MoveSelf()
    {
        float speed = gameController.alienSpeed * Time.deltaTime;
        float step = speed * Time.deltaTime;

        if (target == transform.position)
        {
            if (tracker == 0)
            {
                target = new Vector3(transform.position.x, 0, transform.position.z - distanceZ);
            }
            else if (tracker == 1)
            {
                target = new Vector3(transform.position.x - distanceX, 0, transform.position.z);
            }
            else if (tracker == 2)
            {
                target = new Vector3(transform.position.x, 0, transform.position.z - distanceZ);
            }
            else
            {
                target = new Vector3(transform.position.x + distanceX, 0, transform.position.z);
            }

            tracker = (tracker + 1) % 4;
        }

        transform.position = Vector3.MoveTowards(transform.position, target, step);
    }


    public void GotHit()
    {
        health = Mathf.Max(health-1, 0);

        if (health <= 0)
        {
            GameObject newDebris = Instantiate(debris, transform.position, Quaternion.identity);
            GameObject newExplosion = Instantiate(plasmaExplosion, gameObject.transform.position, Quaternion.identity);
            float time = newExplosion.GetComponent<ParticleSystem>().main.duration;

            gameController.AlienDied(gameObject);

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

    void ResetColor()
    {
        foreach (Material mat in GetComponent<Renderer>().materials)
        {
            GetComponent<Renderer>().material.SetColor("_Color", originalColor);
        }
    }
}
