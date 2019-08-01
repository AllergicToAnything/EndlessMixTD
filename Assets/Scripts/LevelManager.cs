using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Prepare,Prebattle,Battle }

public class LevelManager : MonoBehaviour
{
    public Phase curPhase;
    public int curLevel = 0;
    public Spawner spawner;
    public float prebattleCD = 5f;
    public float PrepareCD = 30f;
    public float pbcd;
    public float pcd;
    public float gameSpeed;

    void OnEnable()
    {
        curLevel = 1;
        if(curLevel==1){curPhase = Phase.Prepare;}
        Time.timeScale = gameSpeed;
        pbcd = prebattleCD;
        pcd = PrepareCD;
    }

    // Update is called once per frame
    void Update()
    {
        if(curPhase == Phase.Prepare)
        {
            PreparationCountdown();
        }
        if(curPhase == Phase.Prebattle)
        {
            ToBattlePhase();
        }

        if(curPhase == Phase.Battle)
        {
            spawner.StartSpawning();
            
        }

    }

   public void PreparationCountdown()
    {
        if (pcd > 0) { pcd -= Time.deltaTime; }
        if (pcd <= 0) { pcd = 0; }
        if(pcd == 0)
        {
            pcd = PrepareCD;
            curPhase = Phase.Prebattle;
            
        }
    }

   public void ToBattlePhase()
    {
        if (pbcd > 0) { pbcd -= Time.deltaTime; }
        if (pbcd <=0) { pbcd = 0; }
        if (pbcd == 0)
        {
            pbcd = prebattleCD;
            curPhase = Phase.Battle;
            
        }
    }

    // if this wave never leak + 1 gold

    

    // each wave gold given
}
