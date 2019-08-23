using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomColor : MonoBehaviour
{
    Color randomColor = new Color();
    float x;
    float y;

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()

    {
        
        x = Random.Range(.7f, 1.5f);
        y = Random.Range(.8f, 1.5f);
        RandomizeColor();
        
    }

    void RandomizeColor()
    {
        
        randomColor.r = Random.Range(x, y);
        randomColor.g = Random.Range(x, y);
        randomColor.b = Random.Range(x, y);
        randomColor.a = Random.Range(x, y);
        this.GetComponent<Image>().color = randomColor;

    }
}
