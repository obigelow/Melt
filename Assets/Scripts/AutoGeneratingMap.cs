using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGeneratingMap : MonoBehaviour
{
    public class Platform
    {
        public Vector3 Start;
        public Vector3 End;
        public int Number;
        public GameObject Plat;
        public Platform(Vector3 start, Vector3 end, int number, GameObject plat)
        {
            Start = start;
            End = end;
            Number = number;
            Plat = plat;
        }
    }

    public GameObject[] enemies;

    public GameObject platformMes;

    public GameObject[] platforms;

    public GameObject platformCoin;

    public GameObject finalPlatform;

    public GameObject coin;

    public GameObject melt;

    public GameObject trigger1;

    public GameObject trigger2;


    int numberOfPlatforms;
    Platform[] platformsOnScreen;


    Vector3 nextPos;

    Vector3 currentPos;

    Vector3 coinPos;

    Vector3 coinPlatformPos;

    Vector3 tempEnd;

    Vector3 finalPos;

    Vector3 enemyPos;

    Vector3 enemyTrigger1Pos;

    Vector3 enemyTrigger2Pos;

    Vector3 lowPoint = new Vector3(9999999, 99999999999, 9999999999);


    GameObject currentPlatform;

    int randomPlatform;
    int randomEnemy;
    int decideDirection;
    int randomNumOneOrTwo;

    float randomX;
    float randomY;
    float randomMovingX;
    float randomMovingY;
    float randomCoinPlatformPosX;
    float randomCoinPlatformPosY;
    float randomEnemyPosX;




    float currentLength;

    float previousLength;



    bool movingPlatform;

    bool wasPreviousMoving = false;

    bool slantedPlatform = false;



    private void Awake()
    {
        //print(platformMes.GetComponent<BoxCollider2D>().bounds.size);
        numberOfPlatforms = 10;
        platformsOnScreen = new Platform[numberOfPlatforms];
        decideDirection = Random.Range(0, 2);
        currentPlatform = gameObject;
        MakeMorePlatforms();
        MakeFinalPlatform();


    }

    private void FixedUpdate()
    {
        for (int i = 0; i < platformsOnScreen.Length; i++)
        {
            if (platformsOnScreen[i].Number == 0)
            {
                platformsOnScreen[i].Plat.transform.position = Vector3.Lerp(platformsOnScreen[i].Start, platformsOnScreen[i].End, Mathf.PingPong(Time.time * 0.5f, 1.0f));

            }


        }

        FellOff();
    }

    void MakeMorePlatforms()
    {

        for (int i = 0; i < numberOfPlatforms; i++)
        {
            randomNumOneOrTwo = Random.Range(0, 5);
            randomMovingX = Random.Range(3f, 10f);
            randomMovingY = Random.Range(0f, 5f);
            randomX = Random.Range(3f, 12f);
            randomY = Random.Range(-6f, 6f);
            CheckCurrent();
            previousLength = currentLength;
            if (movingPlatform)
            {
                randomPlatform = Random.Range(0, 5);

            }
            else if (slantedPlatform)
            {
                randomPlatform = 3;
            }

            else
            {
                randomPlatform = Random.Range(0, platforms.Length);

            }
            CheckCurrent();
            if (!wasPreviousMoving)
            {
                currentPos = currentPlatform.transform.position;

            }
            if (decideDirection == 0)
            {
                if (!slantedPlatform)
                {
                    nextPos.x = currentPos.x - (currentLength / 2) - (previousLength / 2) - randomX;
                    nextPos.y = currentPos.y - randomY;
                    nextPos.z = transform.position.z;
                }
                if (randomPlatform == 8)
                {
                    nextPos.x = currentPos.x - (currentLength / 2) - (previousLength / 2) + 7.5f;
                    nextPos.y = currentPos.y - (currentLength / 2) + 1f;
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.Euler(new Vector3(0, 0, 45)));
                    CheckLowPoint();
                    slantedPlatform = true;
                    nextPos.x -= 30f;
                    nextPos.y -= 20f;
                    currentPlatform.tag = "SlantGroundLeft";
                    coinPlatformPos.x = nextPos.x - 2f;
                    coinPlatformPos.y = nextPos.y + 5f;
                    coinPlatformPos.z = nextPos.z;
                    Instantiate(platformCoin, coinPlatformPos, Quaternion.identity);
                    coinPos.x = coinPlatformPos.x - 3.8f;
                    coinPos.y = coinPlatformPos.y + 9f;
                    coinPos.z = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        Instantiate(coin, coinPos, Quaternion.identity);
                        coinPos.x += 1.5f;

                    }
                    coinPos.x = coinPlatformPos.x - 3.8f;
                    coinPos.y = coinPlatformPos.y + 11f;
                    coinPos.z = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        Instantiate(coin, coinPos, Quaternion.identity);
                        coinPos.x += 1.5f;

                    }

                }
                else if (!slantedPlatform && !movingPlatform)
                {
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.identity);
                    CheckLowPoint();
                    MakeCoinPlatforms();
                    slantedPlatform = false;
                    currentPlatform.tag = "Ground";



                }
                else
                {
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.identity);
                    CheckLowPoint();
                    slantedPlatform = false;

                }
                if (movingPlatform)
                {

                    tempEnd.x = nextPos.x - randomMovingX;
                    tempEnd.y = nextPos.y - randomMovingY;
                    tempEnd.z = nextPos.z;
                    platformsOnScreen[i] = new Platform(tempEnd, nextPos, 0, currentPlatform);
                    wasPreviousMoving = true;
                    currentPos = tempEnd;



                }
                else
                {
                    wasPreviousMoving = false;
                    platformsOnScreen[i] = new Platform(nextPos, nextPos, 1, currentPlatform);


                }

            }
            else if (decideDirection == 1)
            {
                if (!slantedPlatform)
                {
                    nextPos.x = currentPos.x + (currentLength / 2) + (previousLength / 2) + randomX;
                    nextPos.y = currentPos.y + randomY;
                    nextPos.z = transform.position.z;
                }
                if (randomPlatform == 8)
                {
                    nextPos.x = currentPos.x + (currentLength / 2) + (previousLength / 2) - 7.7f;
                    nextPos.y = currentPos.y - (currentLength / 2) + 1f;
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.Euler(new Vector3(0, 0, -45)));
                    CheckLowPoint();
                    slantedPlatform = true;
                    nextPos.x += 30f;
                    nextPos.y -=20f;
                    currentPlatform.tag = "SlantGroundRight";
                    coinPlatformPos.x = nextPos.x;
                    coinPlatformPos.y = nextPos.y + 5f;
                    coinPlatformPos.z = nextPos.z;
                    Instantiate(platformCoin, coinPlatformPos, Quaternion.identity);
                    coinPos.x = coinPlatformPos.x - 3.8f;
                    coinPos.y = coinPlatformPos.y + 9f;
                    coinPos.z = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        Instantiate(coin, coinPos, Quaternion.identity);
                        coinPos.x += 1.5f;

                    }
                    coinPos.x = coinPlatformPos.x - 3.8f;
                    coinPos.y = coinPlatformPos.y + 11f;
                    coinPos.z = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        Instantiate(coin, coinPos, Quaternion.identity);
                        coinPos.x += 1.5f;

                    }

                }
                else if (!slantedPlatform && !movingPlatform)
                {
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.identity);
                    CheckLowPoint();
                    MakeCoinPlatforms();
                    slantedPlatform = false;
                    currentPlatform.tag = "Ground";



                }
                else
                {
                    currentPlatform = Instantiate(platforms[randomPlatform], nextPos, Quaternion.identity);
                    CheckLowPoint();
                    slantedPlatform = false;

                }
                if (movingPlatform)
                {

                    tempEnd.x = nextPos.x + randomMovingX;
                    tempEnd.y = nextPos.y + randomMovingY;
                    tempEnd.z = nextPos.z;
                    platformsOnScreen[i] = new Platform(tempEnd, nextPos, 0, currentPlatform);
                    wasPreviousMoving = true;
                    currentPos = tempEnd;




                }
                else
                {
                    wasPreviousMoving = false;
                    platformsOnScreen[i] = new Platform(nextPos, nextPos, 1, currentPlatform);


                }
            }

        }


    }

    void FellOff()
    {
        if (melt.transform.position.y < (lowPoint.y - 20f))
        {
            GameManager.instance.GameOver();
            Destroy(melt);
        }
    }

    void MakeFinalPlatform()
    {
        if (randomPlatform == 8)
        {
            if (decideDirection == 0)
            {
                nextPos.x -= 5f;

            }
            else
            {
                nextPos.x += 5f;

            }
            nextPos.y -= 5f;
            Instantiate(platforms[3], nextPos, Quaternion.identity);
            CheckLowPoint();

        }
        if (decideDirection == 0)
        {
            finalPos.x = nextPos.x - 4f - (currentLength / 2);
        }
        else if (decideDirection == 1)
        {
            finalPos.x = nextPos.x + 4f + (currentLength / 2);
        }
        finalPos.y = nextPos.y + 6f;
        finalPos.z = nextPos.z;
        Instantiate(finalPlatform, finalPos, Quaternion.identity);


    }

    void CheckLowPoint()
    {
        if (nextPos.y <= lowPoint.y)
        {
            lowPoint = nextPos;
        }

    }

    void MakeCoinPlatforms()
    {
        if (randomPlatform == 3 || randomPlatform == 4)
        {
            if (randomNumOneOrTwo == 0 || randomNumOneOrTwo == 3)
            {
                randomCoinPlatformPosX = Random.Range(-15f, 15f);
                randomCoinPlatformPosY = Random.Range(6.5f, 8f);
                coinPlatformPos.x = nextPos.x + randomCoinPlatformPosX;
                coinPlatformPos.y = nextPos.y + randomCoinPlatformPosY;
                coinPlatformPos.z = nextPos.z;
                Instantiate(platformCoin, coinPlatformPos, Quaternion.identity);
                coinPos.x = coinPlatformPos.x - 3.8f;
                coinPos.y = coinPlatformPos.y + 9f;
                coinPos.z = 0;
                for (int j = 0; j < 5; j++)
                {
                    Instantiate(coin, coinPos, Quaternion.identity);
                    coinPos.x += 1.5f;

                }
                coinPos.x = coinPlatformPos.x - 3.8f;
                coinPos.y = coinPlatformPos.y + 11f;
                coinPos.z = 0;
                for (int j = 0; j < 5; j++)
                {
                    Instantiate(coin, coinPos, Quaternion.identity);
                    coinPos.x += 1.5f;

                }


            }
            else if (randomNumOneOrTwo == 1 || randomNumOneOrTwo == 2 || randomNumOneOrTwo == 4)
            {


                randomEnemy = Random.Range(0, 2);
                randomEnemyPosX = Random.Range(-5f, 5f);
                enemyPos.x = nextPos.x - randomEnemyPosX;
                enemyPos.y = nextPos.y + 5f;
                Instantiate(enemies[randomEnemy], enemyPos, Quaternion.identity);
                if (randomEnemy == 1)
                {
                    enemyTrigger1Pos.x = enemyPos.x - 8f;
                    enemyTrigger2Pos.x = enemyPos.x + 8f;
                }
                else if (randomEnemy == 0)
                {
                    enemyTrigger1Pos.x = enemyPos.x - 5f;
                    enemyTrigger2Pos.x = enemyPos.x + 5f;
                }

                enemyTrigger1Pos.y = enemyPos.y - 3f;
                enemyTrigger2Pos.y = enemyPos.y - 3f;
                Instantiate(trigger1, enemyTrigger1Pos, Quaternion.identity);
                Instantiate(trigger2, enemyTrigger2Pos, Quaternion.identity);







            }


        }


    }



    void CheckCurrent()
    {
        if (randomPlatform == 0)
        {
            currentLength = 3.1f;
            movingPlatform = false;

        }
        else if (randomPlatform == 1)
        {
            currentLength = 6.8f;
            movingPlatform = false;


        }
        else if (randomPlatform == 2)
        {
            currentLength = 15.0f;
            movingPlatform = false;


        }
        else if (randomPlatform == 3)
        {
            currentLength = 39.2f;
            movingPlatform = false;
            randomNumOneOrTwo = Random.Range(0, 2);

            if (randomNumOneOrTwo == 0)
            {

            } 


        }
        else if (randomPlatform == 4)
        {
            currentLength = 59.2f;
            movingPlatform = false;


        }
        else if (randomPlatform == 5) { 

                currentLength = 3.1f;
                movingPlatform = true;
        }
        else if (randomPlatform == 6)
        {

                currentLength = 6.8f;
                movingPlatform = true;

        }
        else if (randomPlatform == 7)
        { 
                currentLength = 15.0f;
                movingPlatform = true;
            
        }
        else if (randomPlatform == 8)
        {
            currentLength = 39.2f;
            movingPlatform = false;
        }
        else if (randomPlatform == 9)
        {
            currentLength = 8.7f;
            movingPlatform = false;
        }
        else if (randomPlatform == 10)
        {
            currentLength = 5.8f;
            movingPlatform = false;
        }

        else if (randomPlatform == 11)
        {
            currentLength = 1.4f;
            movingPlatform = false;
        }

        else if (randomPlatform == 12)
        {
            currentLength = 3.3f;
            movingPlatform = false;
        }



    }
}
