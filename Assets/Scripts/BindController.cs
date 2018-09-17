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
		if(Time.time > createdTime + stayTime)
        {
            gameController.bindCurrentLocation = new Vector3(-100, -100, -100);
            Destroy(gameObject);
        }
	}
}
