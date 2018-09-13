using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

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
    public bool resetting;
    bool spawningWave;

    // Use this for initialization
    void Start()
    {
        alien1 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaZoid");
        alien2 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaRidged");
        alien3 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaBulky");
        player = GameObject.Find("Player");

        boundary = GameObject.Find("Boundary");
        waveText = GameObject.Find("WaveText");

        playerHealthText = GameObject.Find("PlayerHealthText");
        scoreText = GameObject.Find("ScoreText");

        score = 0;
        gameIsOver = false;
        currentWave = 1;
        spawningWave = false;
        resetting = false;

        waveText.SetActive(false);

        SpawnWave();
    }

    void SpawnWave()
    {
        waveText.SetActive(false);
        float radius = boundary.transform.localScale.x / 2;
        float alienRadius = radius - 3;

        float range = farthestDistance - closestDistance;

        for (int i = 0; i < 1; i++)
        {
            //GameObject cube = Instantiate(GameObject.Find("Cube"), new Vector3(0, 0, 10), Quaternion.identity);
            //cube.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, -10);
            Instantiate(alien1, new Vector3(Random.value * 20 + -10, 0, Random.value * 10 + 5), new Quaternion());
            currentNumAliens++;
        }

        //for (int i = 0; i < currentWave * 2; i++)
        //{
        //    Instantiate(alien2, new Vector3((Random.value * 2 + -1) * alienRadius, (Random.value * 2 + -1) * alienRadius, Random.value * range + closestDistance), new Quaternion());
        //    currentNumAliens++;
        //}

        //for (int i = 0; i < currentWave - 1; i++)
        //{
        //    Instantiate(alien3, new Vector3((Random.value * 2 + -1) * alienRadius, (Random.value * 2 + -1) * alienRadius, Random.value * range + closestDistance), new Quaternion());
        //    currentNumAliens++;
        //}

        spawningWave = false;
        currentWave++;
    }

    public void UpdateScore(float scorePoints)
    {
        score += scorePoints;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        //if (resetting)
        //{
        //    return;
        //}

        //if(gameIsOver)
        //{
        //    ResetGame();
        //    return;
        //}

        scoreText.GetComponent<Text>().text = "Score: " + score;
        playerHealthText.GetComponent<Text>().text = "Health: " + player.GetComponent<PlayerController>().health;

        //if(player.GetComponent<PlayerController>().health <= 0)
        //{
        //    gameIsOver = true;
        //}

        //if(currentNumAliens == 0 && !spawningWave)
        //{
        //    waveText.SetActive(true);
        //    waveText.GetComponent<Text>().text = "Wave " + currentWave;
        //    Invoke("SpawnWave", 1);
        //    spawningWave = true;
        //}
    }

    void ResetGame()
    {
        if (Input.GetKey(KeyCode.R))
        {
            resetting = true;

            foreach (GameObject o in GameObject.FindGameObjectsWithTag("GammaZoid"))
            {
                if (o.name.Contains("Clone"))
                {
                    Destroy(o);
                }
            }

            foreach (GameObject o in GameObject.FindGameObjectsWithTag("GammaRidged"))
            {
                if (o.name.Contains("Clone"))
                {
                    Destroy(o);
                }
            }

            foreach (GameObject o in GameObject.FindGameObjectsWithTag("GammaBulky"))
            {
                if (o.name.Contains("Clone"))
                {
                    Destroy(o);
                }
            }

            foreach (GameObject o in GameObject.FindGameObjectsWithTag("AlienBolt"))
            {
                if (o.name.Contains("Clone"))
                {
                    Destroy(o);
                }
            }

            player.GetComponent<PlayerController>().Reset();

            currentNumAliens = 0;
            currentWave = 1;
            gameIsOver = false;
            spawningWave = false;
            score = 0;
            resetting = false;
        }
    }
}
