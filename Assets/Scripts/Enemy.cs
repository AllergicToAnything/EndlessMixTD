using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : Unit
{
    NavMeshAgent agent;
    public Transform target;
    LineRenderer lineRenderer;
    public GameObject hitted;
    public LevelManager lvlManager;
    public float velocity;
    public float rayLength = .5f;
    public Spawner spawner;
    RaycastHit hit;

    public bool isStunned = false;
    public bool isSlowByIce = false;
    public bool isSlowByPoison = false;
    public bool isBurned = false;
    public bool isBurning = false;
    public bool isPoisoned = false;
    public bool isPoisoning = false;

    public float curSpeed;

    float stunCD;
    float stunSpeed = 1;

    float slowByIceCD;
    float icySlowSpeed = 1f;    

    float fireDPS;
    float fireDPSCD = 999999999f;
    float fireDPSInterval = .5f;
    int fireDPSCount;
    int fireDPSLimitCount;

    float poisonSlowSpeed = 1f;
    float poisonDPS;
    int poisonDPSCount;
    int poisonDPSLimitCount;
    float poisonDPSCD = 999999999f;
    float poisonDPSInterval = .8f;
    float slowByPoisonCD;

    float damageTaken;
    float burnDamage;
    float poisonDamage;

    public GameObject enemyBullet;
    public Bullet bullet;

    public Detector curDetector;

    int killCount;

    public float atkCD;
    float atkSpd = 3f;

    bool check = true;
    
    void Start()
    {
        isStunned = false;
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        agent.speed = lvlManager.enemySpeed;
        curSpeed = GetComponent<NavMeshAgent>().speed;
        Invoke("Hp",0.5f);
        atkCD = atkSpd;
    }

    void Move()
    {
        agent.speed = curSpeed * stunSpeed*icySlowSpeed*poisonSlowSpeed;
    }

 

    public void Hp()
    {
        if(lvlManager.curLevel < 30)
        {
            hp = hp + 1 + ((lvlManager.curLevel * lvlManager.curLevel) / 1.4f);
            if (lvlManager.curLevel > 14)
            {
                hp = hp + 1 + ((lvlManager.curLevel * lvlManager.curLevel) /1.2f);
            }
        }
        else
        {
            hp =lvlManager.curLevel * lvlManager.curLevel;
        }
        
    }

    //public GameObject bullet;
    
    
  
    void Update()
    {
        agent.SetDestination(target.position);
        CheckStatus();    
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        velocity = agent.velocity.magnitude / agent.speed;
        Move();
        Invoke("CheckObstacles", 2f);
        if (atkCD > 0) { atkCD -= Time.deltaTime; }
        if (atkCD < 0) { atkCD = 0; }
    }

    public void CheckObstacles()
    {
        

        Ray ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (this.velocity == 0)
            {
                if (!isStunned)
                {
                    if (atkCD == 0)
                    {
                        Invoke("CheckStun", 1f);
                    }
                }
            }
        }
    }

    public void CheckStun()
    {
        if(this.velocity == 0)
        {
            if (!isStunned)
            {
                StartCoroutine(CheckStun2());
            }
        }
    }

    IEnumerator Hitted()
    {
        
        yield return new WaitForSeconds(.03f);
        hitted.gameObject.SetActive(false);
    }

 



    IEnumerator CheckStun2()
    {
        yield return new WaitForSeconds(1f);
        if (this.velocity == 0)
        {
            print("Obsticle Detected");
            Instantiate(enemyBullet, this.transform.position, this.transform.rotation); 
            atkCD = atkSpd; 
            
           /* if (hit.collider.gameObject.tag == "Tower")
            {
                Platform curPlatform = hit.collider.GetComponent<Tower>().platform;
                curPlatform.towerStates = State.I;
                curPlatform.elements = Element.None;
                curPlatform.tower.thisElement = Element.None;

                hit.collider.gameObject.SetActive(false);
            }*/

        }
    }



    public new void TakeDamage(float damageAmount)
    {
        if (hp > 0)
        {
            hp -= damageAmount;
            hitted.gameObject.SetActive(true);
            StartCoroutine(Hitted());
            if (hp <= 0)
            {
                check = false;               
                if (check == false)
                {
                    spawner.killCount++; spawner.debugCount++;
                    curDetector.killCount++;
                    Destroy(this.gameObject);
                }
               
            } // Die
        }
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            bullet = other.GetComponent<Bullet>();
            curDetector = bullet.detector;
            
            damageTaken = bullet.bulletDamage;
            TakeDamage(damageTaken);
            if (bullet.thisElement == Element.Fire)
            {
                    isBurned = true;
                    if (isBurned == true)
                    {
                        fireDPSCount = 1;
                        fireDPSLimitCount = bullet.detector.GetComponent<Tower>().fireDPSLimitCount;
                        fireDPS = bullet.detector.GetComponent<Tower>().fireDPS;
                        burnDamage = fireDPS;
                        fireDPSCD = fireDPSInterval;
                    }
            
            }
            if (bullet.thisElement == Element.Electric)
            {
                isStunned = true;
                if (isStunned == true)
                {
                    stunSpeed = 0f;
                    stunCD = bullet.detector.GetComponent<Tower>().miniStunDur;
                }
            }
            if (bullet.thisElement == Element.Ice)
            {
                isSlowByIce = true;                
                if (isSlowByIce == true)
                {
                    slowByIceCD = bullet.detector.GetComponent<Tower>().icySlowDur;
                    icySlowSpeed = bullet.detector.GetComponent<Tower>().icySlowSpeed;
                    agent.speed *= icySlowSpeed;
                }
                                
            }
                if (bullet.thisElement == Element.Poison)
                {
                    isSlowByPoison = true;
                    isPoisoned = true;

                    if (isPoisoned == true)
                    {
                        poisonDPSCount = 1;
                        poisonDPSLimitCount = bullet.detector.GetComponent<Tower>().poisonDPSLimitCount;
                        poisonDPS = bullet.detector.GetComponent<Tower>().poisonDPS;
                        poisonDamage = poisonDPS;
                        poisonDPSCD = poisonDPSInterval;
                    }

                    if (isSlowByPoison == true)
                    {
                        slowByPoisonCD = bullet.detector.GetComponent<Tower>().poisonSlowDur;
                        poisonSlowSpeed = bullet.detector.GetComponent<Tower>().poisonSlowSpeed;
                        agent.speed *= poisonSlowSpeed;

                    }

                }
            
            
        }
    }

    public void CheckStatus()
    {
        // Stun
        if (stunCD > 0) { stunCD -= Time.deltaTime; }
        if (stunCD <= 0) { stunCD = 0; }
        if (stunCD == 0)
        { isStunned = false; }
        
        if (isStunned == false)
        {
            stunSpeed = 1f;
        }
        if (isSlowByPoison == false)
        {
            poisonSlowSpeed = 1f;
        }
        if (isSlowByIce == false)
        {
            icySlowSpeed = 1f;
        }

        // Slow by Ice
        if (slowByIceCD > 0) { slowByIceCD -= Time.deltaTime; }
        if (slowByIceCD <= 0) { slowByIceCD = 0; }
        if (slowByIceCD == 0) { isSlowByIce = false; }

        if (fireDPSCount < fireDPSLimitCount && fireDPSCD > 0) { fireDPSCD -= Time.deltaTime; }
        if (fireDPSCD <= 0) { fireDPSCD = 0; }
        if (fireDPSCD == 0)
        {
            fireDPSCount++;
            FireDPSCounter();
            fireDPSCD = fireDPSInterval;
        }

        if (poisonDPSCount < poisonDPSLimitCount && poisonDPSCD > 0) { poisonDPSCD -= Time.deltaTime; }
        if (poisonDPSCD <= 0) { poisonDPSCD = 0; }
        if (poisonDPSCD == 0)
        {
            poisonDPSCount++;
            PoisonDPSCounter();
            poisonDPSCD = poisonDPSInterval;
        }


    }

    void PoisonDPSCounter()
    {
        isPoisoning = true;
        if (isPoisoning == true)
        {
            TakeDamage(poisonDamage);
            print("Poison " + poisonDPSCount.ToString());
            hitted.gameObject.SetActive(true);
            StartCoroutine(Hitted());
            isPoisoning = false;
            if (isSlowByPoison == true)
            {
                slowByPoisonCD = bullet.detector.GetComponent<Tower>().poisonSlowDur;
                poisonSlowSpeed = .7f;
                agent.speed *= poisonSlowSpeed;

            }
            if (poisonDPSCount >= poisonDPSLimitCount) { isPoisoned = false; isPoisoning = false; }
            // every 1 s will take damage until a certain count
        }


    }


    void FireDPSCounter()
    {
        isBurning = true;
        if (isBurning == true)
        {
            TakeDamage(burnDamage);
            print("Burned " + fireDPSCount.ToString());
            hitted.gameObject.SetActive(true);
            StartCoroutine(Hitted());
            isBurning = false;
            if (fireDPSCount >= fireDPSLimitCount) { isBurned = false; isBurning = false;}
            // every 1 s will take damage until a certain count
        }        
        
        
    }

    /*
      void GetPathLine()
      {
      if (agent)
        {
            if (agent.hasPath)
            {
                // Create a temporary array to modify;
                Vector3[] newCorners = agent.path.corners;

                lineRenderer.positionCount = agent.path.corners.Length; // set the number of positions of the LineRenderer

                for (int i = 0; i < agent.path.corners.Length-1; i++) //Push our points up by 1 unit
                {
                    newCorners[i] += new Vector3(0,2,0);
                  //  Debug.DrawLine(agent.path.corners[i], agent.path.corners[i+1],Color.magenta);
                }
                
                lineRenderer.SetPositions(agent.path.corners);
            }
        }
      }
     
     */ //For Render Path Line

    // Destroy this when Prepare Phase

    // Raycast a ray to detect the tower, when the velocity = 0 it will attack an obsticle to pass through the tower if it's not stunned

   


}
