using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Unit
{
    NavMeshAgent agent;
    public Transform target;
    LineRenderer lineRenderer;
    public bool isStunned;
    public LevelManager lvlManager;
    public float velocity;
    public float rayLength = .5f;

    //public GameObject bullet;
    void Start()
    {
        isStunned = false;
        agent = GetComponent<NavMeshAgent>();
        lineRenderer = GetComponent<LineRenderer>();

        
    }

    void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * rayLength, Color.red);
        velocity = agent.velocity.magnitude / agent.speed;
        agent.SetDestination(target.position);
        if(velocity == 0)
        {
            RaycastHit hit;
            Ray ray = new Ray(transform.position, Vector3.forward);
            if(Physics.Raycast(ray,out hit, rayLength))
            {
                print("hi");
            }
        }
      
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            TakeDamage(collision.gameObject.GetComponent<Bullet>().bulletDamage);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            TakeDamage(other.gameObject.GetComponent<Bullet>().bulletDamage);
            other.gameObject.SetActive(false);

        }
    }

    

    /*  private void OnCollisionEnter(Collision collision)
      {
          if(collision.gameObject.tag == "Bullet")
          {
              print("Bulleted");
              Destroy(collision.gameObject);
          }
      }*/


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
