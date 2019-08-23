using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    public List<GameObject> allInvaders = new List<GameObject>();
    public LevelManager lvlManager;
    public BuildingTowerManager manager;
    public GameObject invader;
    public float cd = 3;
    public float spawnRate = 3;
    public float spawnLimitPerLevel = 20;
    public float spawnCount = 0;    
    public float addSpawnLimitEveryLevel = 5;
    public float slcd;
    GameObject go;
    public int killCount = 0;
    public GameObject allClear;

    public int debugCount = 0;
    public bool countLock = false;

    public GameObject[] bullet;

    // public GameObject allTowers;
    // Start is called before the first frame update
    void Start()
    {
        slcd = addSpawnLimitEveryLevel -lvlManager.curLevel;
        spawnCount = 0;

    }

    // Update is called once per frame
    void Update()
    {
        if (lvlManager.curPhase == Phase.Prepare|| lvlManager.curPhase==Phase.Prebattle)
        {
            spawnCount = 0;
            killCount = 0;
            debugCount = 0;
        }
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
        if (spawnCount >= spawnLimitPerLevel && lvlManager.curPhase == Phase.Battle)
        {
            foreach (GameObject go in allInvaders)
            {
                
                if (debugCount>=spawnLimitPerLevel)
                {

                    bullet = GameObject.FindGameObjectsWithTag("Bullet");
                    StartCoroutine(LevelClear());
                }
                
            }
        } 

    
    }

    IEnumerator LevelClear()
    {
        if (killCount != spawnLimitPerLevel)
        {
            allClear.GetComponentInChildren<Text>().text = "Wave Done!";
        }

        allClear.gameObject.SetActive(true);


        yield return new WaitForSeconds(5f);
        allClear.gameObject.SetActive(false);
        if (lvlManager.curPhase == Phase.Battle)
        {
            int curLvl = Mathf.RoundToInt(lvlManager.curLevel);
            /*
            if (killCount >= spawnLimitPerLevel)
            {
                manager.gold++;
            }*/
            if (lvlManager.curLevel > 39)
            {
                manager.gold += (curLvl + 1);
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
            lvlManager.curLevel++;
            lvlManager.curPhase = Phase.Prepare;
            lvlManager.enemySpeed = 1+(lvlManager.curLevel / 11f);
            foreach(GameObject r in bullet)
            {
                Destroy(r.gameObject);
            }
           
        }

    }
}
