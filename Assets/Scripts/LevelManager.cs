using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Phase { Prepare,Prebattle,Battle }

public class LevelManager : MonoBehaviour
{
    public Phase curPhase;
    public int curLevel = 1;

    void OnEnable()
    {
       // curPhase = Phase.Prepare;        
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    // if this wave never leak + 1 gold

    

    // each wave gold given
}
