using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetCostOfUpgrades : MonoBehaviour
{
    public List<UpgradeCostPairs> upgradeCostPairs;

    public float GetUpgradeCost(GameObject upgrade)
    {
        for(int i = 0; i < upgradeCostPairs.Count; i++)
        {
            if(upgradeCostPairs[i].upgrade == upgrade)
            {
                return upgradeCostPairs[i].cost;
            }
        }

        return -1;
    }
}

[System.Serializable]
public struct UpgradeCostPairs
{
    public GameObject upgrade;
    public float cost;
}
