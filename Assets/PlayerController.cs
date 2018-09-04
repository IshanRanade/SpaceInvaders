using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour {

    public float speed;

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

    }
}
