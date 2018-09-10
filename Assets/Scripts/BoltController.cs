using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoltController : MonoBehaviour {

    public Quaternion eventualRotation;
    public Vector3 eventualVelocity;
	
	// Update is called once per frame
	void Update () {
        // Delete the bolt when it goes too far
        if (GetComponent<Rigidbody>().position.z > 0.5 * GameObject.Find("Boundary").transform.localScale.z * 8)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
           Physics.IgnoreCollision(collider.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }

        if (collider.gameObject.tag == "GammaZoid")
        {
            Destroy(gameObject);
        }
    }
}
