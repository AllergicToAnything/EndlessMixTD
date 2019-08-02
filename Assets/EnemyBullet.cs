using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        this.transform.Translate(0, 0, 1 * Time.deltaTime);
    }
}
