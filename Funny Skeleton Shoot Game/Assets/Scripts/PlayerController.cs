using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpHeight;
    private Rigidbody2D rigidbody;
    private bool grounded;

    private WiggleObject[] wigglers;

    private int throwableBones;

    [SerializeField] private SpriteRenderer[] sprites;

    private void Start()
    {
        wigglers = GetComponentsInChildren<WiggleObject>();
        rigidbody = GetComponent<Rigidbody2D>();
        throwableBones = sprites.Length;
    }
    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump"))
            Jump();
        if(Input.GetButtonDown("Fire1"))
        {
            ThrowBone();
        }
        Animate();
    }
    private void Animate()
    {
        foreach(var v in wigglers)
        {
            v.SetInputFloat(Mathf.Min(Mathf.Abs(rigidbody.velocity.x / 10), 2f));
        }
    }

    private void Move(float direction)
    {
        rigidbody.AddForce(new Vector2(direction, 0f) * moveSpeed);
    }
    private void Jump()
    {
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, jumpHeight);
    }
    private void ThrowBone()
    {
        throwableBones--;
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = i < throwableBones;
        }
    }
}
