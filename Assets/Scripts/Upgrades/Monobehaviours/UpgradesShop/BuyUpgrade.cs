using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyUpgrade : MonoBehaviour
{
    GameObject upgrade;

    GameObject upgradeManager;
    Button parentButton;

    bool bought;

    private void OnEnable()
    {
        if (!bought)
        {
            parentButton = transform.GetComponent<Button>();
            upgradeManager = GameObject.Find("UpgradeManager");

            parentButton.onClick.AddListener(OnClicked);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()
    {
        if (!bought)
        {
            parentButton.onClick.RemoveListener(OnClicked);
        }
    }

    public void SetUpgrade(GameObject upgrade) 
    {
        this.upgrade = upgrade;
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = upgrade.name + " - " + upgradeManager.GetComponent<GetCostOfUpgrades>().GetUpgradeCost(upgrade);
    }

    void OnClicked()
    {
        if (!bought)
        {
            float upgradeCost = upgradeManager.GetComponent<GetCostOfUpgrades>().GetUpgradeCost(upgrade);

            if (WorldController.playerPoints >= upgradeCost)
            {
                bought = true;
                WorldController.playerPoints -= (int)upgradeCost;
                upgradeManager.GetComponent<GiveUpgrade>().SpawnUpgrade(upgrade);

                gameObject.SetActive(false);
            }
        }
    }
}
