using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Detector detector;
    public float bulletSpeed = 10f;
    Transform target;
    public float bulletDamage;

    private void OnEnable()
    {
        
        Invoke("Delay", 0.001f);
        
    }

    void Delay()
    {
        
        if (detector.invader.Count>0)
        {
            if (detector.invader[0])
            {
                target = detector.invader[0].transform;
                bulletDamage = detector.damage;
            }
        }

        
    }

    private void Update()
    {
        if (target) { transform.position = Vector3.Lerp(transform.position, target.transform.position, bulletSpeed * Time.deltaTime); }
    }
}
