using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisController : MonoBehaviour {

	// Update is called once per frame
	void FixedUpdate () {
		if(transform.position.z < -20)
        {
            Destroy(gameObject);
        }
	}
}
