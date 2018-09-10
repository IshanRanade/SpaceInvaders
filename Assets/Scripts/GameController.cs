using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    private GameObject alien1;
    private GameObject alien2;
    private GameObject alien3;

    private GameObject boundary;

    float closestDistance = 30;
    float farthestDistance = 70;

	// Use this for initialization
	void Start () {
        alien1 = GameObject.Find("GammaZoid");
        alien2 = GameObject.Find("GammaRidged");
        alien3 = GameObject.Find("GammaBulky");

        boundary = GameObject.Find("Boundary");

        SpawnWave();
	}

    void SpawnWave()
    {
        float radius = boundary.transform.localScale.x / 2;
        float alienRadius = radius - 3;
        for(int i = 0; i < 5; i++)
        {
            float range = farthestDistance - closestDistance;
            Instantiate(alien1, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
            Instantiate(alien2, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
            Instantiate(alien3, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
        }
    }
}
