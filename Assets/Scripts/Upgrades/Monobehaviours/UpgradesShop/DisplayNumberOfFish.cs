using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayNumberOfFish : MonoBehaviour
{
    public TextMeshProUGUI text;

    void Update()
    {
        text.text = "Fish: " + WorldController.playerPoints;
    }
}
