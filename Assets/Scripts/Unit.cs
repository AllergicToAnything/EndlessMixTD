using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public float hp = 1;
    public float damage = 1;
  

   
    public void TakeDamage(float damageAmount) // Player and Asteroid take damage and destroy when hp = 0
    {
        hp -= damageAmount; 
        if (hp <= 0) { Destroy(this.gameObject); } // Die
    }
}
