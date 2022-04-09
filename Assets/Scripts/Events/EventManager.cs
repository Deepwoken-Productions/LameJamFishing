using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public Event currentEvent { get; private set; }

    [SerializeField] private Event[] events;
    [SerializeField, Range(0, 120f)] private float pickEventTimer;

    private float currentEventTimer;

    private void Update()
    {
        if(currentEventTimer <= pickEventTimer)
        {
            currentEventTimer += Time.deltaTime;
        }
        else
        {
            if (currentEvent != null && currentEvent.playingEvent)
                currentEvent.EndEvent();

            currentEventTimer = 0.0f;
            currentEvent = events[Random.Range(0, events.Length)];

            if (currentEvent != null)
            {
                currentEvent.PreformEvent();
                Debug.Log(currentEvent.type);
            }
        }
    }
}
