using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HittedParticles : MonoBehaviour
{
    Bullet bullet;
    FindParticles particle;
    FindParticles[] fire;
    FindParticles[] ice;
    FindParticles[] electric;
    FindParticles[] poison;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Bullet"))
        {
            bullet = other.GetComponent<Bullet>();
            if (bullet)
            {
                if (bullet.thisElement == Element.Fire)
                {
                    Instantiate(fire[0]);
                    Instantiate(fire[1]);
                    Instantiate(fire[2]);
                }
                if (bullet.thisElement == Element.Ice)
                {
                    Instantiate(ice[0]);
                    Instantiate(ice[1]);
                    Instantiate(ice[2]);
                }
                if (bullet.thisElement == Element.Electric)
                {
                    Instantiate(electric[0]);
                    Instantiate(electric[1]);
                    Instantiate(fire[2]);
                }
                if (bullet.thisElement == Element.Poison)
                {
                    Instantiate(fire[0]);
                    Instantiate(fire[1]);
                    Instantiate(fire[2]);
                }
            }
        }
    }






}
