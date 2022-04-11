using UnityEngine;

public class StormEvent : Event
{
    [Header("Storm Properties")]

    [SerializeField] private float minWaveSpeed;
    [SerializeField] private float maxWaveSpeed;
    [SerializeField] private int maxClouds;
    [SerializeField] private float camShakeMagnitude;
    [SerializeField] private float cloud;
    [SerializeField] private GameObject[] clouds;
    [SerializeField] private ParticleSystem rainParticles;

    private WaveMovement[] waves;
    private float elapsedTime;
    
    [Header("Fish Frenzy Properties")]

    [SerializeField] private float fishSpawnChance;
    
    private WorldController worldController
    {
        get
        {
            return FindObjectOfType<WorldController>();
        }
    }

    private void Awake()
    {
        waves = FindObjectsOfType<WaveMovement>();
    }

    public override void PreformEvent()
    {
        base.PreformEvent();
        rainParticles.Play();

        float waveSpeed = Random.Range(minWaveSpeed, maxWaveSpeed);

        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].OnStorm(waveSpeed);
        }

        FindObjectOfType<CameraController>().ShakeCamera(duration, camShakeMagnitude);

        CloudManager.instance.SpawnClouds();
        worldController.SetFishSpawnChance(fishSpawnChance);
    }
    private void Update()
    {
        if (!playingEvent)
            return;

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            EndEvent();
        }
    }


    public override void EndEvent()
    {
        base.EndEvent();
        rainParticles.Stop();
        for (int i = 0; i < waves.Length; i++)
        {
            waves[i].EndStorm();
        }
        worldController.SetFishSpawnChance(0.00005f);
        CloudManager.instance.DeleteClouds(20);
    }
}
