using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RectTransform))]
public class WorldController : MonoBehaviour
{
    public static int playerPoints = 0;
    public static float curTime { get; private set; }
    
    [SerializeField] private Transform fishParent;
    [SerializeField] private GameObject [] spawnableFish;
    [SerializeField] private GameObject shinyParticle;
    [Range(0,1f)]
    [SerializeField] private float spawnChance;
    [Range(0,1f)]
    [SerializeField] private float shinyChance;
    
    private RectTransform spawnArea;

    private void Start()
    {
        spawnArea = GetComponent<RectTransform>();
    }

    public async void BeginRound(float duration)
    {
        curTime = 0;
        while (curTime < duration)
        {
            if (Random.Range(0, 1f) <= spawnChance)
            {
                SpawnFish();
            }

            curTime += Time.deltaTime;
            await Task.Yield();
        }
    }

    private void SpawnFish()
    {
        int randomFish = Random.Range(0, spawnableFish.Length);

        Vector2 spawnLoc = new Vector2(Random.Range(spawnArea.rect.min.x, spawnArea.rect.max.x), Random.Range(spawnArea.rect.min.y, spawnArea.rect.max.y));

        Transform go = Instantiate(spawnableFish[randomFish], spawnLoc, Quaternion.identity, fishParent).transform;
        //is shiny
        if (Random.Range(0, 1f) <= shinyChance)
        {
            Instantiate(shinyParticle, go);
        }
    }
}
