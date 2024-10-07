using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerBounds : MonoBehaviour
{

    public Transform[] bounds;

    private void Start()
    {
        if (gameObject.GetComponentInChildren<Creature>())
        {
            gameObject.GetComponentInChildren<Drawer>().open = true;
            gameObject.GetComponent<Animator>().SetBool("Open", true);
        }
    }

}
