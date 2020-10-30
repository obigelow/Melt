using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltmanMovment : MonoBehaviour
{
    Rigidbody2D rb;

    private float moveSpeed = 15;

    private float jumpSpeed = 800;

    float xMovment;

    int jumpCount = 0;

    Animator anim;

    bool rightDown = true;
    bool leftDown = false;
    bool rightDownRun = false;
    bool leftDownRun = false;
    bool shootStandingRight = false;
    bool shootStandingLeft = false;
    bool shootRunningRight = false;
    bool shootRunningLeft = false;

    bool damaged = false;
    bool dying = false;
    bool previousRight;


    public GameObject shotLeft;
    public GameObject shotRight;







    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

    }


    private void FixedUpdate()
    {
        MovePlayerSide();

    }

    private void Update()
    {
        Jump();
        ConfiAnimations();

    }



    void MovePlayerSide()
    {
        xMovment = Input.GetAxisRaw("Horizontal");

        rb.velocity = new Vector2(xMovment * moveSpeed, rb.velocity.y);




    }

    void ConfiAnimations()
    {

        if (!damaged || !dying)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rightDownRun = false;
                rightDown = false;
                leftDown = false;
                leftDownRun = true;
                shootStandingRight = false;
                shootStandingLeft = false;
                shootRunningRight = false;
                shootRunningLeft = false;
                previousRight = false;

            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                leftDown = false;
                rightDownRun = true;
                rightDown = false;
                leftDownRun = false;
                shootStandingRight = false;
                shootStandingLeft = false;
                shootRunningRight = false;
                shootRunningLeft = false;
                previousRight = true;



            }
            if (Input.GetKeyUp(KeyCode.LeftArrow))
            {
                rightDownRun = false;
                rightDown = false;
                leftDown = true;
                leftDownRun = false;
                shootStandingRight = false;
                shootStandingLeft = false;
                shootRunningRight = false;
                shootRunningLeft = false;
                previousRight = false;


            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                rightDown = true;
                rightDownRun = false;
                leftDown = false;
                leftDownRun = false;
                shootStandingRight = false;
                shootStandingLeft = false;
                shootRunningRight = false;
                shootRunningLeft = false;
                previousRight = true;



            }
            if (Input.GetKeyDown(KeyCode.RightShift))
            {

                if (rightDown)
                {
                    rightDown = false;
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingLeft = false;
                    shootStandingRight = true;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = true;


                }
                if (leftDown)
                {
                    rightDown = false;
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = true;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = false;


                }
                if (rightDownRun)
                {
                    rightDown = false;
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    shootRunningRight = true;
                    shootRunningLeft = false;
                    previousRight = true;


                }
                if (leftDownRun)
                {
                    rightDown = false;
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    shootRunningRight = false;
                    shootRunningLeft = true;
                    previousRight = false;


                }
                if (rightDown || rightDownRun || shootRunningRight || shootStandingRight)
                {
                    Vector3 shotPos = new Vector3((gameObject.transform.position.x + 1f), (gameObject.transform.position.y), (gameObject.transform.position.z));
                    Instantiate(shotRight, shotPos, Quaternion.identity);

                }
                if (leftDown || leftDownRun || shootRunningLeft || shootStandingLeft)
                {
                    Vector3 shotPos = new Vector3((gameObject.transform.position.x - 1f), (gameObject.transform.position.y), (gameObject.transform.position.z));
                    Instantiate(shotLeft, shotPos, Quaternion.identity);

                }




            }

            if (Input.GetKeyUp(KeyCode.RightShift))
            {

                if (shootStandingRight)
                {
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    rightDown = true;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = true;


                }
                if (shootStandingLeft)
                {
                    rightDownRun = false;
                    leftDown = true;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    rightDown = false;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = false;


                }
                if (shootRunningRight)
                {
                    rightDownRun = true;
                    leftDown = false;
                    leftDownRun = false;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    rightDown = false;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = true;


                }
                if (shootRunningLeft)
                {
                    rightDownRun = false;
                    leftDown = false;
                    leftDownRun = true;
                    shootStandingRight = false;
                    shootStandingLeft = false;
                    rightDown = false;
                    shootRunningRight = false;
                    shootRunningLeft = false;
                    previousRight = false;


                }
            }

            anim.SetBool("melt_right", rightDown);
            anim.SetBool("melt_left", leftDown);
            anim.SetBool("melt_right_run", rightDownRun);
            anim.SetBool("melt_left_run", leftDownRun);
            anim.SetBool("shoot_while_standing_right", shootStandingRight);
            anim.SetBool("shoot_while_standing_left", shootStandingLeft);
            anim.SetBool("running_right_shooting", shootRunningRight);
            anim.SetBool("running_shooting_left", shootRunningLeft);
        }
        else
        {
            return;
        }


    }



    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount == 0)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));
                if (rightDown || rightDownRun)
                {
                    anim.SetTrigger("melt_jump_right");

                }
                if (leftDown || leftDownRun)
                {

                    anim.SetTrigger("melt_jump_left");

                }



            }
            if (jumpCount == 1)
            {
                rb.AddForce(new Vector2(rb.velocity.x, jumpSpeed));


            }
            jumpCount++;
        }

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            jumpCount = 0;
            rb.rotation = 0f;



        }
        if (collision.gameObject.tag == "MovingGround")
        {
            jumpCount = 0;
            rb.rotation = 0f;

            this.transform.parent = collision.transform;


        }
        if (collision.gameObject.tag == "SlantGroundRight")
        {
            jumpCount = 0;
            rb.rotation = -45f;
            rb.gravityScale = 100;
        }
        if (collision.gameObject.tag == "SlantGroundLeft")
        {
            jumpCount = 0;
            rb.rotation = 45f;
            rb.gravityScale = 65;

        }
        if (collision.gameObject.tag == "Waste")
        {
            gameObject.transform.localScale -= new Vector3(0.005f, 0.005f, 0.005f);
            damaged = true;
            if (previousRight)
            {
                anim.SetTrigger("melt_damage_right");

            }
            else
            {
                anim.SetTrigger("melt_damage_left");
            }
            damaged = false;





        }
        if (collision.gameObject.tag == "Enemy")
        {
            dying = true;
            if (previousRight)
            {
                anim.SetTrigger("melt_death_right");
            }
            else
            {
                anim.SetTrigger("melt_death_left");
            }

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingGround")
        {

            this.transform.parent = null;


        }
        if (collision.gameObject.tag.Contains("Slant"))
        {
            rb.gravityScale = 5;
        }






    }




}
