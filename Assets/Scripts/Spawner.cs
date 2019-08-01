using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> allInvaders = new List<GameObject>();
    public LevelManager lvlManager;
    public BuildingTowerManager manager;
    public GameObject invader;
    public float cd = 3;
    public float spawnRate = 3;
    public int spawnLimitPerLevel = 20;
    public int spawnCount = 0;    
    public int addSpawnLimitEvery = 5;
    public int slcd;
    GameObject go;
    public int killCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        slcd = addSpawnLimitEvery -1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Start Spawning
    public void StartSpawning()
    {
        if (cd > 0)
        {
           cd -= Time.deltaTime;
        }
        if (cd < 0) { cd = 0; }
        if(cd<=0 && spawnCount < spawnLimitPerLevel )
        {
            go = Instantiate(invader.gameObject, this.transform.position, this.transform.rotation);
            spawnCount++;
            cd = spawnRate;
            allInvaders.Add(go);
            foreach (GameObject go in allInvaders)
            {
                if (go!=null)
                {
                    go.GetComponent<Enemy>().hp += go.GetComponent<Enemy>().hp * lvlManager.curLevel;
                    go.GetComponent<Enemy>().spawner = this;
                }                
            }
        }
        if (allInvaders.Count > spawnLimitPerLevel - 1)
        {
            foreach (GameObject go in allInvaders)
            {
                if (!go)
                {
                    StartCoroutine(LevelClear());
                }
            }
        } 
    }

    IEnumerator LevelClear()
    {
        yield return new WaitForSeconds(5);
        if (allInvaders.Count > spawnLimitPerLevel - 1 && lvlManager.curPhase==Phase.Battle)
        {
            
            lvlManager.curLevel++;
            spawnCount = 0;
            allInvaders.Clear();
            
            if(killCount >= spawnLimitPerLevel)
            {
                manager.gold++;
            }

            if (lvlManager.curLevel > 34)
            {
                manager.gold += (lvlManager.curLevel + 24);
            }
            else if (lvlManager.curLevel > 29)
            {
                manager.gold += (lvlManager.curLevel + 12);
            }
            else if (lvlManager.curLevel > 24)
            {
                manager.gold += (lvlManager.curLevel + 6);
            }
            else if(lvlManager.curLevel > 19)
            {
                manager.gold += (lvlManager.curLevel+4);
            }
            else if( lvlManager.curLevel > 14)
            {
                manager.gold += (lvlManager.curLevel+2);
            }
            else if (lvlManager.curLevel == 10)
            {
                manager.gold += 10;
            }
            else if (lvlManager.curLevel == 5)
            {
                manager.gold += 5;
            }
            else
            {
                manager.gold++;
            }
            
            slcd--;
            if (slcd == 0)
            {
                spawnLimitPerLevel++;
                slcd = addSpawnLimitEvery;
                
            }

            
        }
        if (allInvaders.Count >= (spawnLimitPerLevel - 1))
        {
            lvlManager.curPhase = Phase.Prepare;
        }
    }
}
