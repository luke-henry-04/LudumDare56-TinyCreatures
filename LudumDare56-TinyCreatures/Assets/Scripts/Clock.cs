using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : MonoBehaviour
{
    public Transform arm;
    public float bossInterval = 20;
    float timer = 0;

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        arm.localRotation=Quaternion.Euler(new Vector3(arm.localEulerAngles.x, Mathf.Lerp(0,360,timer/bossInterval), arm.localEulerAngles.z));
        if (timer >= bossInterval)
        {
            BossCheck();
        }
        timer %= bossInterval;

       
    }

    void BossCheck()
    {
        //BOSS CHECK CODE
        Animator AM = boss.GetComponent<Animator>();
        AM.Play("Start");


    }
}
