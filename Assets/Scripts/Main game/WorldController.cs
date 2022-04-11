using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

[RequireComponent(typeof(RectTransform))]
public class WorldController : MonoBehaviour
{
    public UnityEngine.Events.UnityEvent gameEndEvent;
    public static int playerPoints = 0;
    public float multiplier = 1;
    public static float curTime { get; private set; }
    
    [SerializeField] private GameObject [] spawnableFish;
    [SerializeField] private GameObject shinyParticle;
    [SerializeField] private Text fishCollectedText; 
    [SerializeField] private float[] fishSpawnZs;
    [Range(0,1f)]
    [SerializeField] private float spawnChance;
    [Range(0,1f)]
    [SerializeField] private float shinyChance;

    [SerializeField] private float shinyRewardMultiplier;
    
    private RectTransform spawnArea;
    private float fishSpawnChance;

    private void Start()
    {
        spawnArea = GetComponent<RectTransform>();
        fishSpawnChance = spawnChance;
    }
    
    //Called from menu controller
    
    public IEnumerator BeginRound(float duration)
    {
        curTime = 0;
        while (curTime < duration)
        {
            if (Random.Range(0, 1f) <= fishSpawnChance)
            {
                SpawnFish();
            }

            curTime += Time.deltaTime;
            yield return null;
        }
        gameEndEvent.Invoke();
        yield return null;
    }

    private void Update()
    {
        fishCollectedText.text = "FISH COLLECTED: " + playerPoints;
    }

    private void SpawnFish()
    {
        int randomFish = Random.Range(0, spawnableFish.Length);

        Vector3 spawnLoc = new Vector3(Random.Range(spawnArea.rect.min.x, spawnArea.rect.max.x), Random.Range(spawnArea.rect.min.y, spawnArea.rect.max.y), fishSpawnZs[Random.Range(0,fishSpawnZs.Length)]);

        spawnLoc += transform.position;
        Transform go = Instantiate(spawnableFish[randomFish], spawnLoc, spawnableFish[randomFish].transform.rotation, transform).transform;
        //is shiny
        go.GetComponent<Fish>().ptsValue = (int) (multiplier * go.GetComponent<Fish>().ptsValue);
        if (Random.Range(0, 1f) <= shinyChance)
        {
            go.GetComponent<Fish>().ptsValue =  (int)(go.GetComponent<Fish>().ptsValue * shinyRewardMultiplier);
            Instantiate(shinyParticle, go);
        }
    }

    public void SetFishSpawnChance(float spawnChance) => fishSpawnChance = spawnChance;
}
