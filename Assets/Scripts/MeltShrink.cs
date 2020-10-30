using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeltShrink : MonoBehaviour
{

    public GameObject melt;

    private void FixedUpdate()
    {
        melt.transform.localScale -= new Vector3(0.0005f, 0.0005f, 0.0005f);

        if (melt.transform.localScale.x <= 0f || melt.transform.localScale.y <= 0f)
        {
            Destroy(gameObject);
        }


        
    }
}
