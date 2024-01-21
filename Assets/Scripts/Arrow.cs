using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] float arrowSpeed;
    [SerializeField] float xArrow = 1f;

    PlayerController player;
    //EnemyController enemy;

    List<EnemyController> enemies = new List<EnemyController>();
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerController>();
        //enemy = FindObjectOfType<EnemyController>();
        EnemyController[] enemyControllers = FindObjectsOfType<EnemyController>();
        xArrow = player.transform.localScale.x * arrowSpeed;

    }
    void Update()
    {
        rb.velocity = new Vector2(xArrow, 0f);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Debug.Log("noluyo");
            EnemyController enemy = col.GetComponent<EnemyController>();




            enemy.anim.SetTrigger("Death");
                enemy.isAlive = false;
                enemy.rb.simulated = false;
            Debug.Log("ananý silerim");

        }
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
