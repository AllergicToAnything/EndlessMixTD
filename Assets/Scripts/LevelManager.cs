﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Phase { Prepare,Prebattle,Battle }

public class LevelManager : MonoBehaviour
{

    public float curLevel = 0;
    public float gameSpeed = 1;
    public float enemySpeed = 1f;

    public Phase curPhase;

    public Shield barrier;
    
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
        
    }

    // Update is called once per frame
    void Update()
    {
        
        Time.timeScale = gameSpeed;
        if (curPhase == Phase.Prepare)
        {
            phaseLvl.text = "Preparing . . . " + pcd.ToString("n0");
            PreparationCountdown();
        }
        if(curPhase == Phase.Prebattle)
        {
            phaseLvl.text = "Next Wave in . . . " + pbcd.ToString("n0");
            ToBattlePhase();
        }

        if(curPhase == Phase.Battle)
        {
            phaseLvl.text = "Wave " + curLevel.ToString();
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

    public void PauseTheGame()
    {
        gameSpeed = 0f;
    }

    public void Faster2()
    {
        gameSpeed = 2f;
    }

    public void Faster10()
    {
        gameSpeed = 10f;
    }

    public void NormalSpeed()
    {
        gameSpeed = 1f;
    }

    // if this wave never leak + 1 gold

    

    // each wave gold given
}
