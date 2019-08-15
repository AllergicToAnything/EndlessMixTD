using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : Unit
{
    public Text HP;
    public GameObject loseScreen;

    private void Update()
    {
        HP.text = "Lives : " + hp.ToString();
    }

    public Spawner spawner;
    private void OnTriggerEnter(Collider other)
    {

        if(other.gameObject.CompareTag("Invader"))
        {
            spawner.debugCount++;
            hp -= 1;
            Destroy(other.gameObject);
            if(this.hp <= -1) { loseScreen.SetActive(true); Destroy(this.gameObject); }
            
            
        }
    }
    // Shield = HP
    // destroy this when hp = 1
}
