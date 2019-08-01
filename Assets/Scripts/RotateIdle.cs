using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateIdle : MonoBehaviour
{
    public float speed = 1f;
    public float directionX = 0f;
    public float directionY = -1f;    
    public float directionZ = 0f;


    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(speed * directionX * Time.deltaTime, speed*directionY*Time.deltaTime, speed * directionZ * Time.deltaTime);
    }
}
