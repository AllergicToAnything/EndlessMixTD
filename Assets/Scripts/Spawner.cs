using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> allInvaders = new List<GameObject>();
    public LevelManager lvlManager;
    public GameObject invader;
    public float cd = 3;
    public float spawnRate = 3;
    public int spawnLimitPerLevel = 20;
    int spawnCount = 0;
    GameObject go;

    // Start is called before the first frame update
    void Start()
    {
        
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
        if(cd<=0 && spawnCount < spawnLimitPerLevel)
        {
            go = Instantiate(invader.gameObject, this.transform.position, this.transform.rotation);
            spawnCount += 1;
            cd = spawnRate;
            allInvaders.Add(go);
        }
        if (allInvaders.Count > spawnLimitPerLevel - 1)
        {
            foreach (GameObject go in allInvaders)
            {
                if(go == null)
                {
                    StartCoroutine(Clear());
                }
            }
        } 
    }

    IEnumerator Clear()
    {
        yield return new WaitForSeconds(5);
        if (allInvaders.Count > spawnLimitPerLevel - 1 && lvlManager.curPhase==Phase.Battle)
        { lvlManager.curPhase = Phase.Prepare; lvlManager.curLevel++; spawnCount = 0; allInvaders.Clear(); print("Cleared"); }
    }
}
