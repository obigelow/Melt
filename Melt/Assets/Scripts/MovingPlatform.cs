using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    public Vector3 pos1;
    public Vector3 pos2;
    float moveSpeed = 0.2f;




    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * moveSpeed, 1.0f));
    }




}
