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
           
            currentEventTimer = 0.0f;
            if (currentEvent != null && currentEvent.playingEvent)
            {
                print("Ending Event");
                currentEvent.EndEvent();
                return; // Added return to have the game go calm again
            }
            print("Running Event");
            currentEvent = events[Random.Range(0, events.Length)];

            currentEvent.PreformEvent();
        }
    }
}
