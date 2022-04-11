using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMultiplier: MonoBehaviour, IUpgrade
{
    public FloatVariableObject multiplier;
    public FloatVariableObject fishPoints;

    public void Execute()
    {
        WorldController.playerPoints += (((int)multiplier.value * (int)fishPoints.value) - (int)fishPoints.value);
    }

}
