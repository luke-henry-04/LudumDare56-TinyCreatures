using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGamePanel : MonoBehaviour
{
    public float fadeIn = 3;
    float timer = 0;
    Image img;
    public Color endColor;
    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < fadeIn)
        {
            img.color = Color.Lerp(Color.black, endColor, timer / fadeIn);
            timer += Time.deltaTime;
        }
        else
        {
            img.color = endColor;
        }
    }
}
