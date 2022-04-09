using UnityEngine;

public enum EventTypes
{
    Storm,
    FishFrenzy
}

public class Event : MonoBehaviour
{
    public bool playingEvent { get; private set; }

    [Header("Event Properties")]
    public EventTypes type;
    public float duration;

    private float t = 0.0f;

    public virtual void PreformEvent()
    {
        playingEvent = true;
    }

    private void Update()
    {
        if (!playingEvent)
            return;

        t += Time.deltaTime;

        if(t >= duration)
        {
            EndEvent();
        }
    }

    public virtual void EndEvent()
    {
        playingEvent = false;
        t = 0.0f;
    }
}
