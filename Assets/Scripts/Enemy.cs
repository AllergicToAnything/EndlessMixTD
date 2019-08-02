using System.Collections;
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

    public GameObject bullet;

    void Start()
    {
        isStunned = false;
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();
        curSpeed = GetComponent<NavMeshAgent>().speed;
        hp += hp * lvlManager.curLevel;
    }

    public void Stunned(float stunDuration)
    {
        stunCD = stunDuration;

        if (isStunned == true)
        {
            
            GetComponent<NavMeshAgent>().speed = 0f;            
            
        }
        if (isStunned == false)
        {
            GetComponent<NavMeshAgent>().speed = curSpeed;
            
        }
    }

    //public GameObject bullet;
    
  
    void Update()
    {
        CheckStunned();        

        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        velocity = agent.velocity.magnitude / agent.speed;
        agent.SetDestination(target.position);

        Invoke("CheckObstacles",2f);
      
    }

    public void CheckObstacles()
    {
        RaycastHit hit;

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
                Stunned(bullet.detector.GetComponent<Tower>().miniStunDur);
            }
            
            other.gameObject.SetActive(false);   
        }
    }

    public void CheckStunned()
    {
        if (stunCD > 0) { stunCD -= Time.deltaTime; }
        if (stunCD <= 0) { stunCD = 0; }
        if (stunCD == 0)
        { isStunned = false; }
        if (isStunned == true)
        {
           GetComponent<NavMeshAgent>().speed = 0f;
        }
        if (isStunned == false)
        {
            GetComponent<NavMeshAgent>().speed = curSpeed;
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
