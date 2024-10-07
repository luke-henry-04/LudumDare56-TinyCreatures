using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool holding = false;
    private Camera cam;
    public float sensitivity;
    public float viewDegrees = 45;
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

        //cam.transform.RotateAround(cam.transform.position, gameObject.transform.right, );
        cam.transform.localEulerAngles = new Vector3(cam.transform.localEulerAngles.x - Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime, 0, 0);

        cam.transform.localRotation = Quaternion.Euler(
            cam.transform.localEulerAngles.x<=180?
                Mathf.Clamp(cam.transform.localEulerAngles.x,0, viewDegrees) :
                Mathf.Clamp(cam.transform.localEulerAngles.x,360- viewDegrees, 360),
            0,
            0
        );

        transform.RotateAround(cam.transform.position, Vector3.up, Input.GetAxis("Mouse X")*sensitivity*Time.deltaTime);

        transform.position += speed *(transform.forward * Input.GetAxis("Vertical") + transform.right * Input.GetAxis("Horizontal"))* Time.deltaTime;
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, bounds[0].position.x, bounds[1].position.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, bounds[0].position.z, bounds[1].position.z)
        );

    }
}
