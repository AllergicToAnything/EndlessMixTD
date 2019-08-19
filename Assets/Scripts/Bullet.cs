using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Detector detector;
    public float bulletSpeed = 10f;
    Transform target;
    public float bulletDamage;
    public float decayTime = 1f;
    public Element thisElement;

    private void OnEnable()
    {
        Invoke("Delay", 0.005f);
        Invoke("Decay", decayTime );
    }

    void Delay()
    {
        if (detector.invader.Count > 0)
        {
            if (detector.invader[0] == null)
            {
                detector.invader.RemoveAt(0);
            }
            else
            {
                target = detector.invader[0].transform;
                bulletDamage = detector.damage;
            }
        }
    }

    void Decay()
    {
        GetComponent<MeshRenderer>().enabled = !GetComponent<MeshRenderer>().enabled;
        Invoke("DestroyThis", decayTime);
    }

    void DestroyThis()
    {
        Destroy(this.gameObject);
    }

    private void Update()
    {
        if (target) { transform.position = Vector3.Lerp(transform.position, target.transform.position, bulletSpeed * Time.deltaTime); }
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


}
