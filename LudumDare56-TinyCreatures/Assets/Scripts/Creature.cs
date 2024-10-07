using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Creature : MonoBehaviour
{
    public DrawerBounds drawer;
    public GameObject canvas;
    public Image image;

    public float anger = 0.1f;
    public float angerRate;
    public float angerMax;

    public float speed;

    public string[] needTags;
    public float[] maxNeeds;
    public Sprite[] needSprites;
    float[] needs;
    public Material angry;
    Material color;

    public bool held = false;
    
    Player player;

    AudioSource AS;
    Camera cam;
    public Rigidbody RB;
    MeshRenderer MR;
    MeshFilter MF;
    Mesh mesh;
    Vector3[] verts;
    int[] tris;

    // Start is called before the first frame update
    void Start()
    {
        AS = gameObject.GetComponent<AudioSource>();

        color = gameObject.GetComponent<MeshRenderer>().material;

        canvas.SetActive(false);
        cam = Camera.main;

        needs = new float[maxNeeds.Length];
        for(int i = 0; i<needs.Length; i++) needs[i] = Random.Range(0f, 1f) * maxNeeds[i];

        player = cam.transform.parent.gameObject.GetComponent<Player>();
        drawer = gameObject.GetComponentInParent<DrawerBounds>();
        RB = gameObject.GetComponent<Rigidbody>();
        MR = gameObject.GetComponent<MeshRenderer>();
        MF = gameObject.GetComponent<MeshFilter>();
        mesh = MF.mesh;
        verts = mesh.vertices;
        tris = mesh.triangles;


    }

    private void Update()
    {
        verts = mesh.vertices;
        for (int i = 0; i < verts.Length; i++)
        {
            verts[i] =
                transform.localScale.x *
                (
                    verts[i]).normalized 
                    *((Mathf.PerlinNoise(((float)i) / ((float)verts.Length), Time.time)+ 0.5f) 
                    / 2f 
                    + Random.Range(-anger, anger)
                );
        }
        mesh.SetVertices(verts);
        MF.mesh = mesh;

        UpdateNeeds();
        if(!held && !player.holding)PickUpCheck();
        if (canvas.activeSelf) RotateUI();
        
    }
    void RotateUI()
    {
        canvas.transform.LookAt(cam.transform.position);

    }
    void PickUpCheck()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.rigidbody == RB)
                {
                    player.AS.Play();
                    player.holding = true;
                    held = true;
                    transform.SetParent(cam.transform);
                    RB.isKinematic = true;
                    gameObject.GetComponent<Collider>().enabled = false;
                    transform.localPosition = new Vector3(0, 0, 5);
                }
                
            }
        }

    }
    void UpdateNeeds()
    {
        for(int i = 0; i<needs.Length; i++)
        {
            needs[i] -= Time.deltaTime;
            if (needs[i] < 0)
            {
                if (!canvas.activeSelf)
                {
                    canvas.SetActive(true);
                    image.sprite = needSprites[i];
                }

                anger += Time.deltaTime * angerRate;
                anger = Mathf.Clamp(anger, 0, angerMax);
            }
        }

        if (anger >= angerMax - 0.01f && !held)
        {
            if (!AS.isPlaying)
            {
                AS.Play();
            }
            gameObject.GetComponent<MeshRenderer>().material = angry;
            Drawer d = drawer.GetComponentInChildren<Drawer>();
            if (!d.animator.GetBool("Open"))
            {
                AnimatorStateInfo ASI = d.animator.GetCurrentAnimatorStateInfo(0);
                if ((ASI.IsName("TopDrawerIn") ||  ASI.IsName("BottomDrawerIn") || ASI.IsName("New State")) &&   ASI.normalizedTime >= 0.75)
                {
                    d.open = true;
                    d.animator.SetBool("Open", true);
                }
            }
        }

    }

    private void FixedUpdate()
    {
        if (!held)
        {
            RB.MovePosition(RB.position - transform.forward * speed);
            RB.position = new Vector3(
                Mathf.Clamp(RB.position.x, drawer.bounds[0].position.x, drawer.bounds[1].position.x),
                Mathf.Clamp(RB.position.y, drawer.bounds[1].position.y, drawer.bounds[0].position.y),
                Mathf.Clamp(RB.position.z, drawer.bounds[0].position.z, drawer.bounds[1].position.z)
            );
        }
        transform.localEulerAngles = new Vector3(0, 20 * (2 * Mathf.PerlinNoise(Time.time, (float)gameObject.GetInstanceID()) - 1) + transform.localEulerAngles.y,0);

    }

    private void OnTriggerStay(Collider other)
    {
        if (!held && canvas.activeSelf)
        {
            for (int i = 0; i < needs.Length; i++) {
                if (other.CompareTag(needTags[i]))
                {
                    if (image.sprite == needSprites[i])
                    {
                        Food f = other.GetComponent<Food>();
                        if (f!=null)
                        {
                            if (f.foodLevel > 0)
                            {
                                AS.Stop();
                                gameObject.GetComponent<MeshRenderer>().material = color;
                                canvas.SetActive(false);
                                needs[i] = maxNeeds[i];
                                anger = 0.05f;
                                f.foodLevel -= 20;
                                f.UpdateFoodLevel();
                                break;
                            }
                        }

                        Water w = other.GetComponent<Water>();
                        if (w != null)
                        {
                            if (w.waterLevel > 0)
                            {
                                AS.Stop();
                                gameObject.GetComponent<MeshRenderer>().material = color;
                                canvas.SetActive(false);
                                needs[i] = maxNeeds[i];
                                anger = 0.05f;
                                w.waterLevel -= 20;
                                w.UpdateWaterLevel();
                                break;
                            }
                        }

                    }
                }
            }
            
            
        }
    }

}


