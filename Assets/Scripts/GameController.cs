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
    private GameObject backWall;

    private GameObject waveText;
    private GameObject scoreText;
    private GameObject playerHealthText;

    public int currentNumAliens;
    public int currentWave;

    private float score;
    public bool gameIsOver;
    public bool resetting;
    bool spawningWave;

    public float minX;
    public float maxX;
    public float startX;
    public float startZ;
    public float distanceX;
    public float distanceZ;
    public float alienSpeed;

    // Use this for initialization
    void Start()
    {
        alien1 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaZoid");
        alien2 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaRidged");
        alien3 = Resources.Load<GameObject>("GammaZoids/Prefabs/GammaBulky");
        backWall = GameObject.Find("BackWall");
        player = GameObject.Find("Player");
        boundary = GameObject.Find("Boundary");
        waveText = GameObject.Find("WaveText");
        playerHealthText = GameObject.Find("PlayerHealthText");
        scoreText = GameObject.Find("ScoreText");

        Physics.IgnoreCollision(player.GetComponent<Collider>(), backWall.GetComponent<Collider>());
        Physics.IgnoreCollision(player.GetComponent<Collider>(), GameObject.Find("Buffer").GetComponent<Collider>());

        Physics.gravity = new Vector3(0, 0, -9.8f);

        score = 0;
        gameIsOver = false;
        currentWave = 1;
        spawningWave = false;
        resetting = false;

        minX = -25;
        maxX = 25;

        startX = -20;
        startZ = 20;

        distanceX = 20;
        distanceZ = 5;

        waveText.SetActive(false);
    }

    void SpawnWave()
    {
        spawningWave = true;

        alienSpeed = 100.0f;

        float x = startX;
        float z = startZ;
        for (int i = 0; i < 5; i++)
        {
            for(int j = 0; j < 5; j ++)
            {

                GameObject alien = createGamma(new Vector3(x, 0, z), Quaternion.identity, "GammaZoid");

                if(z == 20)
                {
                    alien.GetComponent<GammaController>().canShoot = true;
                } else
                {
                    alien.GetComponent<GammaController>().canShoot = false;
                }

                currentNumAliens++;

                x += 3;
            }

            z += 4;
            x = -20;
        }

        spawningWave = false;
        currentWave++;
    }

    public void UpdateScore(float scorePoints)
    {
        score += scorePoints;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.R) && !resetting)
        {
            ResetGame();
        }

        if (resetting)
        {
            return;
        }

        scoreText.GetComponent<Text>().text = "Score: " + score;
        playerHealthText.GetComponent<Text>().text = "Health: " + player.GetComponent<PlayerController>().health;

        if (currentNumAliens == 0 && !spawningWave)
        {
            waveText.SetActive(true);
            waveText.GetComponent<Text>().text = "Wave " + currentWave;
            SpawnWave();
            Invoke("HideWaveText", 1);
        }
    }

    void ResetGame()
    {
        resetting = true;

        string[] tags = { "AlienBolt", "Bolt", "GammaZoid", "GammaRidged", "GammaBulky", "PlasmaExplosionEffect", "BigExplosionEffect" };

        foreach (string tag in tags) {
            foreach (GameObject o in GameObject.FindGameObjectsWithTag(tag))
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

    private void HideWaveText()
    {
        waveText.SetActive(false);
    }

    private GameObject createGamma(Vector3 position, Quaternion rotation, string name)
    {
        GameObject alien;
        if(name == "GammaZoid")
        {
            alien = Instantiate(alien1, position, rotation);

            alien.GetComponent<GammaController>().scorePoints = 1;
            alien.GetComponent<GammaController>().alienBoltSpeed = 10f;
            alien.GetComponent<GammaController>().health = 1;
            alien.GetComponent<GammaController>().flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
            alien.GetComponent<GammaController>().nextShotPeriod = 2.0f;
        }
        else if(name == "GammaRidged")
        {
            alien = Instantiate(alien2, position, Quaternion.identity);

            alien.GetComponent<GammaController>().scorePoints = 1;
            alien.GetComponent<GammaController>().alienBoltSpeed = 35f;
            alien.GetComponent<GammaController>().health = 5;
            alien.GetComponent<GammaController>().flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
            alien.GetComponent<GammaController>().nextShotPeriod = 2.0f;
        }
        else if(name == "GammaBulky")
        {
            alien = Instantiate(alien3, position, Quaternion.identity);

            alien.GetComponent<GammaController>().scorePoints = 1;
            alien.GetComponent<GammaController>().alienBoltSpeed = 35f;
            alien.GetComponent<GammaController>().health = 5;
            alien.GetComponent<GammaController>().flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
            alien.GetComponent<GammaController>().nextShotPeriod = 2.0f;
        }
        else
        {
            throw new System.ArgumentException("Not a real gamma zoid name");
        }

        return alien;
    }

}
