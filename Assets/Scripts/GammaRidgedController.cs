 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaRidgedController : GammaController {
    public override void SetSpecificValues()
    {
        plasmaExplosion = GameObject.Find("PlasmaExplosionEffect");
        scorePoints = 1;
        alienBoltSpeed = 40f;
        health = 3;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        nextShotPeriod = 0.75f;

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
