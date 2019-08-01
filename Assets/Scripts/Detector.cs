using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : Unit
{
    Tower tower;
    bool hasTarget;
    public List<GameObject> invader = new List<GameObject>();

    private void Update()
    {
       if(tower.target != null)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }
        
    }

    private void OnEnable()
    {
        tower = this.gameObject.GetComponent<Tower>();
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Invader")
        {
            
           invader.Add(other.gameObject);
            //tower.target = other.gameObject;
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
