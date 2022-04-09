using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFrenzyEvent : Event
{
    [Header("Fish Frenzy Properties")]

    [SerializeField] private float fishSpawnChance;

    private WorldController worldController
    {
        get
        {
            return FindObjectOfType<WorldController>();
        }
    }

    public override void PreformEvent()
    {
        worldController.SetFishSpawnChance(fishSpawnChance);
    }

    public override void EndEvent()
    {
        worldController.SetFishSpawnChance(0.0005f);
    }
}
