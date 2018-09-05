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

    void SpawnWave()
    {
        for(int i = 0; i < 10; i++)
        {
            Instantiate(GameObject.Find("Alien"), new Vector3(Random.value * 20, Random.value * 20, Random.value * 50), new Quaternion());
        }
    }
}
