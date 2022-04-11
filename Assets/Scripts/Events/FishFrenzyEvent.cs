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
        base.PreformEvent();
        worldController.SetFishSpawnChance(fishSpawnChance);
    }

    public override void EndEvent()
    {
        base.EndEvent();
        worldController.SetFishSpawnChance(0.00005f);
    }
}
