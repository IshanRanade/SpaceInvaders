using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    private GameObject alien1;
    private GameObject alien2;
    private GameObject alien3;

    private GameObject player;

    private GameObject boundary;
    private GameObject waveText;
    private GameObject scoreText;
    private GameObject playerHealthText;

    float closestDistance = 25;
    float farthestDistance = 50;

    public float currentNumAliens;
    float currentWave;

    private float score;

    public bool gameIsOver;
    bool spawningWave;

    // Use this for initialization
    void Start()
    {
        alien1 = GameObject.Find("GammaZoid");
        alien2 = GameObject.Find("GammaRidged");
        alien3 = GameObject.Find("GammaBulky");
        player = GameObject.Find("Player");

        boundary = GameObject.Find("Boundary");
        waveText = GameObject.Find("WaveText");

        playerHealthText = GameObject.Find("PlayerHealthText");
        scoreText = GameObject.Find("ScoreText");

        score = 0;
        gameIsOver = false;
        currentWave = 1;
        spawningWave = false;

        waveText.SetActive(false);

        PlayGame();
    }

    void SpawnWave()
    {
        waveText.SetActive(false);
        float radius = boundary.transform.localScale.x / 2;
        float alienRadius = radius - 3;
        for(int i = 0; i < 1; i++)
        {
            float range = farthestDistance - closestDistance;
            Instantiate(alien1, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
            //Instantiate(alien2, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
            //Instantiate(alien3, new Vector3(Random.value * alienRadius, Random.value * alienRadius, Random.value * range + closestDistance), new Quaternion());
            currentNumAliens++;
        }

        spawningWave = false;
        currentWave++;
    }

    public void UpdateScore(float scorePoints)
    {
        score += scorePoints;
    }

    void Update()
    {
        if(gameIsOver)
        {
            return;
        }

        scoreText.GetComponent<Text>().text = "Score: " + score;
        playerHealthText.GetComponent<Text>().text = "Health: " + player.GetComponent<PlayerController>().health;

        if(player.GetComponent<PlayerController>().health <= 0)
        {
            gameIsOver = true;
        }

        if(currentNumAliens == 0 && !spawningWave)
        {
            waveText.SetActive(true);
            waveText.GetComponent<Text>().text = "Wave " + currentWave;
            Invoke("SpawnWave", 1);
            spawningWave = true;
        }
    }

    void ResetGame()
    {

    }

    void PlayGame()
    {

    }
}
