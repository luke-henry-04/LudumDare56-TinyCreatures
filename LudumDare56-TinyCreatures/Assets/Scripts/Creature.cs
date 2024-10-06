using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{

    public float anger = 0.1f;
   
    MeshRenderer MR;
    MeshFilter MF;
    Mesh mesh;
    Vector3[] verts;
    int[] tris;

    // Start is called before the first frame update
    void Start()
    {
        
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

}


