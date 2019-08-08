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
    public int addSpawnLimitEveryLevel = 5;
    public int slcd;
    GameObject go;
    public int killCount = 0;

    public int debugCount = 0;
    public bool countLock = false;

    // Start is called before the first frame update
    void Start()
    {
        slcd = addSpawnLimitEveryLevel -lvlManager.curLevel;
        spawnCount = 0;

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
                    go.GetComponent<Enemy>().spawner = this;
                    go.GetComponent<Enemy>().lvlManager = this.lvlManager;
                    
                }                
            }
        }
        if (spawnCount == spawnLimitPerLevel && lvlManager.curPhase == Phase.Battle)
        {
            foreach (GameObject go in allInvaders)
            {
                
                if (debugCount==spawnLimitPerLevel)
                {
                    StartCoroutine(LevelClear());
                }
                
            }
        } 
    }

    IEnumerator LevelClear()
    {
        yield return new WaitForSeconds(5);
        if (lvlManager.curPhase == Phase.Battle)
        {
            if (killCount >= spawnLimitPerLevel)
            {
                manager.gold++;
            }

            if (lvlManager.curLevel > 39)
            {
                manager.gold += (lvlManager.curLevel + 1);
            }
            else if (lvlManager.curLevel > 29)
            {
                manager.gold += 3;
            }
            else if (lvlManager.curLevel > 19)
            {
                manager.gold += 2;
            }
            else if (lvlManager.curLevel == 10)
            {
                manager.gold += 1;
            }
            else
            {
                manager.gold++;
            }

            slcd--;
            if (slcd == 0)
            {
                spawnLimitPerLevel++;
                slcd = addSpawnLimitEveryLevel;
            }
            allInvaders = new List<GameObject>();
            spawnCount = 0;
            lvlManager.curLevel++;
            killCount = 0;
            debugCount = 0;
            print("Prepare Phase");
            lvlManager.curPhase = Phase.Prepare;
        }

    }
}
