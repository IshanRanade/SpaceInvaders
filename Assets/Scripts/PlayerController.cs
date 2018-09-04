using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    public float speed;

    public float fireRate;
    public float boltSpeed;
    private GameObject bolt;

    private float nextFire;

    private void Start()
    {
        bolt = GameObject.Find("Bolt");
    }

    void Update()
    {
        if(Input.GetKey(KeyCode.Space) && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            GameObject newShot = Instantiate(bolt, GetComponent<Rigidbody>().position, bolt.transform.rotation);

            Rigidbody boltRigidbody = newShot.GetComponent<Rigidbody>();
            boltRigidbody.velocity = new Vector3(0, 0, boltSpeed);   
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

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rigidbody.velocity = new Vector3(speed * xMovement, speed * yMovement, speed * zMovement);

        // Clamp the player position to be inside the boundary

        GameObject boundary = GameObject.Find("Boundary");

        float margin = 1;
        rigidbody.position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, -0.5f * boundary.transform.localScale.x + margin, 0.5f * boundary.transform.localScale.x - margin),
            Mathf.Clamp(rigidbody.position.y, -0.5f * boundary.transform.localScale.y + margin, 0.5f * boundary.transform.localScale.y - margin),
            Mathf.Clamp(rigidbody.position.z, -0.5f * boundary.transform.localScale.z + margin, 0.5f * boundary.transform.localScale.z - margin)
        );

    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bolt")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
