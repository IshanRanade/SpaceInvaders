﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceGammaController : MonoBehaviour {

    Vector3 target;
    float speed;
    GameObject resourceEffect;

	// Use this for initialization
	void Start () {
        target = new Vector3(-transform.position.x, 0, transform.position.z);
        resourceEffect = Resources.Load<GameObject>("Prefab/ResourceEffect");
        speed = 10.0f;

        DoEffect();
    }

    private void DoEffect()
    {
        GameObject newEffect = Instantiate(resourceEffect, gameObject.transform.position, Quaternion.identity);
        float time = 0.75f;
        Destroy(newEffect, time);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(transform.position == target)
        {
            DoEffect();
            Destroy(gameObject);
        } else
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target, step);
        }
	}
}