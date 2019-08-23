using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detector : Unit
{
    public float initDamage;
    Tower tower;
    public List<GameObject> invader = new List<GameObject>();
    Obsticles obsticles;
    public int killCount;
    int killCountTarget;
    public int lvlUPPerKill = 30;
    public Text killCountTxt;

    private void Update()
    {
        if (killCount == killCountTarget + lvlUPPerKill)
        {
            tower.towerLevel++;
            killCountTarget = killCount;
            tower.platform.TowerElementAttribute();
        }
        killCountTxt.text = "Killed : " + killCount.ToString();
    }

    private void OnEnable()
    {
        tower = this.gameObject.GetComponent<Tower>();
        obsticles = gameObject.GetComponentInChildren<Obsticles>();
        hp = 1;
        initDamage = damage;
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

   

    }
