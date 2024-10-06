using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Camera cam;
    public float sensitivity;
    public float speed;
    public Transform[] bounds;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    // Update is called once per frame
    void Update()
    {

        cam.transform.RotateAround(cam.transform.position, gameObject.transform.right, -Input.GetAxis("Mouse Y")*sensitivity*Time.deltaTime);
        transform.RotateAround(cam.transform.position, Vector3.up, Input.GetAxis("Mouse X")*sensitivity*Time.deltaTime);

        transform.position += speed *(transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"))* Time.deltaTime;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds[0].position.x, bounds[1].position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bounds[0].position.z, bounds[1].position.z)
        );

    }
}
