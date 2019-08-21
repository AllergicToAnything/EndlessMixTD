using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
    public Material selection;
    public Material fire;
    public Material electric;
    public Material poison;
    public Material ice;
    public Material baseMat;
    public Material rim;
    Renderer rend;

    public BuildingTowerManager manager;
    public Platform platform;

    private void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
    }
    private void Update()
    {
        platform = manager.platform;

        if (platform.towerStates == State.B)
        {
            if (this.gameObject.tag == "Rim")
            {
                rend.sharedMaterial = rim;
            }
            if (this.gameObject.tag == "BaseWell")
            {
                rend.sharedMaterial = baseMat;
            }
        }

    }

    public void TurnToInvisibleState()
    {
        platform.towerStates = State.I;
        if (this.gameObject.tag == "Rim")
        {
            rend.sharedMaterial = selection;
        }
        if (this.gameObject.tag == "BaseWell")
        {
            rend.sharedMaterial = selection;
        }
        if (this.gameObject.tag == "Energy")
        {
            rend.sharedMaterial = selection;
        }
        if (this.gameObject.tag == "EnergyWell")
        {
            rend.sharedMaterial = selection;
        }
    }


    public void ChangeButton()
    {
        
        if (platform.towerStates == State.P)
        {
            if (this.gameObject.tag == "Rim")
            {
                rend.sharedMaterial = selection;
            }
            if (this.gameObject.tag == "BaseWell")
            {
                rend.sharedMaterial = selection;
            }
            if (this.gameObject.tag == "Energy")
            {
                rend.sharedMaterial = selection;
            }
            if (this.gameObject.tag == "EnergyWell")
            {
                rend.sharedMaterial = selection;
            }
        }
    }

    public void ChangeMatToFire()
    {
        if (platform.towerStates != State.B)
        {
            if (this.gameObject.tag == "Energy")
            {
                if (platform.elements == Element.Fire)
                {
                    platform.towerStates = State.B;

                }
            }            
        }
        if (this.gameObject.tag == "Energy" && platform.towerStates == State.B && platform.elements == Element.Fire)
        {
            rend.sharedMaterial = fire;
        }
    }
    public void ChangeMatToIce()
    {
        if (platform.towerStates != State.B)
        {
            if (this.gameObject.tag == "Energy")
            {
                if (platform.elements == Element.Ice)
                {
                    platform.towerStates = State.B;
                }
            }
        }
        if (this.gameObject.tag == "Energy" && platform.towerStates == State.B && platform.elements == Element.Ice)
        {
            rend.sharedMaterial = ice;
        }
    }
    public void ChangeMatToElectric()
    {
        if (platform.towerStates != State.B)
        {
            if (this.gameObject.tag == "Energy")
            {
                if (platform.elements == Element.Electric)
                {
                    platform.towerStates = State.B;
                }
            }
        }
        if (this.gameObject.tag == "Energy" && platform.towerStates == State.B && platform.elements == Element.Electric)
        {
            rend.sharedMaterial = electric;
        }
    }
    public void ChangeMatToPoison()
    {
        if (platform.towerStates != State.B)
        {
            if (this.gameObject.tag == "Energy" || this.gameObject.tag == "EnergyWell")
            {
                if (platform.elements == Element.Poison)
                {
                    platform.towerStates = State.B;
                }
            }
        }
        if (this.gameObject.tag == "Energy" && platform.towerStates == State.B && platform.elements == Element.Poison)
        {
            rend.sharedMaterial = poison;
        }
    }



}

