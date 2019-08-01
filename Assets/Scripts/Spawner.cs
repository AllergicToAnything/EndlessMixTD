using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public LevelManager lvlManager;
    public GameObject invader;
    public int cd = 120;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartSpawning();
    }

    // Wait for seconds
    
    // Start Spawning
    public void StartSpawning()
    {
        if (lvlManager.curPhase == Phase.Battle)
        {
            cd--;
            if(cd<=0)
            {
                Instantiate(invader.gameObject, this.transform.position, this.transform.rotation);
                cd = 120;

            }
        }

            
        
    }
}
