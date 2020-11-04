using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovment : MonoBehaviour
{

    Rigidbody2D rb;

    float moveSpeed = 5;

    public GameObject enemy;

    public GameObject spawn = null;

    public GameObject[] wasteArray;

    int randomNum;


    Vector3 enemyPos;

    float enemyXPos;

    Vector3 enemyScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        enemyScale = transform.localScale;
    }

    private void Start()
    {
        StartSpawn();
    }


    private void FixedUpdate()
    { 
        if (gameObject.name.Contains("Meat"))
        {
            rb.velocity = Vector2.left * moveSpeed;
        }

        if (gameObject.name.Contains("Man"))
        {
            rb.velocity = Vector2.right * moveSpeed;
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shot")
        {
            StopSpawn();
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

    void WasteSpawn()
    {
        enemyPos = enemy.transform.position;

        randomNum = Random.Range(0, 7);

        if (enemyScale.x == -25 || enemyScale.x == -8)
        {
            enemyXPos = enemyPos.x - 5f;

        }
        else if (enemyScale.x == 25 || enemyScale.x == 8)
        {
            enemyXPos = enemyPos.x + 5f;
        }



        spawn.transform.position = new Vector3(enemyXPos, (enemyPos.y - 2.66f), enemyPos.z);
        Instantiate(wasteArray[randomNum], spawn.transform.position, Quaternion.identity);
    }


    void StartSpawn()
    {
        if (gameObject.name.Contains("Meat"))
        {
            InvokeRepeating("WasteSpawn", 0f, 2.5f);
        }

        if (gameObject.name.Contains("Man"))
        {
            InvokeRepeating("WasteSpawn", 0f, 1.5f);
        }


    }

    public void StopSpawn()
    {
        CancelInvoke("WasteSpawn");
    }







}
