using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Detector detector;
    public float bulletSpeed = 10f;
    Transform target;
    public float bulletDamage;
    public Element thisElement;
    MeshRenderer mr;
    public Rigidbody rb;
    GameObject[] particles; // 0 = Fire 1 = Ice 2 = Electric 3 = Poison
   


    public Spawner spawner;

    private void OnEnable()
    {
        Invoke("Delay", 0.001f);
        rb = this.GetComponent<Rigidbody>();
        mr = this.GetComponent<MeshRenderer>();
        mr.enabled = false;
    }

    private void Update()
    {
        if (target)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, bulletSpeed * Time.deltaTime);            
        }
        if (rb.velocity.y != 0) { mr.enabled = true; }
        if (rb.velocity.x != 0) { mr.enabled = true; }
        if (rb.velocity.z != 0) { mr.enabled = true; }

    }

   


    void Delay()
    {
        if (detector.invader.Count > 0)
        {
            if (detector.invader[0] == null)
            {
                detector.invader.RemoveAt(0);
                
                foreach (GameObject r in spawner.bullet)
                {
                    Destroy(r.gameObject);
                    
                }
                
            }
            else
            {
                target = detector.invader[0].transform;
                bulletDamage = detector.damage;
            }
        }
    }

   
    

   public void AllElement()
    {
        if(detector.GetComponent<Tower>().thisElement == Element.Fire )
        {
            thisElement = Element.Fire;
        }
        if (detector.GetComponent<Tower>().thisElement == Element.Ice)
        {
            thisElement = Element.Ice;
            
        }
        if (detector.GetComponent<Tower>().thisElement == Element.Electric)
        {
            thisElement = Element.Electric;
            
        }
        if (detector.GetComponent<Tower>().thisElement == Element.Poison)
        {
            thisElement = Element.Poison;
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
    
        if(other.tag == "Invader")
        {
            mr.enabled = false;

            Instantiate(particles[0], transform.position, transform.rotation);
            Instantiate(particles[1], transform.position, transform.rotation);
            Instantiate(particles[2], transform.position, transform.rotation);
            Instantiate(particles[3], transform.position, transform.rotation);

            Destroy(this.gameObject);
        }
    }



}
