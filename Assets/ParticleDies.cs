using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDies : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Die", 1f);
    }

    void Die()
    {
        Destroy(gameObject);
    }
}
