using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Tower : MonoBehaviour
{
    public MaterialChanger[] allChild; 
    public Platform platform;
    public BuildingTowerManager manager;
    public Element thisElement;
    public GameObject target;
    public GameObject curTarget;
    public Bullet bullet;
    public bool ableToAttack = false;
    public GameObject[] orb;
    public List<Bullet> allOrbs = new List<Bullet>();
    public float cd;
    public float attackCooldown = 1f;
    public Detector detector;
    public int towerLevel = 1;
    public int cost = 1;
    public float miniStunDur = .05f;
    public float icySlowDur = 1f;
    public float poisonSlowDur = 1.5f;
    public float fireDPS = .5f;
    public int fireDPSLimitCount;
   


    private void OnEnable()
    {
        thisElement = Element.None;
        allChild = GetComponentsInChildren<MaterialChanger>();
        detector = GetComponent<Detector>();
    }

    private void Update()
    {
        AttackCondition();
    }

    public void BuildTower()
    {
        if (platform.towerStates == State.B)
        {
            thisElement = platform.elements;
        }
    }

    void AttackCondition()
    {
        if (platform.towerStates == State.B)
        {
            ableToAttack = true;
        }
        if (platform.towerStates != State.B)
        {
            ableToAttack = false;
        }       
            Attack();       
    }
      
    // Attack within an area
    public void Attack()
    {
        if (allOrbs.Count > 20)
        {
            Destroy(allOrbs[0]);
            allOrbs.RemoveAt(0);
        }

        if (cd > 0)
        {
            cd -= Time.deltaTime;
        } // cd
        if(cd < 0) { cd = 0; } 
        if (ableToAttack == true && cd == 0)
        {
            if(attackCooldown <=0)
            { attackCooldown = .001f; }
            GameObject towerOrb;
            if (platform.elements == Element.Fire)
            {
                towerOrb = Instantiate(orb[0], this.transform.position, this.transform.rotation);
                allOrbs.Add(towerOrb.GetComponent<Bullet>());
            }
            if (platform.elements == Element.Electric)
            {
                towerOrb = Instantiate(orb[1], this.transform.position, this.transform.rotation);
                allOrbs.Add(towerOrb.GetComponent<Bullet>());
            }
            if (platform.elements == Element.Ice)
            {
                towerOrb = Instantiate(orb[2], this.transform.position, this.transform.rotation);
                allOrbs.Add(towerOrb.GetComponent<Bullet>());
            }
            if (platform.elements == Element.Poison)
            {
                towerOrb = Instantiate(orb[3], this.transform.position, this.transform.rotation);
                allOrbs.Add(towerOrb.GetComponent<Bullet>());
            }
            
            cd = attackCooldown;
            curTarget = target;
        } // Choose Attack Orb to Instantiate
        
            foreach (Bullet bullet in allOrbs)
            {
                bullet.detector = detector;
                
        }
    }

   public void LevelUP()
    {
        towerLevel++;
        cost= towerLevel*towerLevel;
    }



    // Upgrade to level 2


    // Upgrade to level 3


    // ELEMENTS
    // ELEMENT QUARKS

    // Ice (Attack speed slow and slow the opponent)
    // Fire (DPS attack fast)
    // Electric (Mini Stun)
    // Poison (Slow + DPS)

    // Venom = Poison + Ice (Very Slow + DPS)
    // Gasic = Poison + Fire (High DPS + Slow)
    // Sarin = Poison + Electric (Mini-stun like invoker's cold snap with damage )
    // Stunned = Electric + Ice (Stun)
    // Lit = Electric + Fire (High DPS + Mini-stun)
    // 
    //

    


}
