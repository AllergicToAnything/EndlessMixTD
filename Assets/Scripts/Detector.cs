using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : Unit
{
    public float initDamage;
    Tower tower;
    public List<GameObject> invader = new List<GameObject>();
   

    private void OnEnable()
    {
        tower = this.gameObject.GetComponent<Tower>();
        hp = 1;
        initDamage = damage;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Invader")
        {
           invader.Add(other.gameObject);
        }
        if(other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);
        }
        
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!this.invader[0])
        {
            invader.RemoveAt(0);
        }
    }

    void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.tag == "Invader")
        {
            invader.RemoveAt(0);
            
        }
    }

   

    }
