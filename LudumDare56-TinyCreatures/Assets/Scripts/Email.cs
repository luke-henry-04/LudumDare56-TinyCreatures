using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Email : MonoBehaviour
{

    Image img;
    public Color startColor;
    public Color endColor;
    public Color blinkColor;
    float timer = 0;
    public float blinkTimer = 0;
    float blinkRate = 0.5f;
    
    

    // Start is called before the first frame update
    void Start()
    {
        img = gameObject.GetComponent<Image>();
        img.color = startColor;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < 1)
        {
            if (Random.Range(0f, 1f) <= Time.deltaTime / 10f)
            {
                timer += 0.2f;
                img.color = Color.Lerp(startColor, endColor, timer);
            }
            
        }
        else
        {
            blinkTimer += Time.deltaTime;
            if (blinkTimer < blinkRate)
            {
                img.color = blinkColor;
            }else if (blinkTimer < blinkRate * 2)
            {
                img.color = Color.red;
            }
            else
            {
                img.color = blinkColor;
                blinkTimer = 0;
               if(blinkRate>0.1f) blinkRate *= 0.93f; 
               //NOTE tie blink rate to losing
            }
        }
    }

    void OnMouseUpAsButton()
    {
        if (blinkTimer > 0.0001f)
        {
            blinkTimer = 0;
            blinkRate = 0.5f;
            timer = 0;
            img.color = startColor;

        }   
    }
}
