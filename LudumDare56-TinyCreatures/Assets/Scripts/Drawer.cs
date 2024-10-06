using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drawer : MonoBehaviour
{

    public Camera camera;
    public Animator animator;
    public AnimationClip[] OpenCloseAnims;
    private bool open = false;
  
    
    private void OnMouseDown()
    {
        camera = Camera.main;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)){
            open = !open;
            animator.SetBool("Open", open);
            animator.updateMode = AnimatorUpdateMode.AnimatePhysics;
            
        }
    }
}
