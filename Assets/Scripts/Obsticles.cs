using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
    public Tower tower;
    float hp = 1;
    float initDamage;
    float damage;

    private void OnEnable()
    {
        tower = GetComponentInParent<Tower>();
        damage = tower.detector.damage;
        initDamage = tower.detector.initDamage;
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            TakeDamage(1);
        }
    }


    public void TakeDamage(float damageAmount)
    {
        Debug.Log("Teeheh");
        hp -= damageAmount;
        if (hp <= 0)
        {
            
            tower.detector.invader.Clear();
            tower.platform.elements = Element.None;
            tower.thisElement = Element.None;
            damage = initDamage;
            tower.attackCooldown = 1;
            tower.towerLevel = 0;
            tower.ableToAttack = false;
            tower.platform.towerStates = State.I;
            tower.platform.isOccupied = false;
            tower.icySlowSpeed = tower.initIcySlowSpeed;
            tower.poisonSlowSpeed = tower.initPoisonSlowSpeed;

            
            foreach (MaterialChanger mc in tower.allChild)
            {
                mc.TurnToInvisibleState();
            }         


        } // Die
    }
}
