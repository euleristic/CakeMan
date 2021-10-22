using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogBehaviour : MonoBehaviour
{
    enum State
    {
        Walking,
        Chasing
    }
    State currentState;

    public int hp;
    [SerializeField] float walkSpeed;
    [SerializeField] float runSpeed;
    [SerializeField] float detectionRange;

    CharacterController controller;
    GameObject player;
    bool facingRight;
    float detectionSqr;
    Vector3 velocity;

    void Start()
    {
        facingRight = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
            Destroy(this);
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerRelative = player.transform.position - transform.position;
        if (playerRelative.sqrMagnitude < detectionSqr)
        {
            currentState = State.Chasing;
            velocity.x = new Vector2(playerRelative.x, 0f).normalized.x * runSpeed;
        }
        else
        {
            //controller.
        }


        controller.Move(velocity);
    }
}
