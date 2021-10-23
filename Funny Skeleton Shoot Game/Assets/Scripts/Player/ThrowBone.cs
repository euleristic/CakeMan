using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class ThrowBone : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private BoneToPick pickup;
    [SerializeField] private AudioClip tossSound;
    private void Awake()
    {
        SoundEffectPlayer.PlaySoundEffect(tossSound, 1f, 1f, 0.15f);
        rb = GetComponent<Rigidbody2D>();
    }
    public void Throw(Vector2 direction, float throwPower, Sprite spr = null)
    {
        if(rb == null)
        {
            Debug.LogError("No rigidbody(?!?!)");
            return;
        }

        if(spr != null)
        {
            GetComponent<SpriteRenderer>().sprite = spr;
        }

        direction.Normalize();
        rb.AddForce(direction * throwPower);
        rb.angularVelocity = -1000f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //do damage and stuff
        if(collision.transform.GetComponent<ThrowBone>() == null)
            BecomeBoneToPick();
    }

    private void BecomeBoneToPick()
    {
        Instantiate(pickup, transform.position + Vector3.up * 3f, Quaternion.identity);
        pickup.GetComponent<Rigidbody2D>().velocity = rb.velocity;
        Destroy(gameObject);
    }
}
