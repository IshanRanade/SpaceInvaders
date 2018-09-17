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
    private GameObject camera;

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
        camera = GameObject.Find("Camera");

        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        
        flashColor = new Color(10.0f, 5.0f, 0.0f);
        flashColor *= 0.75f;
        originalColor = gameObject.GetComponent<Renderer>().material.color;

        nextFire = Time.time;
        maxHealth = 10.0f;
        health = maxHealth;
        isDead = false;
    }

    public void Reset()
    {
        health = maxHealth;
        nextFire = Time.time;
        gameObject.GetComponent<Renderer>().enabled = true;
        isDead = false;
        transform.position = new Vector3(0, 0, 0);
        transform.rotation = Quaternion.identity;
        GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
    }

    public void BlowUp()
    {
        GameObject newExplosion = Instantiate(bigExplosionEffect, gameObject.transform.position, Quaternion.identity);
        float time = newExplosion.GetComponent<ParticleSystem>().main.duration;
        Destroy(newExplosion, time);

        playerExplosionSound.GetComponent<AudioSource>().Play();

        gameObject.GetComponent<Renderer>().enabled = false;
        isDead = true;
    }

    void FixedUpdate()
    {
        if(isDead)
        {
            return;
        }

        if(health <= 0)
        {
            BlowUp();
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

            newShot.transform.forward = transform.forward;
            newShot.transform.rotation *= Quaternion.Euler(90, 0, 0);
            
            newShot.transform.position += transform.rotation * new Vector3(0, 0, 2.3f);

            Rigidbody boltRigidbody = newShot.GetComponent<Rigidbody>();
            Vector3 vel = transform.forward;
            vel.Normalize();

            boltRigidbody.velocity = boltSpeed * vel;

            playerBoltSound.GetComponent<AudioSource>().Play();
        }

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(speed * xMovement, 0, 0);
        
        // Clamp the player position to be inside the boundary
        rigidbody.position = new Vector3(Mathf.Clamp(rigidbody.position.x, gameController.minX, gameController.maxX), 0.0f, 0.0f);

        // Make the ship bank in the right direction
        float maxRotation = 35;

        Quaternion targetRotation;

        if (rigidbody.velocity.x > 0)
        {
            targetRotation = Quaternion.Euler(0, 0, -maxRotation);
        }
        else if (rigidbody.velocity.x < 0)
        {
            targetRotation = Quaternion.Euler(0, 0, maxRotation);
        }
        else
        {
            targetRotation = Quaternion.identity;
        }

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 100.0f * Time.deltaTime);

        // Check if the player is trapped in a bind
        if(transform.position.x + 1.5 > gameController.bindCurrentLocation.x - gameController.bindRadius &&
            transform.position.x - 1.5 < gameController.bindCurrentLocation.x + gameController.bindRadius &&
            Time.time > gameController.bindCreatedTime + gameController.bindActivateTime)
        {
            rigidbody.velocity = new Vector3(0, 0, 0);
            gameController.bindsCurrentScore = -1;
        }
    }

    public void GotHit()
    {
        camera.GetComponent<CameraController>().Shake();

        health = Mathf.Max(0, health - 1);

        if(health == 0)
        {
            gameController.gameIsOver = true;
            return;
        }

        gameObject.GetComponent<Renderer>().material.SetColor("_Color", flashColor);
        Invoke("ResetColor", 0.5f);
    }

    void ResetColor()
    {
        gameObject.GetComponent<Renderer>().material.SetColor("_Color", originalColor);
    }
}
