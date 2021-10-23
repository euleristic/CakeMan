using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour, IDamagable
{
    enum State
    {
        Walking,
        Chasing
    }
    State currentState;
    State toState;

    public int hp;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float detectionRange;
    [SerializeField] float gravity;
    [SerializeField] Sprite neutralSprite;
    [SerializeField] Sprite angrySprite;
    [SerializeField] float boneMaxX;
    [SerializeField] float boneMaxY;
    [SerializeField] float boneMaxXVel;
    [SerializeField] float boneMaxYVel;
    [SerializeField] float boneMinYVel;
    [SerializeField] float boneMaxAng;
    [SerializeField] int bonesDrop;
    [SerializeField] GameObject BoneToPick;

    [Header("Don't change me!")]
    [SerializeField] SpriteRenderer headRenderer;
    [SerializeField] Sensor forward;
    [SerializeField] Sensor forwardDown;
    [SerializeField] Sensor down;
    [SerializeField] WiggleObject[] legs;

    GameObject player;
    bool facingRight;
    float detectionSqr;
    Vector3 velocity;
    [SerializeField] private AudioClip dieSound;

    void Start()
    {
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Destroy(this);
        detectionSqr = detectionRange * detectionRange;

    }


    void Update()
    {
        if (toState != currentState)
        {
            currentState = toState;
            switch (currentState)
            {
                case State.Walking:
                    headRenderer.sprite = neutralSprite;
                    foreach (var leg in legs)
                        leg.SetWiggleSpeedFloat(walkSpeed);
                    break;
                case State.Chasing:
                    headRenderer.sprite = angrySprite;
                    foreach (var leg in legs)
                        leg.SetWiggleSpeedFloat(runSpeed);
                    break;
                default:
                    //what
                    break;
            }
        }

        //x
        Vector3 playerRelative = player.transform.position - transform.position;
        if (playerRelative.sqrMagnitude < detectionSqr)
        {
            toState = State.Chasing;
            velocity.x = new Vector2(playerRelative.x, 0f).normalized.x * runSpeed;
            if ((velocity.x < 0f && facingRight ||
                 velocity.x > 0f && !facingRight) && Mathf.Abs(playerRelative.x) > .1f)
            {
                facingRight = !facingRight;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            toState = State.Walking;
            if (forward.Triggered() || !forwardDown.Triggered())
            {
                facingRight = !facingRight;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            velocity.x = (facingRight ? walkSpeed : -walkSpeed);
        }

        //apply
        transform.position += velocity * Time.deltaTime;

        //y
        if (down.Triggered()) velocity.y = Mathf.Max(velocity.y, 0f);
        else velocity.y += Mathf.Max(gravity * Time.deltaTime);
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            Die();
    }

    private void Die()
    {
        for (int i = 0; i < bonesDrop; i++)
        {
            Rigidbody2D rb = Instantiate(BoneToPick, transform.position +
                new Vector3(Random.Range(-boneMaxX, boneMaxX), Random.Range(0f, boneMaxY)),
                Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Random.Range(-boneMaxXVel, boneMaxXVel), Random.Range(boneMinYVel, boneMaxYVel));
            rb.angularVelocity = Random.Range(-boneMaxAng, boneMaxAng);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var rb = transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
            if(rb != null)
            {
                var force = Random.Range(10f, 15f);
                rb?.AddForce(new Vector3(Random.Range(-1f, 1f), 1) * force, ForceMode2D.Impulse);
                rb.angularVelocity = Random.Range(-10000f, 10000f);
                Destroy(rb?.gameObject, 5f);
            }
        }
        SoundEffectPlayer.PlaySoundEffect(dieSound, 1f, 1f / transform.localScale.x);
        transform.DetachChildren();
        Destroy(gameObject);
    }
}