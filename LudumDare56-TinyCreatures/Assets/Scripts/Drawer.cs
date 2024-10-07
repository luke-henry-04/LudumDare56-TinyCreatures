using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{

    Camera cam;
    Player player;
    public Animator animator;
    public AnimationClip[] OpenCloseAnims;
    public bool open = false;
    bool dropping = false;
    public Light spotlight;

    private void Start()
    {
        cam = Camera.main;
        player = cam.gameObject.GetComponentInParent<Player>();
    }

    private void OnMouseDown()
    {
        
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            open = !open;
            animator.SetBool("Open", open);
            animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
        }

    }

    private void LateUpdate()
    {
        if (dropping)
        {
            player.holding = false;
            dropping = false;
        }
    }


    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1) && player.holding)
        {
            
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit) && open)
            {
                
                Creature heldCreature = cam.gameObject.GetComponentInChildren<Creature>();
                if (heldCreature != null)
                {
                    heldCreature.gameObject.transform.SetParent(transform.parent);
                    heldCreature.held = false;
                    heldCreature.RB.isKinematic = false;
                    dropping = true;

                    heldCreature.gameObject.GetComponent<Collider>().enabled = true;
                    heldCreature.transform.localPosition = new Vector3(8.783f, 2.36f, 2.12f);

                    heldCreature.drawer = transform.parent.parent.GetComponent<DrawerBounds>();
                    heldCreature.transform.rotation = Quaternion.Euler(0, 0, 0);
                }

            }

        }
       
    }
}
