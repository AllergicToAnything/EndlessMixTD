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

    private void OnEnable()
    {
        isOccupied = false;
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
        }
        if (towerStates == State.P) // What happen when State P
        {
            tower.gameObject.SetActive(true);
            isOccupied = true;

        }
        if (towerStates == State.B)// What happen when State B
        {
            tower.gameObject.SetActive(true);
            isOccupied = true;
        }

    }

  

    
  
}