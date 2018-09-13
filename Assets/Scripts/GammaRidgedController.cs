 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GammaRidgedController : GammaController {

    public override void SetSpecificValues()
    {
        scorePoints = 3;
        alienBoltSpeed = 20f;
        health = 3;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        nextShotPeriod = 3.0f;
    }
}
