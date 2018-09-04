using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject alien;

	// Use this for initialization
	void Start () {
        alien = GameObject.Find("Alien");

        SpawnWave();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnWave()
    {
        Instantiate(alien, new Vector3(0, 0, 10), new Quaternion());
    }
}
