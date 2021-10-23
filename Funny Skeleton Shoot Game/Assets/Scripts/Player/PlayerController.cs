using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed, jumpHeight, throwStrength;
    private Rigidbody2D rigidbody;
    private bool grounded;

    private WiggleObject[] wigglers;

    private int throwableBones;

    [SerializeField] private SpriteRenderer[] sprites;
    [SerializeField] private Transform throwStart;
    [SerializeField] private ThrowBone throwableBone;
    [SerializeField] private AudioClip boneAddSound;
    private Camera cam;
    private void Start()
    {
        cam = Camera.main;
        wigglers = GetComponentsInChildren<WiggleObject>();
        rigidbody = GetComponent<Rigidbody2D>();
        throwableBones = sprites.Length;
    }
    private void Update()
    {
        Move(Input.GetAxis("Horizontal"));
        if (Input.GetButtonDown("Jump") && IsGrounded())
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

    public bool GetBone()
    {
        if (throwableBones >= sprites.Length) return false;
        throwableBones++;
        UpdateBoneVisibilities();
        if (boneAddSound != null)
            SoundEffectPlayer.PlaySoundEffect(boneAddSound, 1f, 1.2f, 0.125f);
        return true;
    }
    private void ThrowBone()
    {
        if (throwableBones < 1) return;
        throwableBones--;

        UpdateBoneVisibilities();

        var bone = Instantiate(throwableBone, throwStart.position, transform.rotation);
        
        bone.Throw(cam.ScreenToWorldPoint(Input.mousePosition) -transform.position, throwStrength, sprites[throwableBones].sprite);
    }

    private void UpdateBoneVisibilities()
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].enabled = i < throwableBones;
        }
    }

    private bool IsGrounded()
    {
        var len = 3f;
        var hit = Physics2D.Raycast(transform.position, Vector3.down, len);
        Debug.DrawRay(transform.position, Vector3.down * len, Color.yellow, 1f);
        if (hit)
        {
           return true;
        }
        return false;
    }



    public void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.GetComponent<IPlayerCollide>()?.OnCollideWithPlayer(this);
    }
}
