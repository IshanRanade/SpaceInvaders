using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    private GameObject bolt;
    private AudioSource playerBoltSound;
    private AudioSource playerExplosionSound;
    private GameObject bigExplosionEffect;
    private GameController gameController;

    public float speed;
    public float fireRate;
    public float boltSpeed;
    private float nextFire;
    public float health;
    public float maxHealth;

    private Color originalColor;
    private Color flashColor;

    private bool isDead;

    private void Start()
    {
        bolt = Resources.Load<GameObject>("Prefab/Bolt");
        playerBoltSound = GameObject.Find("PlayerBoltSound").GetComponent<AudioSource>();
        playerExplosionSound = GameObject.Find("PlayerExplosionSound").GetComponent<AudioSource>();
        bigExplosionEffect = Resources.Load<GameObject>("Prefab/BigExplosionEffect");

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        flashColor = new Color(10.0f, 5.0f, 0.0f);
        flashColor *= 0.75f;
        originalColor = gameObject.GetComponent<Renderer>().material.color;

        nextFire = Time.time;
        maxHealth = 5.0f;
        health = maxHealth;
        isDead = false;
    }

    public void Reset()
    {
        health = maxHealth;
        nextFire = Time.time;
        gameObject.GetComponent<Renderer>().enabled = true;
        isDead = false; 
    }

    void Update()
    {
        if(isDead)
        {
            return;
        }

        if(health <= 0)
        {
            GameObject newExplosion = Instantiate(bigExplosionEffect, gameObject.transform.position, Quaternion.identity);
            float time = newExplosion.GetComponent<ParticleSystem>().main.duration;
            Destroy(newExplosion, time);

            playerExplosionSound.GetComponent<AudioSource>().Play();

            gameObject.GetComponent<Renderer>().enabled = false;
            isDead = true;
            return;
        }

        KeyCode xUpKey = KeyCode.RightArrow;
        KeyCode xDownKey = KeyCode.LeftArrow;
        KeyCode shootKey = KeyCode.Space;

        float xMovement = 0.0f;

        if (Input.GetKey(xUpKey))
        {
            xMovement += 1.0f;
        }
        if (Input.GetKey(xDownKey))
        {
            xMovement -= 1.0f;
        }

        if (Input.GetKey(shootKey) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject newShot = Instantiate(bolt, transform.position, Quaternion.identity);

            newShot.transform.rotation *= Quaternion.Euler(90, 0, 0);
            newShot.transform.position += new Vector3(0, 0, 1.5f);

            Rigidbody boltRigidbody = newShot.GetComponent<Rigidbody>();
            Vector3 vel = new Vector3(0, 0, 1);

            boltRigidbody.velocity = boltSpeed * vel;

            playerBoltSound.GetComponent<AudioSource>().Play();
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(speed * xMovement, 0, 0);

        // Clamp the player position to be inside the boundary

        GameObject boundary = GameObject.Find("Boundary");

        float margin = 1;
        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, -0.5f * boundary.transform.localScale.x + margin, 0.5f * boundary.transform.localScale.x - margin),
            0.0f,
            0.0f
        );

        // Make the ship bank in the right direction
        float maxRotationSpeed = 70;
        float maxRotation = 35;
        float rotationSpeed = 50;

        if (rigidbody.velocity.x > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -maxRotation), rotationSpeed * Time.deltaTime);

        }
        else if (rigidbody.velocity.x < 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, maxRotation), rotationSpeed * Time.deltaTime);
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), 50 * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bolt")
        {
            Physics.IgnoreCollision(collider.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collider.gameObject.tag == "AlienBolt")
        {
            health = Mathf.Max(0, health - 1);

            gameObject.GetComponent<Renderer>().material.SetColor("_Color", flashColor);
            Invoke("ResetColor", 0.5f);

        }
    }

    void ResetColor()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", originalColor);
    }
}
