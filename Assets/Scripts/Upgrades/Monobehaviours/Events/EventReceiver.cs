using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventReceiver : MonoBehaviour
{
    public List<EventDelegatePairs> eventDelegatePairs;

    private void OnEnable()
    {
        for(int i = 0; i < eventDelegatePairs.Count; i++)
        {
            eventDelegatePairs[i].serializedEvent.RegisterAsListener(this);
        }
    }

    private void OnDisable()
    {
        for (int i = 0; i < eventDelegatePairs.Count; i++)
        {
            eventDelegatePairs[i].serializedEvent.UnRegisterAsListener(this);
        }
    }

    public void InvokeEvent(EventObject invoker)
    {
        for(int i = 0; i < eventDelegatePairs.Count; i++)
        {
            if(eventDelegatePairs[i].serializedEvent == invoker)
            {
                eventDelegatePairs[i].serializedDelegate.Invoke();
            }
        }
    }
}

[System.Serializable]
public struct EventDelegatePairs
{
    public UnityEvent serializedDelegate;
    public EventObject serializedEvent;
}
