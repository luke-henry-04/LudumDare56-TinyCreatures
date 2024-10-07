using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Clock : MonoBehaviour
{
    public Transform arm;
    public float bossInterval = 20;
    float timer = 0;
    Animator AM;
    public GameObject boss;
    public float bossDuration = 5;
    public float timeAfterLoss = 5;
    float lossTimer = 0;
    public Image LostPanel;


    public Drawer[] drawers;

    public Light[] lights;
    public GameObject player;
    public Email[] emails;
    public Light screenSpotlight;

    public Transform[] spawners;
    public Material[] creatureMaterials;
    public GameObject creaturePrefab;
    AudioSource AS;
    bool sfxPlayed = false;

    // Start is called before the first frame update
    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();
         AM = boss.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (lossTimer < 0.05f)
        {
            timer += Time.deltaTime;
            arm.localRotation = Quaternion.Euler(new Vector3(arm.localEulerAngles.x, Mathf.Lerp(0, 360, timer / bossInterval) - 90, arm.localEulerAngles.z));
            if (timer >= bossInterval)
            {
                if(!sfxPlayed)AS.Play();
                BossCheck();

            }
            if (timer - bossInterval > bossDuration)
            {
                Spawn(1.1f);
                Spawn(0.5f);
                timer = 0;
                sfxPlayed = false;
                AM.SetBool("Check", false);
            }
        }
        else
        {
            lossTimer += Time.deltaTime;
            LostPanel.color = Color.Lerp(new Color(0,0,0,0), Color.black, lossTimer / timeAfterLoss);
            if (lossTimer > timeAfterLoss)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                SceneManager.LoadScene("Game Over");
            }
        }

       
    }

    void Spawn(float prob)
    {
        if (Random.Range(0f, 1f) < prob)
        {
            int ind = Random.Range(0, spawners.Length);
            GameObject child = GameObject.Instantiate(creaturePrefab, spawners[ind].parent);
            child.transform.position = spawners[ind].position;
            child.GetComponent<MeshRenderer>().material = creatureMaterials[Random.Range(0, creatureMaterials.Length)];
            spawners[ind].parent.GetComponentInChildren<Drawer>().open = true;
            spawners[ind].parent.GetComponent<Animator>().SetBool("Open", true);

        }
    }

    void BossCheck()
    {
        //BOSS CHECK CODE
        sfxPlayed = true;
        AM.SetBool("Check", true);

        if(AM.GetCurrentAnimatorStateInfo(0).IsName("Checking"))
        {

            int blinking = 0;
            for (int i = 0; i < emails.Length; i++)
            {
               if (emails[i].blinkTimer > 0.0001f)
                {
                    blinking++;
                }
            }
            if (blinking > 2)
            {
                screenSpotlight.enabled = true;
                Lose();
            }

            if (player.GetComponentInChildren<Creature>())
            {
                Lose();
            }
            for(int i = 0; i < drawers.Length; i++)
            {
                //CHECK ALL OPEN DRAWERS
                if (drawers[i].open)
                {
                    if (drawers[i].transform.parent.parent.GetComponentInChildren<Creature>())
                    {
                        drawers[i].spotlight.enabled = true;
                        Lose();
                    }
                }
            }
        }

    }

    void Lose()
    {
        for (int j = 0; j < lights.Length; j++)
        {
            AM.SetBool("Lost", true);
            lossTimer += 0.1f;
            lights[j].color = Color.red;
            
        }
    }

}
