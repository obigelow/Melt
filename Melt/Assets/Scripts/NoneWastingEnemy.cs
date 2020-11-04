using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoneWastingEnemy : MonoBehaviour
{
    Rigidbody2D rb;

    float moveSpeed = 5;

    public GameObject melt;

    public GameObject enemy;

    Vector3 enemyScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScale = transform.localScale;
    }


    private void FixedUpdate()
    {
        rb.velocity = Vector2.right * moveSpeed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(melt);
        }
        if (collision.gameObject.tag == "Shot")
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyStop")
        {
            enemyScale.x *= -1;
            transform.localScale = enemyScale;
            moveSpeed *= -1;

        }

    }

}
