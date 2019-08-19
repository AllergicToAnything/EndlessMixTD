using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obsticles : MonoBehaviour
{
    Tower tower;
    float hp = 1;
    float initDamage;
    float damage;

    private void OnEnable()
    {
        tower = GetComponentInParent<Tower>();
        damage = tower.detector.damage;
        initDamage = tower.detector.initDamage;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EnemyBullet") { 
        TakeDamage(1);
    }
    }


    public void TakeDamage(float damageAmount)
    {
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
            foreach (MaterialChanger mc in tower.allChild)
            {
                mc.ChangeButton();
            }         


        } // Die
    }
}
