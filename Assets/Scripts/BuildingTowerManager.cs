using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class BuildingTowerManager : MonoBehaviour
{
    
    public Platform platform;
    public int gold = 1;
    public GameObject _confirmUI;
    public GameObject _towerUpgradeMenu;
    public GameObject _elementalSelection;
    public Text notEnoughGold;
    public Text goldTextOutput;
    public LevelManager lvlManager;
    public LayerMask mask = -1;

    Vector3 targetPosition;
    public Camera cam;
    Tower currentTower;

    private void Update()
    {
        WhenClick();
        goldTextOutput.text = "Gold : "+gold.ToString();
    }
    
    public void WhenClick()
    {
        if (Input.GetMouseButtonDown(0) /*&& lvlManager.curPhase == Phase.Prepare*/)
        {
            GetWorldPoint(Input.mousePosition);
            UISwitchButton();
        }
    }

    public void DeactiveForButton() // Change to State Invisible if it is not builded
    {
        if (platform.towerStates != State.B)
        {
            platform.towerStates = State.I;
            UISwitchButton();
        }
        
    }

    public void UISwitchButton() // State Invisible turn off UI | State Prebuild Turn on UI
    {
        if (platform.towerStates == State.P)
        {
            _towerUpgradeMenu.gameObject.SetActive(true);
        }
        if (platform.towerStates != State.P)
        {
            _towerUpgradeMenu.gameObject.SetActive(false);            
            _confirmUI.gameObject.SetActive(false);
            _elementalSelection.gameObject.SetActive(true);
            
            
        }
    }

    public void SwitchOnButton()
    {
        currentTower = platform.tower;
        foreach (MaterialChanger mc in currentTower.allChild)
        {
            mc.ChangeButton();
        }
    }

    public void FireButton()
    {
        if (platform.towerStates == State.P)
        {
            platform.elements = Element.Fire;
            currentTower = platform.tower;
            foreach (MaterialChanger mc in currentTower.allChild)
            {
                mc.ChangeMatToFire();
                platform.tower.BuildTower();
                UISwitchButton();
            }
            platform.TowerElementAttribute();
            gold -= 1;

        }
    }
    public void ElectricButton()
    {
        
        if (platform.towerStates == State.P)
        {
            platform.elements = Element.Electric;
            currentTower = platform.tower;
            foreach (MaterialChanger mc in currentTower.allChild)
            {
                mc.ChangeMatToElectric();
                platform.tower.BuildTower();
                UISwitchButton();
            }
            platform.TowerElementAttribute();
            gold -= 1;
        }
    }
    public void IceButton()
    {
        
        if (platform.towerStates == State.P)
        {
            platform.elements = Element.Ice;
            currentTower = platform.tower;
            foreach (MaterialChanger mc in currentTower.allChild)
            {
                mc.ChangeMatToIce();
                platform.tower.BuildTower();
                UISwitchButton();
                
            }
            platform.TowerElementAttribute();
            gold -= 1;
        }
    }
    public void PoisonButton()
    {
        if (platform.towerStates == State.P)
        {
            platform.elements = Element.Poison;
            currentTower = platform.tower;
            foreach (MaterialChanger mc in currentTower.allChild)
            {
                mc.ChangeMatToPoison();
                platform.tower.BuildTower();
                UISwitchButton();
            }
            platform.TowerElementAttribute();
            gold -= 1;
        }
    }   
    

    Vector3 GetWorldPoint(Vector3 screenSpace) // Collision for click
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);

        // Fire a ray to the touch position
        if (Physics.Raycast(ray, out RaycastHit info, Mathf.Infinity, mask))
        {
            if((info.collider.gameObject.tag == "Platform")&&_towerUpgradeMenu.activeSelf==false)
            {
                if (gold > 0)
                {
                    platform = info.collider.gameObject.GetComponent<Platform>();
                    if(platform.isOccupied == false)
                    {
                        platform.towerStates = State.P;
                    }
                    
                }
                else if( gold <= 0)
                {
                    notEnoughGold.gameObject.SetActive(true);
                }
            }
             return info.point;
        }
        else
        {
            return Vector3.zero;
        }
    } 


    
}