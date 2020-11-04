using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltShrink : MonoBehaviour
{

    private void FixedUpdate()
    {
        gameObject.transform.localScale -= new Vector3(0.001f, 0.001f, 0.001f);

        if (gameObject.transform.localScale.x <= 0.5f || gameObject.transform.localScale.y <= 0.5f)
        {
            GameManager.instance.GameOver();
            Destroy(gameObject);

        }



    }
}
