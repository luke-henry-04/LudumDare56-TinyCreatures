using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public DrawerBounds drawer;
    public float anger = 0.1f;
    public float speed;
    Rigidbody RB;
    MeshRenderer MR;
    MeshFilter MF;
    Mesh mesh;
    Vector3[] verts;
    int[] tris;

    // Start is called before the first frame update
    void Start()
    {
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
            verts[i] = (verts[i]).normalized * (((Mathf.PerlinNoise(((float)i) / ((float)verts.Length), Time.time) + 0.5f)) / 2f + Random.Range(-anger, anger)) * transform.localScale.x;

        }


        mesh.SetVertices(verts);
        MF.mesh = mesh;
    }

    private void FixedUpdate()
    {
        RB.MovePosition(RB.position - transform.forward*speed);
        RB.MoveRotation(transform.rotation * Quaternion.Euler(new Vector3(0, 10*(2*Mathf.PerlinNoise(Time.time, 0.5f)-1))));
        RB.position = new Vector3(
            Mathf.Clamp(RB.position.x, drawer.bounds[0].position.x, drawer.bounds[1].position.x),
            Mathf.Clamp(RB.position.y, drawer.bounds[1].position.y, drawer.bounds[0].position.y),
            Mathf.Clamp(RB.position.z, drawer.bounds[0].position.z, drawer.bounds[1].position.z)
        ) ;
    }

}


