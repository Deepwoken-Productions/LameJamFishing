using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveUpgrade : MonoBehaviour
{
    public List<IncompatibleUpgrades> incompaibleUpgradesList;

    List<GameObject> spawnedUpgrades;

    private void Start()
    {
        spawnedUpgrades = new List<GameObject>();
    }

    public void SpawnUpgrade(GameObject upgrade)
    {
        for (int v = 0; v < spawnedUpgrades.Count; v++)
        {
            if (spawnedUpgrades[v].name == upgrade.name)
            {
                return;
            }

            for (int i = 0; i < incompaibleUpgradesList.Count; i++)
            {
                if (incompaibleUpgradesList[i].upgrade == upgrade)
                {
                    for (int z = 0; z < incompaibleUpgradesList[i].incompatibleUpgrades.Count; z++)
                    {
                        if (incompaibleUpgradesList[i].incompatibleUpgrades[z].name == spawnedUpgrades[v].name)
                        {
                            Destroy(spawnedUpgrades[v]);
                            spawnedUpgrades.RemoveAt(v);
                        }
                    }
                }
            }

        }

        GameObject instantiatedUpgrade = Instantiate(upgrade);
        instantiatedUpgrade.name = upgrade.name;

        spawnedUpgrades.Add(instantiatedUpgrade);
        Debug.Log(spawnedUpgrades[spawnedUpgrades.Count - 1].name);
    }
}

[System.Serializable]
public struct IncompatibleUpgrades
{
    public List<GameObject> incompatibleUpgrades;
    public GameObject upgrade;
}
