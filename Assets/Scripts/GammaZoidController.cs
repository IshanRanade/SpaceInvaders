using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GammaZoidController : GammaController {

    public override void SetSpecificValues()
    {
        plasmaExplosion = GameObject.Find("PlasmaExplosionEffect");
        scorePoints = 1;
        alienBoltSpeed = 15f;
        health = 1;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        nextShotTime = createdTime;
        nextShotPeriod = 1.5f;

        Rigidbody rigidbody = GetComponent<Rigidbody>();
        spinDirection = (Random.value > 0.5f);
        if (spinDirection)
        {
            rigidbody.velocity = new Vector3(0, 2, 0);
            rigidbody.velocity = Quaternion.Euler(0, 0, Random.value * 360) * rigidbody.velocity;
        }
        else
        {
            rigidbody.velocity = new Vector3(0, -2, 0);
            rigidbody.velocity = Quaternion.Euler(0, 0, Random.value * 360) * rigidbody.velocity;

        }
    }

    void FixedUpdate()
    {
        float spinSpeed = 3;
        Rigidbody rigidbody = GetComponent<Rigidbody>();

        if (spinDirection)
        {
            rigidbody.velocity = Quaternion.Euler(new Vector3(0, 0, spinSpeed)) * rigidbody.velocity;
        }
        else
        {
            rigidbody.velocity = Quaternion.Euler(new Vector3(0, 0, -spinSpeed)) * rigidbody.velocity;
        }
    }

    void Update()
    {
        if (!gameController.gameIsOver && Time.time > createdTime + 2.0)
        {
            ShootAtPlayer();
        }
    }


}
