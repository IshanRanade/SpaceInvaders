using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    public float speed;

    public float fireRate;
    public float boltSpeed;

    private GameObject bolt;
    private GameObject camera;
    private GameObject reticule;
    private GameObject playerBoltSound;
    private GameObject playerExplosionSound;

    private GameObject bigExplosionEffect;

    private float nextFire;
    public float health;
    public float maxHealth;

    private Color originalColor;
    private Color flashColor;

    bool playedExplosion;

    private void Start()
    {
        bolt = GameObject.Find("Bolt");
        camera = GameObject.Find("Camera");
        reticule = GameObject.Find("Reticule");
        playerBoltSound = GameObject.Find("PlayerBoltSound");
        playerExplosionSound = GameObject.Find("PlayerExplosionSound");
        nextFire = Time.time;

        reticule.GetComponent<Renderer>().material.SetColor("_Color", new Color(1.0f,.0f,.0f));
        bigExplosionEffect = GameObject.Find("BigExplosionEffect");

        flashColor = new Color(10.0f, 5.0f, 0.0f);
        flashColor *= 0.75f;
        originalColor = gameObject.GetComponent<Renderer>().material.color;

        maxHealth = 5.0f;
        health = maxHealth;
        playedExplosion = false;
    }

    public void Reset()
    {
        health = maxHealth;
        nextFire = Time.time;
        gameObject.GetComponent<Renderer>().enabled = true;
        playedExplosion = false;
    }

    void Update()
    {
        if(health <= 0)
        {
            if (!playedExplosion)
            {
                GameObject newExplosion = Instantiate(bigExplosionEffect, gameObject.transform.position, Quaternion.identity);
                float time = newExplosion.GetComponent<ParticleSystem>().main.duration;
                Destroy(newExplosion, time);
                playedExplosion = true;

                playerExplosionSound.GetComponent<AudioSource>().Play();
            }

            gameObject.GetComponent<Renderer>().enabled = false;
            return;
        }
    }

    void FixedUpdate()
    {
        KeyCode xUpKey = KeyCode.RightArrow;
        KeyCode xDownKey = KeyCode.LeftArrow;
        KeyCode yUpKey = KeyCode.UpArrow;
        KeyCode yDownKey = KeyCode.DownArrow;
        KeyCode zUpKey = KeyCode.W;
        KeyCode zDownKey = KeyCode.S;

        float xMovement = 0.0f;
        float yMovement = 0.0f;
        float zMovement = 0.0f;

        // Use key input to change the velocity of the player in the correct direction 

        if(Input.GetKey(xUpKey))
        {
            xMovement += 1.0f;
        }
        if(Input.GetKey(xDownKey))
        {
            xMovement -= 1.0f;
        }

        if(Input.GetKey(yUpKey))
        {
            yMovement += 1.0f;
        }
        if(Input.GetKey(yDownKey))
        {
            yMovement -= 1.0f;
        }

        if (Input.GetKey(zUpKey))
        {
            zMovement += 1.0f;
        }
        if (Input.GetKey(zDownKey))
        {
            zMovement -= 1.0f;
        }
        if (Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject newShot = Instantiate(bolt, camera.transform.position, bolt.transform.rotation);

            Rigidbody boltRigidbody = newShot.GetComponent<Rigidbody>();
            Vector3 vel = reticule.transform.position - camera.transform.position;
            vel.Normalize();


            boltRigidbody.velocity = boltSpeed * vel;

            Quaternion q;
            Vector3 a = Vector3.Cross(new Vector3(0, 0, 1), vel);
            q.x = a.x;
            q.y = a.y;
            q.z = a.z;
            q.w = Vector3.Dot(new Vector3(0, 0, 1), vel);

            boltRigidbody.rotation = q * boltRigidbody.rotation;
            boltRigidbody.position += vel * 2.0f;

            playerBoltSound.GetComponent<AudioSource>().Play();
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(speed * xMovement, speed * yMovement, speed * zMovement);

        // Clamp the player position to be inside the boundary

        GameObject boundary = GameObject.Find("Boundary");

        float margin = 1;
        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, -0.5f * boundary.transform.localScale.x + margin, 0.5f * boundary.transform.localScale.x - margin),
            Mathf.Clamp(rigidbody.position.y, -0.5f * boundary.transform.localScale.z + margin, 0.5f * boundary.transform.localScale.z - margin),
            0.0f
        );

        // Make the ship bank in the right direction
        float maxRotationSpeed = 70;
        float maxRotation = 35;
        float rotationSpeed = 50;

        if(rigidbody.velocity.x > 0)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, -maxRotation), rotationSpeed * Time.deltaTime);

        } else if(rigidbody.velocity.x < 0)
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
