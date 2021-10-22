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
    [SerializeField] int bonesDrop;
    [SerializeField] GameObject BoneToPick;

    [Header("Don't change me!")]
    [SerializeField] Sensor forward;
    [SerializeField] Sensor forwardDown;
    [SerializeField] Sensor down;

    SpriteRenderer spriteRenderer;
    GameObject player;
    bool facingRight;
    float detectionSqr;
    Vector3 velocity;

    void Start()
    {
        facingRight = true;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Destroy(this);
        detectionSqr = detectionRange * detectionRange;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (toState != currentState)
        {
            currentState = toState;
            switch (currentState)
            {
                case State.Walking:
                    spriteRenderer.sprite = neutralSprite;
                    break;
                case State.Chasing:
                    spriteRenderer.sprite = angrySprite;
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

        //y
        if (down.Triggered()) velocity.y = Mathf.Max(velocity.y, 0f);
        else velocity.y += gravity * Time.deltaTime;

        //apply
        transform.position += velocity * Time.deltaTime;
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp <= 0)
            throw new System.NotImplementedException();
    }

    private void Die()
    {
        //for (int)
    }
}
