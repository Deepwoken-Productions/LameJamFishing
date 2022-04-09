using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRaiseEvent : MonoBehaviour
{
    public EventObject eventToRaise;

    void Start()
    {
        eventToRaise.Invoke();
    }
}
