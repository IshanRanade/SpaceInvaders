using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject alien1;
    private GameObject alien2;
    private GameObject alien3;

	// Use this for initialization
	void Start () {
        alien1 = GameObject.Find("GammaZoid");
        alien2 = GameObject.Find("GammaRidged");
        alien3 = GameObject.Find("GammaBulky");

        SpawnWave();
	}

    void SpawnWave()
    {
        for(int i = 0; i < 5; i++)
        {
            Instantiate(alien1, new Vector3(Random.value * 20, Random.value * 20, Random.value * 40 + 10), new Quaternion());
            Instantiate(alien2, new Vector3(Random.value * 20, Random.value * 20, Random.value * 40 + 10), new Quaternion());
            Instantiate(alien3, new Vector3(Random.value * 20, Random.value * 20, Random.value * 40 + 10), new Quaternion());
        }
    }
}
