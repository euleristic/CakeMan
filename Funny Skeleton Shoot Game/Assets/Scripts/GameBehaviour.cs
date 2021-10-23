using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBehaviour : MonoBehaviour, IDamagable, IPlayerCollide
{
    [SerializeField] int hp;
    bool facingRight = true;
    [SerializeField] float inversePeriod;
    [SerializeField] float xSinAmp;
    [SerializeField] float ySinAmp;
    [SerializeField] int bonesDrop;
    [SerializeField] GameObject boneToPick;
    [SerializeField] float boneMaxX;
    [SerializeField] float boneMaxY;
    [SerializeField] float boneMaxXVel;
    [SerializeField] float boneMinYVel;
    [SerializeField] float boneMaxYVel;
    [SerializeField] float boneMaxAng;
    Vector3 startingPos;

    [SerializeField] private AudioClip dieSound;

    void Start()
    {
        startingPos = transform.position;
    }


    void Update()
    {
        transform.position = startingPos + new Vector3(Mathf.Sin(Time.time * inversePeriod) * xSinAmp, -Mathf.Sin(Time.time * 3f) * ySinAmp);
        if (!facingRight && Mathf.Sign(Mathf.Cos(Time.time * inversePeriod)) > 0f)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }
        if (facingRight && Mathf.Sign(Mathf.Cos(Time.time * inversePeriod)) < 0f)
        {
            facingRight = !facingRight;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, 1f);
        }

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
            Rigidbody2D rb = Instantiate(boneToPick, transform.position +
                new Vector3(Random.Range(-boneMaxX, boneMaxX), Random.Range(0f, boneMaxY)),
                Quaternion.identity).GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(Random.Range(-boneMaxXVel, boneMaxXVel), Random.Range(boneMinYVel, boneMaxYVel));
            rb.angularVelocity = Random.Range(-boneMaxAng, boneMaxAng);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            var rb = transform.GetChild(i).gameObject.AddComponent<Rigidbody2D>();
            if (rb != null)
            {
                var force = Random.Range(10f, 15f);
                rb?.AddForce(new Vector3(Random.Range(-1f, 1f), 1) * force, ForceMode2D.Impulse);
                rb.angularVelocity = Random.Range(-10000f, 10000f);
                Destroy(rb?.gameObject, 5f);
            }
        }
        SoundEffectPlayer.PlaySoundEffect(dieSound, 1f, 1f);
        transform.DetachChildren();
        Destroy(gameObject);
    }

    public void OnCollideWithPlayer(PlayerController player)
    {
        player.GetComponent<IDamagable>()?.TakeDamage(1);
    }
}
