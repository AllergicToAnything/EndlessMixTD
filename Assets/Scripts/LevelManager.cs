using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Prepare,Prebattle,Battle }

public class LevelManager : MonoBehaviour
{
    public Phase curPhase;
    public int curLevel = 0;
    public Spawner spawner;

    void OnEnable()
    {
        //curLevel = 1;
        if(curLevel==1){curPhase = Phase.Prepare;}
        Time.timeScale = 5f;
    }

    // Update is called once per frame
    void Update()
    {
        if(curPhase == Phase.Prepare)
        {
            spawner.spawnLimitPerLevel = 0;
            Time.timeScale = 0.5f;
        }

        if(curPhase == Phase.Battle)
        {
            spawner.StartSpawning();
            Time.timeScale = 5f;
        }

        if(curPhase == Phase.Prebattle)
        {
            Time.timeScale = .8f;            
            // Start Countdown
        }
    }

    public void EnterLevel()
    {

    }

    // if this wave never leak + 1 gold

    

    // each wave gold given
}
