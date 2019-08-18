using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPBar : MonoBehaviour
{
    public GameObject[] HPBarSprites; // 0 = BG / 1 = LastHP / 2 = HPBar / 3 = HPBorder / 4 = FullHP / 5 = TakeDamage
    public Enemy enemy;
    public float initHP;
    public float curHP;
    public float hp;
    private void OnEnable()
    {
        enemy = gameObject.GetComponent<Enemy>();
        StartCoroutine(DelayHP());
    }

    IEnumerator DelayHP()
    {
        yield return new WaitForSeconds(1f);
        initHP = enemy.hp;
    }

    // Update is called once per frame
    void Update()
    {

        curHP = enemy.hp;             

        hp = (curHP / initHP);

        if(hp >= 1)
        {
            HPBarSprites[4].SetActive(true);
        }
        else if(hp < 1)
        {
            HPBarSprites[4].SetActive(false);
        }

        if(hp <= .02f)
        {
            HPBarSprites[2].SetActive(false);
        }
        else if (hp>.02f)
        {
            HPBarSprites[2].SetActive(true);
        }

        if(hp <= 0.001f)
        {
            HPBarSprites[1].SetActive(false);
        }
        else if(hp>0.001f)
        {
            HPBarSprites[1].SetActive(true);
        }

    }
}
