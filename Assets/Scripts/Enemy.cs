﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    NavMeshAgent agent;
    public Transform target;
    LineRenderer lineRenderer;

    public LevelManager lvlManager;
    public float velocity;
    public float rayLength = .5f;
    public Spawner spawner;
    public bool isStunned = false;
    public bool isSlowByIce = false;
    public bool isSlowByPoison = false;
    public float curSpeed;
    public float stunCD;
    public float slowByIceCD;
    public float slowByPoisonCD;
    RaycastHit hit;
    public float stunSpeed = 1;
    public float icySlowSpeed = 1f;
    public float poisonSlowSpeed = 1f;

    public GameObject bullet;

    void Start()
    {
        isStunned = false;
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        curSpeed = GetComponent<NavMeshAgent>().speed;
        Invoke("Hp",0.5f);
      
    }

    void Move()
    {
        agent.speed = curSpeed * stunSpeed*icySlowSpeed*poisonSlowSpeed;
    }

 

    public void Hp()
    {
        hp = 1+((lvlManager.curLevel * lvlManager.curLevel )/ 1.6f);
        Debug.Log($"lvlManager : {lvlManager.curLevel}");
    }

    //public GameObject bullet;
    
    
  
    void Update()
    {
        CheckStatus();        

        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        velocity = agent.velocity.magnitude / agent.speed;
        agent.SetDestination(target.position);
        Move();
        Invoke("CheckObstacles",2f);
      
    }

    public void CheckObstacles()
    {
        

        Ray ray = new Ray(transform.position, Vector3.forward);
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (this.velocity == 0)
            {
                if (this.isStunned == false)
                {
                    Invoke("CheckStun", 2f);
                }
            }
        }
    }

    public void CheckStun()
    {
        if(this.velocity == 0)
        {
            StartCoroutine(CheckStun2());
        }
    }

   

    IEnumerator CheckStun2()
    {
        yield return new WaitForSeconds(5f);
        if (this.velocity == 0&& this.isStunned == false)
        {
            print("Obsticle Detected");
            Instantiate(bullet, this.transform.position, this.transform.rotation);
            hit.collider.gameObject.SetActive(false);
        }
    }

    

    public new void TakeDamage(float damageAmount)
    {
        hp -= damageAmount;
        if (hp <= 0) { spawner.killCount++;  spawner.debugCount++; Destroy(this.gameObject); } // Die
    }

   

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            Bullet bullet = other.gameObject.GetComponent<Bullet>();
            if (bullet != null) { print(name + "\n" + " has received " + bullet.bulletDamage.ToString() + " damage.  "); print(name + "\n HP: " + hp.ToString());  }
            
            TakeDamage(bullet.bulletDamage);
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
                    icySlowSpeed = .5f;
                    agent.speed *= icySlowSpeed;
                    print("Invader speed : " + agent.speed.ToString());
                }
                                
            }
            if (bullet.thisElement == Element.Poison)
            {
                isSlowByPoison = true;                
                if (isSlowByPoison == true)
                {
                    slowByPoisonCD = bullet.detector.GetComponent<Tower>().poisonSlowDur;
                    poisonSlowSpeed = .7f;
                    agent.speed *= poisonSlowSpeed;
                    print("Invader speed : " + agent.speed.ToString());
                }
                
            }
            other.gameObject.SetActive(false);   
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
