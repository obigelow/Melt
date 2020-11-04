using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotMovment : MonoBehaviour
{

    Rigidbody2D rb;

    [SerializeField]
    private float shotSpeed;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Vector2.right * shotSpeed;
        if (rb.position.x > 1000 || rb.position.x < -1000)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "MovingGround" || collision.gameObject.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }


}
