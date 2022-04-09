using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New event", menuName = "Event")]
public class EventObject : ScriptableObject
{
    List<EventReceiver> listeners = new List<EventReceiver>();

    public void Invoke()
    {
        for(int i = 0; i < listeners.Count; i++)
        {
            listeners[i].InvokeEvent(this);
        }
    }

    

    public void RegisterAsListener(EventReceiver newListener) => listeners.Add(newListener);

    public void UnRegisterAsListener(EventReceiver listener) => listeners.Remove(listener);
}
