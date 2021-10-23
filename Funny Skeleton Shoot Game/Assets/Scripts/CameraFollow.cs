using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform player;
    private Rigidbody2D pRb;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        pRb = player.GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, player.position + Vector3.ClampMagnitude((Vector3)pRb.velocity * 50f, 30f), 0.6f * Time.deltaTime);
        transform.position = transform.position.With(z: -10f);
    }
}
