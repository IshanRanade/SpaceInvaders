 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GammaRidgedController : GammaController {
    public override void SetSpecificValues()
    {
        scorePoints = 1;
        alienBoltSpeed = 35f;
        health = 3;
        flashColor = new Color(240.0f / 255.0f, 141.0f / 255.0f, 141.0f / 255.0f);
        nextShotPeriod = 0.5f;
    }


}
