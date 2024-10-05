using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour
{
    public DNA dna;

    CircleCollider2D col;
    MeshRenderer MR;
    Mesh mesh;
    List<Vector3> verts;
    List<int> tris;

    // Start is called before the first frame update
    void Start()
    {
        col = gameObject.AddComponent<CircleCollider2D>();
        MR = gameObject.AddComponent<MeshRenderer>();
        verts = new List<Vector3>();
        tris = new List<int>();

        



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class DNA
{
    public Color color;
    public float radius;
    public int something;
    public DNA(Color c_)
    {
        color = c_;

    }

}
