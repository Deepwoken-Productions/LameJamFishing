using UnityEngine;

public class StormEvent : Event
{
    [Header("Storm Properties")]

    [SerializeField] private float minLightIntensity;
    [SerializeField] private float maxLightIntensity;
    [SerializeField] private ParticleSystem rainParticles;

    public override void PreformEvent()
    {
        rainParticles.Play();
        //increase wave speed?
        //clouds?
        //set the light to a rnd value
    }

    public override void EndEvent()
    {
        rainParticles.Stop();
    }
}
