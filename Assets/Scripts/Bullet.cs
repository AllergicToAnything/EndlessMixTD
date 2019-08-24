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
    public bool isDying = false;

    public FindParticles[] allChild;
    public Spawner spawner;

    private void OnEnable()
    {
        Invoke("Delay", 0.001f);
        rb = this.GetComponent<Rigidbody>();
        mr = this.GetComponent<MeshRenderer>();
        mr.enabled = false;
        allChild = GetComponentsInChildren<FindParticles>();
        foreach (FindParticles r in allChild)
        {
            r.gameObject.SetActive(false);
        }
        

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
        if (detector.GetComponent<Tower>().thisElement == Element.Electric)
        {
            thisElement = Element.Electric;
        }
        if (detector.GetComponent<Tower>().thisElement == Element.Ice)
        {
            thisElement = Element.Ice;
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
            
            foreach (FindParticles r in allChild)
            {
                r.gameObject.SetActive(true);
                StartCoroutine(TurnOffParticles());
            }
            Destroy(this.gameObject);
        }
    }

    IEnumerator TurnOffParticles()
    {
        yield return new WaitForSeconds(.8f);
        foreach (FindParticles r in allChild)
        {
            r.gameObject.SetActive(false);
        }
    }

    /*
    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.GetComponent<Enemy>().hp > 0)
        {

                if (collision.collider.GetComponent<Enemy>().hp <= 0)
                {
                    detector.killCount++;
                isDying = true;
                }
           
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.GetComponent<Enemy>().hp > 0)
        {

            if (collision.collider.GetComponent<Enemy>().hp <= 0)
            {
                detector.killCount++;
                isDying = true;
            }

        }
    }*/

}
