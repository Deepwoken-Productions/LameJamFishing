using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaiseEvent : MonoBehaviour
{
    public EventObject eventToRaise;
    public GameObject upgrade;
    public GameObject incompatibleUpgrade;

    public GiveUpgrade upgradeGiver;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            eventToRaise.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            upgradeGiver.SpawnUpgrade(upgrade);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            upgradeGiver.SpawnUpgrade(incompatibleUpgrade);
        }
    }
}
