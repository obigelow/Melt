using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltmanGrow : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag == "Coin")
        {
            gameObject.transform.localScale += new Vector3(0.5f, 0.5f, 0.5f);

        }
        
    }
}
