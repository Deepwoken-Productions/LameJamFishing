using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestUpgrade : MonoBehaviour, IUpgrade
{
    public void Execute()
    {
        Debug.Log("Test works");
    }

}
