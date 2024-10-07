using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int foodLevel = 100;
    public Transform foodDisk;
    public Material[] FoodMaterials;

    // Start is called before the first frame update
    void Start()
    {
        UpdateFoodLevel();
    }

    public void UpdateFoodLevel()
    {
        foodDisk.localScale -= (foodDisk.localScale.y - Mathf.Lerp(1,3.5f,((float)foodLevel)/100f))*Vector3.up;
        if (foodLevel <= 0)
        {
            foodDisk.GetComponentInChildren<MeshRenderer>().material = FoodMaterials[1];
        }
        else
        {
            foodDisk.GetComponentInChildren<MeshRenderer>().material = FoodMaterials[0];  
        }
    }
}
