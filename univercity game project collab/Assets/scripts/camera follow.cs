using System;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    private void Update()
    {
        gameObject.transform.position = new Vector3(playerTransform.position.x, playerTransform.position.y, gameObject.transform.position.z);
    }
}
