using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float attackRange = 2f;
    [SerializeField] float facingThreshold = 30f;

    [SerializeField] Transform playerTransform;

    public Rigidbody2D rb;
    public Animator anim;

    public bool isAlive = true;

    PlayerController player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        player = FindObjectOfType<PlayerController>();
    }


    void Update()
    {
        rb.velocity = new Vector2(moveSpeed, 0);
        CheckAttack();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "turnPoint":
                moveSpeed = -moveSpeed;
                transform.localScale = new Vector2(-(Mathf.Sign(rb.velocity.x)) * 3f, 3f);

                break;
        }

    }

    void CheckAttack()
    {
        Vector2 directionToPlayer = playerTransform.position - transform.position;
        float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg;

        if (Mathf.Abs(angle) < facingThreshold)
        {
            float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

            if (distanceToPlayer < attackRange && player.isAlive)
            {
                anim.SetTrigger("Attack");

            }
        }
    }
    
}
