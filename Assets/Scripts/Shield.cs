using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Unit
{
    public Spawner spawner;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Invader"))
        {

            spawner.debugCount++;
            hp -= 1;
            Destroy(other.gameObject);
            if(this.hp <= 1) { Destroy(this.gameObject); }
            
        }
    }
    // Shield = HP
    // destroy this when hp = 1
}
