using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Phase { Prepare,Prebattle,Battle }

public class LevelManager : MonoBehaviour
{

    public int curLevel = 0;
    public float gameSpeed = 1;
    public float enemySpeed = 2f;

    public Phase curPhase;
    
    public Spawner spawner;
    public float prebattleCD = 5f;
    public float PrepareCD = 30f;
    public Text phaseLvl;

    [SerializeField]
    private float pbcd;
    [SerializeField]
    private float pcd;

    void OnEnable()
    {
        curLevel = 1;
        if(curLevel==1){curPhase = Phase.Prepare;}
        pbcd = prebattleCD;
        pcd = PrepareCD;
        print("Prepare Phase");
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = gameSpeed;
        if (curPhase == Phase.Prepare)
        {
            phaseLvl.text = "Preparing... " + pcd.ToString("n0");
            PreparationCountdown();
        }
        if(curPhase == Phase.Prebattle)
        {
            phaseLvl.text = "Going to Start in " + pbcd.ToString("n0");
            ToBattlePhase();
        }

        if(curPhase == Phase.Battle)
        {
            phaseLvl.text = "Battling - Wave " + curLevel.ToString();
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
            print("Prebattle Phase");
            print("Current Level: " + curLevel.ToString());
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
            print("Battle Phase");
            curPhase = Phase.Battle;
            
        }
    }

    // if this wave never leak + 1 gold

    

    // each wave gold given
}
