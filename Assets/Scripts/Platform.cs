using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State { I, P, B, } // I = Invisible P = Prebuild B = Builded
public enum Element { Fire, Electric, Poison, Ice, None } 


public class Platform : MonoBehaviour
{
    public State towerStates = State.I;
    public BuildingTowerManager manager;
    public Tower tower;
    public Element elements = Element.None;
    public bool isOccupied;
    Detector detector;
    public Spawner spawner;

    private void OnEnable()
    {
        isOccupied = false;
        detector = tower.GetComponent<Detector>();
    }

    private void Update()
    {
        StateTransform();

    }

    void StateTransform()
    {
        if (towerStates == State.I) // What happen when State I
        {
            tower.gameObject.SetActive(false);
            isOccupied = false;
            tower.towerLevel = 1;
        }
        if (towerStates == State.P) // What happen when State P
        {
            tower.gameObject.SetActive(true);
            isOccupied = true;

        }
        if (towerStates == State.B)// What happen when State B
        {
            isOccupied = true;
        }
        
    }

    public void TowerElementAttribute()
    {

        if (elements == Element.Fire)
        {
            tower.attackCooldown *= .51f - .002f * tower.towerLevel;
            detector.damage = detector.initDamage * (1.3f * tower.towerLevel);
            tower.fireDPS = detector.damage / 2;
            tower.fireDPSLimitCount = 7 + tower.towerLevel;
        }
        if (elements == Element.Electric)
        {
            tower.attackCooldown *= .32f - .003f * tower.towerLevel;
            detector.damage = detector.initDamage * 1.1f * tower.towerLevel;

        }
        if (elements == Element.Ice)
        {
            tower.attackCooldown *= 1.505f - .0075f * tower.towerLevel;
            detector.damage = detector.initDamage * 2 * tower.towerLevel;
            tower.icySlowSpeed =( tower.initIcySlowSpeed - .05f*tower.towerLevel); 
        }
        if (elements == Element.Poison)
        {
            tower.attackCooldown *= .88f - .005f * tower.towerLevel;
            detector.damage = detector.initDamage * 1.5f * tower.towerLevel;
            tower.poisonDPS = detector.damage*1.5f;
            tower.poisonDPSLimitCount = 3 + tower.towerLevel;
            tower.poisonSlowSpeed = (tower.initPoisonSlowSpeed - .03f*tower.towerLevel);
        }
    }
        
  




}