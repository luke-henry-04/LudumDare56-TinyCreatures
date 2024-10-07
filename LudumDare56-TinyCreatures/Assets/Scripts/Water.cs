using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    public int waterLevel = 100;
    public Transform waterDisk;
    public Material[] waterMaterials;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWaterLevel();
    }

    public void UpdateWaterLevel()
    {
        waterDisk.localScale -= (waterDisk.localScale.y - Mathf.Lerp(1, 3.5f, ((float)waterLevel) / 100f)) * Vector3.up;
        if (waterLevel <= 0)
        {
            waterDisk.GetComponentInChildren<MeshRenderer>().material = waterMaterials[1];
        }
        else
        {
            waterDisk.GetComponentInChildren<MeshRenderer>().material = waterMaterials[0];
        }
    }
}
