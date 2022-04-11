using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeUpgradesShop : MonoBehaviour
{
    public GameObject upgradeButtonPrefab;
    public GameObject upgradeManager;

    void Start()
    {
        GetCostOfUpgrades upgradeCosts = upgradeManager.GetComponent<GetCostOfUpgrades>();
        for(int i = 0; i < upgradeCosts.upgradeCostPairs.Count; i++)
        {
            GameObject instantiatedButton = Instantiate(upgradeButtonPrefab, transform);
            instantiatedButton.GetComponent<BuyUpgrade>().SetUpgrade(upgradeCosts.upgradeCostPairs[i].upgrade);
        }
    }
}
