using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private GameObject player;
    private GameController gameController;
    private Vector3 originalPosition;
    private Quaternion originalRotation;

    private Vector3 hitPosition;

    public float shakeStrength = 0.01f;
    public float shake = 1f;
    public bool shaking = false;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("Player");
        gameController = GameObject.Find("GameController").GetComponent<GameController>();
        originalPosition = transform.position;
        originalRotation = transform.rotation;
	}
	
    void LateUpdate()
    {
        if (gameController.gameIsOver)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, originalPosition.y, originalPosition.z), 1.0f);
        transform.rotation = originalRotation;

        if (shaking)
        {
            transform.localPosition = transform.position + (Random.insideUnitSphere * shake) * 0.15f;
            shake = Mathf.MoveTowards(shake, 0, Time.deltaTime * shakeStrength);
        }
    }

    public void Shake()
    {
        shake = shakeStrength;
        shaking = true;
        hitPosition = transform.position;
        Invoke("Unshake", 0.5f);
    }

    public void Unshake()
    {
        shaking = false;
    }

}
