using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GammaZoidController : GammaController {

    public override void SetSpecificValues()
    {
        scorePoints = 1;
        alienBoltSpeed = 10f;
        health = 1;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        nextShotPeriod = 2.0f;
    }
}
