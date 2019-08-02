using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : Unit
{
    Tower tower;
    public List<GameObject> invader = new List<GameObject>();
    

    private void Update()
    {
       
        
    }

    private void OnEnable()
    {
        tower = this.gameObject.GetComponent<Tower>();
        hp = 1;
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Invader")
        {
           invader.Add(other.gameObject);
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

    public new void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0) { tower.platform.towerStates = State.I; tower.platform.elements = Element.None; tower.thisElement = Element.None; this.gameObject.SetActive(false);  } // Die
    }

    }
