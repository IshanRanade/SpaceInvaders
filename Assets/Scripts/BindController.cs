using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindController : MonoBehaviour {

    private float stayTime;
    private GameController gameController;
    private float createdTime;

	// Use this for initialization
	void Start () {
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        stayTime = gameController.bindStayTime;
        createdTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
        Renderer rend = GetComponent<Renderer>();
        if (Time.time < createdTime + gameController.bindActivateTime)
        {
            rend.material.SetFloat("_RimStrength", 1.5f);
        } else
        {
            rend.material.SetFloat("_RimStrength", 6.2f);
        }


		if(Time.time > createdTime + stayTime)
        {
            gameController.bindCurrentLocation = new Vector3(-100, -100, -100);
            gameController.bindsCurrentScore++;
            gameController.score += gameController.bindsCurrentScore;
            Destroy(gameObject);
        }
	}
}
