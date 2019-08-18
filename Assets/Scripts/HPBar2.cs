using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar2 : MonoBehaviour
{
    HPBar hpBar;

    private void Start()
    {
        hpBar = GetComponentInParent<HPBar>();
    }
    // Update is called once per frame
    void Update()
    {
        GetComponent<Image>().fillAmount = hpBar.hp;
    }
}
